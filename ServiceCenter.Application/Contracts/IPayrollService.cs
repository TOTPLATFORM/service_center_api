using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// Service interface for handling payroll-related operations.
/// </summary>
public interface IPayrollService : IApplicationService, IScopedService
{
    /// <summary>
    /// Calculates the payroll for an employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>A <see cref="Result{PayrollResponseDto}"/> representing the payroll calculation result.</returns>
    public Task<Result<PayrollResponseDto>> PayrollCalculate(string employeeId);

    /// <summary>
    /// Calculates the salary for an employee within a specific period.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <returns>A <see cref="Result{PayrollResponseDto}"/> representing the salary calculation result.</returns>
    public Task<Result<PayrollResponseDto>> CalculateSalaryInSpecificPeriod(string employeeId, DateTime startDate, DateTime endDate);

    /// <summary>
    /// Calculates the payroll for an employee for a specific month.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="year">The year of the month.</param>
    /// <param name="month">The month for which to calculate the payroll.</param>
    /// <returns>A <see cref="Result{PayrollResponseDto}"/> representing the payroll calculation result.</returns>
    public Task<Result<PayrollResponseDto>> CalculatePayrollForSpecificMonth(string employeeId, int year, int month);

    /// <summary>
    /// Generates a list of payrolls for all months that an employee has a salary in.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>A list of <see cref="PayrollResponseDto"/> for each month the employee has a salary.</returns>
    public Task<Result<List<PayrollResponseDto>>> CalculatePayrollsForAllMonths(string employeeId);
}