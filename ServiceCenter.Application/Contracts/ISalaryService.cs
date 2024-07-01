using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// Service interface for handling salary-related operations.
/// </summary>
public interface ISalaryService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new salary.
    /// </summary>
    /// <param name="salaryRequestDto">The data transfer object containing salary information.</param>
    /// <returns>The result indicating the success of adding the salary.</returns>
    public Task<Result> AddSalaryAsync(SalaryRequestDto salaryRequestDto);

    /// <summary>
    /// Retrieves all salaries.
    /// </summary>
    /// <returns>The result containing a list of salary response data transfer objects.</returns>
    public Task<Result<List<SalaryResponseDto>>> GetAllSalariesAsync();

    /// <summary>
    /// Retrieves a salary by its ID.
    /// </summary>
    /// <param name="id">The ID of the salary to retrieve.</param>
    /// <returns>The result containing the salary response data transfer object.</returns>
    public Task<Result<SalaryResponseDto>> GetSalaryByIdAsync(int id);

    /// <summary>
    /// Updates a salary by its ID.
    /// </summary>
    /// <param name="id">The ID of the salary to update.</param>
    /// <param name="salaryRequestDto">The data transfer object containing updated salary information.</param>
    /// <returns>The result containing the updated salary response data transfer object.</returns>
    public Task<Result<SalaryResponseDto>> UpdateSalaryAsync(int id, SalaryRequestDto salaryRequestDto);

    /// <summary>
    /// Removes a salary by its ID.
    /// </summary>
    /// <param name="id">The ID of the salary to remove.</param>
    /// <returns>The result indicating the success of removing the salary.</returns>
    public Task<Result> DeleteSalaryAsync(int id);

    public Task<Result<SalaryResponseDto>> AddBonusOrDeductionAsync(int id, SalaryUpdateDto salaryUpdateDto);

    /// <summary>
    /// Retrieves a salary by employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>The result containing the salary response data transfer object.</returns>
    public Task<Result<List<SalaryResponseDto>>> GetSalaryByEmployeeIdAsync(string employeeId);
}
