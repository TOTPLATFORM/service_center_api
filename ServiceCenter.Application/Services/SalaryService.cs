using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class SalaryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<SalaryService> logger, IUserContextService userContext) : ISalaryService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<SalaryService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result> AddSalaryAsync(SalaryRequestDto salaryRequestDto)
    {
        var salary = _mapper.Map<Salary>(salaryRequestDto);
        var employee = await _dbContext.Employees.FindAsync(salaryRequestDto.EmployeeId);

        var existSalary = await _dbContext.Salaries
            .Where(e => e.EmployeeId == salaryRequestDto.EmployeeId && e.SalaryDate.Year == salaryRequestDto.SalaryDate.Year && e.SalaryDate.Month == salaryRequestDto.SalaryDate.Month)
            .FirstOrDefaultAsync();

        if (existSalary != null)
        {
            _logger.LogError("Employee already has salary for this month");
            return Result.Error("Employee already has salary for this month");
        }


        if (salary is null)
        {
            _logger.LogError("Failed to map salaryRequestDto to Salary. salaryRequestDto: {@salaryRequestDto}", salaryRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Error"
                }
            });
        }

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", salaryRequestDto.EmployeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        salary.Employee = employee;

        salary.CreatedBy = _userContext.Email;
        await _dbContext.Salaries.AddAsync(salary);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Salary added successfully in the database");
        return Result.SuccessWithMessage("Salary added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteSalaryAsync(int id)
    {
        var salary = await _dbContext.Salaries.FindAsync(id);

        if (salary is null)
        {
            _logger.LogWarning("salary Invaild Id ,Id {id}", id);
            return Result.NotFound(["salary Invaild Id"]);
        }

        _dbContext.Salaries.Remove(salary);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("salary removed successfully in the database");
        return Result.SuccessWithMessage("Salary removed successfully ");
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<SalaryResponseDto>>> GetAllSalariesAsync(int itemCount ,int index)
    {
        var salaries = await _dbContext.Salaries
                  .ProjectTo<SalaryResponseDto>(_mapper.ConfigurationProvider)
                  .GetAllWithPagination(itemCount, index);
        _logger.LogInformation("Fetching all salaries. Total count: {salaries}.", salaries.Data.Count);
        return Result.Success(salaries);
    }

    /// <inheritdoc/>
    public async Task<Result<SalaryResponseDto>> GetSalaryByIdAsync(int id)
    {
        var salary = await _dbContext.Salaries
            .ProjectTo<SalaryResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (salary is null)
        {
            _logger.LogWarning("salary Id not found,Id {id}", id);
            return Result.NotFound(["salary not found"]);
        }
        _logger.LogInformation("Fetching salary");
        return Result.Success(salary);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<SalaryResponseDto>>> GetSalaryByEmployeeIdAsync(string employeeId,int itemCount,int index)
    {
        var employee = await _dbContext.Employees.FindAsync(employeeId);

        var salary = await _dbContext.Salaries
            .Where(e => e.EmployeeId == employeeId)
            .ProjectTo<SalaryResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount,index);

        if (employee is null)
        {
            _logger.LogWarning("employee Id not found,Id {employeeId}", employeeId);
            return Result.NotFound(["employee not found"]);
        }

        if (salary is null)
        {
            _logger.LogWarning("salary not found,Id {employeeId}", employeeId);
            return Result.NotFound(["salary not found"]);
        }
        _logger.LogInformation("Fetching salary");
        return Result.Success(salary);
    }

    /// <inheritdoc/>
    public async Task<Result<SalaryResponseDto>> UpdateSalaryAsync(int id, SalaryRequestDto salaryRequestDto)
    {
        var salary = await _dbContext.Salaries.FindAsync(id);
        var employee = _dbContext.Employees.Find(salaryRequestDto.EmployeeId);

        if (salary is null)
        {
            _logger.LogWarning("salary Id not found,Id {id}", id);
            return Result.NotFound(["Salary not found"]);
        }



        salary.ModifiedBy = _userContext.Email;

        _mapper.Map(salaryRequestDto, salary);

        await _dbContext.SaveChangesAsync();

        var salaryResponse = _mapper.Map<SalaryResponseDto>(salary);
        if (salaryResponse is null)
        {
            _logger.LogError("Failed to map SalaryRequestDto to salaryResponseDto. SalaryRequestDto: {@salaryRequestDto}", salaryRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }

        _logger.LogInformation("Updated salary , Id {id}", id);

        return Result.Success(salaryResponse);
    }

    /// <inheritdoc/>
    public async Task<Result<SalaryResponseDto>> AddBonusOrDeductionAsync(int id, SalaryUpdateDto salaryUpdateDto)
    {
        var salary = await _dbContext.Salaries.FindAsync(id);
        //var employee = _dbContext.Employees.Find(salaryUpdateDto.EmployeeId);

        if (salary is null)
        {
            _logger.LogWarning("salary Id not found,Id {id}", id);
            return Result.NotFound(["Salary not found"]);
        }

        //if (employee is null)
        //{
        //    _logger.LogWarning("employee Invaild Id ,Id {employeeId}", salaryUpdateDto.EmployeeId);
        //    return Result.NotFound(["employee Invaild Id"]);
        //}

        //var bonus = salary.Bonus;
        //var deduction = salary.Deduction;

        salary.Bonus += salaryUpdateDto.Bonus;
        salary.Deduction += salaryUpdateDto.Deduction;


        salary.ModifiedBy = _userContext.Email;

        //_mapper.Map(salaryUpdateDto, salary);

        await _dbContext.SaveChangesAsync();

        var salaryResponse = _mapper.Map<SalaryResponseDto>(salary);
        if (salaryResponse is null)
        {
            _logger.LogError("Failed to map SalaryRequestDto to salaryResponseDto. SalaryRequestDto: {@salaryRequestDto}", salaryUpdateDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }

        _logger.LogInformation("Updated salary , Id {id}", id);

        return Result.Success(salaryResponse);
    }
}