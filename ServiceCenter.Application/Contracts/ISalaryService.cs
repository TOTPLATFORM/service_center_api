using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for salary -related services that manages salary  data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface ISalaryService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new salary  to the database.
	/// </summary>
	/// <param name="salaryRequestDto">the salary  data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the salary  addition.</returns>
	public Task<Result> AddSalaryAsync(SalaryRequestDto salaryRequestDto);

	/// <summary>
	/// asynchronously retrieves all salary s in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of salary  to retrieve</param>
	///<param name="index">index of salary  to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of salary  response DTOs.</returns>
	public Task<Result<PaginationResult<SalaryResponseDto>>> GetAllSalariesAsync(int itemCount,int index);

	/// <summary>
	/// asynchronously retrieves a salary  by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the salary  to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the salary  response DTO.</returns>
	public Task<Result<SalaryResponseDto>> GetSalaryByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing salary .
	/// </summary>
	/// <param name="id">the unique identifier of the salary  to update.</param>
	/// <param name="salaryRequestDto">the salary  data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<SalaryResponseDto>> UpdateSalaryAsync(int id, SalaryRequestDto salaryRequestDto);

	/// <summary>
	/// asynchronously deletes a salary  from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the salary  to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteSalaryAsync(int id);

    public Task<Result<SalaryResponseDto>> AddBonusOrDeductionAsync(int id, SalaryUpdateDto salaryUpdateDto);

    /// <summary>
    /// Retrieves a salary by employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>The result containing the salary response data transfer object.</returns>
    public Task<Result<PaginationResult<SalaryResponseDto>>> GetSalaryByEmployeeIdAsync(string employeeId,int itemCount,int index);
}
