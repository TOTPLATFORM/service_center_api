using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class EmployeeController(IEmployeeService employeeService) : BaseController
{
	private readonly IEmployeeService _employeeService = employeeService;

	/// <summary>
	/// Adds a new employee to the system.
	/// </summary>
	/// <param name="employeeRequestDto">The data transfer object containing employee details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddEmployee(EmployeeRequestDto employeeRequestDto)
	{
		return await _employeeService.AddEmployeeAsync(employeeRequestDto);
	}

	/// <summary>
	/// get all employees in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<List<EmployeeResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<List<EmployeeResponseDto>>> GetAllEmployees()
	{
		return await _employeeService.GetAllEmployeesAsync();
	}
	/// <summary>
	/// get all employees in the system.
	/// </summary>
	///<param name="id">id of employee.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<EmployeeResponseDto>> GetEmployeeById(string id)
	{
		return await _employeeService.GetEmployeeByIdAsync(id);
	}

	/// <summary>
	/// get  employee by id in the system.
	/// </summary>
	///<param name="id">id of employee.</param>
	///<param name="employeeRequestDto">employee dto.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager,Employee")]
    [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<EmployeeResponseDto>> UpdateEmployee(string id, EmployeeRequestDto employeeRequestDto)
	{
		return await _employeeService.UpdateEmployeeAsync(id, employeeRequestDto);
	}
	/// <summary>
	/// search  employee by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search/{text}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<EmployeeResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<List<EmployeeResponseDto>>> SerachEmployeeByText(string text)
	{
		return await _employeeService.SearchEmployeeByTextAsync(text);
	}
    /// <summary>
    /// delete  employee by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result> DeleteEmployeeAsycn(string id)
	{
		return await _employeeService.DeleteEmployeeAsync(id);
	}
}
