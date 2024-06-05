using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IDepartmentService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new department to the database.
    /// </summary>
    /// <param name="departmentRequestDto">the department data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the department addition.</returns>
	public Task<Result> AddDepartmentAsync(DepartmentRequestDto departmentRequestDto);

    /// <summary>
    /// asynchronously retrieves all departments in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of department response DTOs.</returns>
	public Task<Result<PaginationResult<DepartmentResponseDto>>> GetAllDepartmentsAsync(int itemCount, int index);

    /// <summary>
    /// asynchronously retrieves a department by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the department to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the department response DTO.</returns>
	public Task<Result<DepartmentGetByIdResponseDto>> GetDepartmentByIdAsync(int id);

    /// <summary>
    /// asynchronously updates the data of an existing department.
    /// </summary>
    /// <param name="id">the unique identifier of the department to update.</param>
    /// <param name="departmentRequestDto">the department data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<DepartmentGetByIdResponseDto>> UpdateDepartmentAsync(int id, DepartmentRequestDto departmentRequestDto);


    /// <summary>
    /// asynchronously searches for departments based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within department data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of department response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<DepartmentResponseDto>>> SearchDepartmentByTextAsync(string text, int itemCount, int index);

    /// <summary>
    /// asynchronously retrieves employees by department unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the department to retrieve its employees.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all employees by department id operation.</returns>
	public Task<Result<PaginationResult<DepartmentResponseDto>>> GetAllEmployeesForSpecificDepartmentAsync(int id, int itemCount, int index);
    /// <summary>
    /// asynchronously deletes a department from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the department to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteDepartmentAsync(int id);
}
