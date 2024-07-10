using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class DepartmentController(IDepartmentService departmentService) : BaseController
{
	private readonly IDepartmentService _departmentService = departmentService;

	/// <summary>
	/// Adds a new department to the system.
	/// </summary>
	/// <param name="departmentRequestDto">The data transfer object containing department details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddDepartment(DepartmentRequestDto departmentRequestDto)
	{
		return await _departmentService.AddDepartmentAsync(departmentRequestDto);
	}


	/// <summary>
	/// get all departments in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin,Manager" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<PaginationResult<DepartmentResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<DepartmentResponseDto>>> GetAllDepartments(int itemCount, int index)
	{
		return await _departmentService.GetAllDepartmentsAsync(itemCount,index);
	}
    /// <summary>
    /// retrieves a department  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the department .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the department category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<DepartmentGetByIdResponseDto>> GetDepartmentById(int id)
	{
		return await _departmentService.GetDepartmentByIdAsync(id);
	}

    /// <summary>
    /// updates an existing department's information.
    /// </summary>
    /// <param name="id">the unique identifier of the expense to update.</param>
    /// <param name="departmentRequestDto">the data transfer object containing updated details for the department.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<DepartmentGetByIdResponseDto>> UpdateDepartment(int id, DepartmentRequestDto departmentRequestDto)
	{
		return await _departmentService.UpdateDepartmentAsync(id, departmentRequestDto);
	}
    /// <summary>
    /// searches department  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of departments to retrieve</param>
    ///<param name="index">index of departments to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of department  that match the search criteria.</returns>

    [HttpGet("search")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<DepartmentResponseDto>>> SerachDepartmentByText(string text, int itemCount, int index)
	{
		return await _departmentService.SearchDepartmentByTextAsync(text,itemCount,index);
	}

    /// <summary>
    /// retrieves facilities by their property unique identifier.
    /// </summary>
    ///<param name="id">the unique identifier of the property</param>  
    /// <param name = "itemCount" > item count of department to retrieve</param>
    ///<param name="index">index of department to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the property's facilities.</returns>


    [HttpGet("searchByRelation")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<DepartmentResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<PaginationResult<EmployeeResponseDto>>> SearchDepartmentByRelation(int id, int itemCount, int index)
	{
		return await _departmentService.GetAllEmployeesForSpecificDepartmentAsync(id,itemCount,index);
	}


    /// <summary>
    /// deletes a department from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the department to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>

    [HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result> DeleteDepartment(int id)
	{
		return await _departmentService.DeleteDepartmentAsync(id);
	}
}