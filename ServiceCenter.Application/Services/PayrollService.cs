using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

/// <summary>
/// Provides services for payroll calculations.
/// </summary>
public class PayrollService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<PayrollService> logger, IUserContextService userContext) : IPayrollService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PayrollService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result<PayrollResponseDto>> PayrollCalculate(string employeeId)
    {
        var employee = await _dbContext.Employees
            .Include(e => e.Salaries)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            return Result<PayrollResponseDto>.NotFound("Employee ID is invalid");
        }

        var latestSalary = employee.Salaries.OrderByDescending(s => s.SalaryDate).FirstOrDefault();

        decimal totalSalary = employee.BaseSalary;
        if (latestSalary != null)
        {
            totalSalary += (latestSalary.Bonus ?? 0m) - (latestSalary.Deduction ?? 0m);
        }

        var payrollResponseDto = new PayrollResponseDto
        {
            TotalSalary = totalSalary,
            BaseSalary = employee.BaseSalary,
            Bonus = latestSalary?.Bonus ?? 0m,
            Deduction = latestSalary?.Deduction ?? 0m,
            EmployeeId = employee.Id,
            SalaryDate = latestSalary?.SalaryDate ?? DateOnly.FromDateTime(DateTime.Now),
           // EmployeeName = $"{employee.FirstName} {employee.LastName}"
        };

        return Result<PayrollResponseDto>.Success(payrollResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<PayrollResponseDto>> CalculateSalaryInSpecificPeriod(string employeeId, DateTime startDate, DateTime endDate)
    {
        var totalHoursWorked = await CalculateTotalHoursWorked(employeeId, startDate, endDate);

        var employee = await _dbContext.Employees
            .Include(e => e.Salaries)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            _logger.LogError("Employee record not found for ID: {employeeId}", employeeId);
            return Result<PayrollResponseDto>.NotFound("Employee record not found.");
        }

        var salary = employee.Salaries.FirstOrDefault(s => s.SalaryDate >= DateOnly.FromDateTime(startDate) && s.SalaryDate <= DateOnly.FromDateTime(endDate));

        double standardHours = 8 * (endDate - startDate).TotalDays;
        var hourlyRate = employee.BaseSalary / 30 / 8;

        var (bonusAmount, deductionAmount) = CalculateBonusAndDeduction(totalHoursWorked, employee.BaseSalary, standardHours);

        decimal totalSalary = employee.BaseSalary + bonusAmount - deductionAmount;
        if (salary != null)
        {
            totalSalary += (salary.Bonus ?? 0m) - (salary.Deduction ?? 0m);
        }

        var payrollResponseDto = new PayrollResponseDto
        {
            TotalSalary = totalSalary,
            EmployeeId = employee.Id,
          //  EmployeeName = $"{employee.FirstName} {employee.LastName}",
            SalaryDate = salary?.SalaryDate ?? DateOnly.FromDateTime(DateTime.Now),
            Bonus = salary?.Bonus ?? 0m,
            BaseSalary = employee.BaseSalary,
            Deduction = salary?.Deduction ?? 0m,
            TotalHoursWorked = totalHoursWorked,
            StandardHours = standardHours,
            HourlyRate = hourlyRate,
            BonusAmount = bonusAmount,
            DeductionAmount = deductionAmount
        };

        return Result<PayrollResponseDto>.Success(payrollResponseDto, "Salary adjusted successfully.");
    }

    /// <inheritdoc/>
    public async Task<Result<PayrollResponseDto>> CalculatePayrollForSpecificMonth(string employeeId, int year, int month)
    {
        DateTime startDate = new DateTime(year, month, 1);
        DateTime endDate = startDate.AddMonths(1).AddDays(-1);

        var totalHoursWorked = await CalculateTotalHoursWorked(employeeId, startDate, endDate);

        var employee = await _dbContext.Employees
            .Include(e => e.Salaries)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            _logger.LogError("Employee record not found for ID: {employeeId}", employeeId);
            return Result<PayrollResponseDto>.NotFound("Employee record not found.");
        }

        var salary = employee.Salaries.FirstOrDefault(s => s.SalaryDate.Year == year && s.SalaryDate.Month == month);

        double standardHours = 8 * DateTime.DaysInMonth(year, month);
        var hourlyRate = employee.BaseSalary / 30 / 8;

        var (bonusAmount, deductionAmount) = CalculateBonusAndDeduction(totalHoursWorked, employee.BaseSalary, standardHours);

        decimal totalSalary = employee.BaseSalary + bonusAmount - deductionAmount;
        if (salary != null)
        {
            totalSalary += (salary.Bonus ?? 0m) - (salary.Deduction ?? 0m);
        }

        var payrollResponseDto = new PayrollResponseDto
        {
            TotalSalary = totalSalary,
            EmployeeId = employee.Id,
         //   EmployeeName = $"{employee.FirstName} {employee.LastName}",
            SalaryDate = salary?.SalaryDate ?? new DateOnly(year, month, 1),
            Bonus = salary?.Bonus ?? 0,
            BaseSalary = employee.BaseSalary,
            Deduction = salary?.Deduction ?? 0,
            TotalHoursWorked = totalHoursWorked,
            StandardHours = standardHours,
            HourlyRate = hourlyRate,
            BonusAmount = bonusAmount,
            DeductionAmount = deductionAmount
        };

        return Result<PayrollResponseDto>.Success(payrollResponseDto, "Payroll calculated successfully for the specified month.");
    }

    /// <inheritdoc/>
    public async Task<Result<List<PayrollResponseDto>>> CalculatePayrollsForAllMonths(string employeeId)
    {
        var employee = await _dbContext.Employees
            .Include(e => e.Salaries)
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            return Result<List<PayrollResponseDto>>.NotFound("No salary records found for the employee.");
        }

        var payrolls = new List<PayrollResponseDto>();

        var firstAttendance = await _dbContext.Attendances
            .Where(a => a.EmployeeId == employeeId)
            .OrderBy(a => a.AttendanceDate)
            .FirstOrDefaultAsync();

        if (firstAttendance != null)
        {
            DateTime startDate = firstAttendance.AttendanceDate.ToDateTime(TimeOnly.MinValue);
            DateTime endDate = DateTime.Now;

            while (startDate <= endDate)
            {
                var payrollResult = await CalculatePayrollForSpecificMonth(employeeId, startDate.Year, startDate.Month);
                if (payrollResult.IsSuccess)
                {
                    payrolls.Add(payrollResult.Value);
                }
                startDate = startDate.AddMonths(1);
            }
        }

        return Result<List<PayrollResponseDto>>.Success(payrolls, "Payrolls calculated for all months.");
    }

    /// <summary>
    /// Calculates the total hours worked by an employee within a specific period.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <returns>The total hours worked by the employee.</returns>
    private async Task<double> CalculateTotalHoursWorked(string employeeId, DateTime startDate, DateTime endDate)
    {
        var attendances = await _dbContext.Attendances
            .Where(a => a.EmployeeId == employeeId &&
            a.AttendanceDate >= DateOnly.FromDateTime(startDate) &&
            a.AttendanceDate <= DateOnly.FromDateTime(endDate))
            .ToListAsync();

        var totalHoursWorked = attendances.Sum(a => a.TotalHours);

        return totalHoursWorked;
    }

    /// <summary>
    /// Calculates the bonus and deduction amounts based on hours worked and base salary.
    /// </summary>
    /// <param name="totalHoursWorked">The total hours worked by the employee.</param>
    /// <param name="baseSalary">The base salary of the employee.</param>
    /// <returns>A tuple containing the bonus amount and deduction amount.</returns>
    private (decimal bonusAmount, decimal deductionAmount) CalculateBonusAndDeduction(double totalHoursWorked, decimal baseSalary, double standardHours)
    {
        var hourlyRate = baseSalary / 30 / 8;

        decimal bonusAmount = 0;
        decimal deductionAmount = 0;

        if (totalHoursWorked > standardHours)
        {
            bonusAmount = (decimal)(totalHoursWorked - standardHours) * hourlyRate;
        }
        else if (totalHoursWorked < standardHours)
        {
            deductionAmount = (decimal)(standardHours - totalHoursWorked) * hourlyRate;
        }

        return (bonusAmount, deductionAmount);
    }
}