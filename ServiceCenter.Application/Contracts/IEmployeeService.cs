using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IEmployeeService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new employee to the database.
    /// </summary>
    /// <param name="employeeRequestDto">the employee data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the employee addition.</returns>
	public Task<Result> AddEmployeeAsync(EmployeeRequestDto employeeRequestDto);

    /// <summary>
    /// asynchronously retrieves all employees in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of employee response DTOs.</returns>
	public Task<Result<PaginationResult<EmployeeResponseDto>>> GetAllEmployeesAsync(int itemCount, int index);

    /// <summary>
    /// asynchronously retrieves a employee by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the employee to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the employee response DTO.</returns>
	public Task<Result<EmployeeGetByIdResponseDto>> GetEmployeeByIdAsync(string id);

    /// <summary>
    /// asynchronously updates the data of an existing employee.
    /// </summary>
    /// <param name="id">the unique identifier of the employee to update.</param>
    /// <param name="employeeRequestDto">the employee data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<EmployeeResponseDto>> UpdateEmployeeAsync(string id, EmployeeRequestDto employeeRequestDto);


    /// <summary>
    /// asynchronously searches for employees based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within employee data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of employee response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<EmployeeResponseDto>>> SearchEmployeeByTextAsync(string text, int itemCount, int index);

    /// <summary>
    /// asynchronously deletes a employee from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the employee to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteEmployeeAsync(string id);
}
