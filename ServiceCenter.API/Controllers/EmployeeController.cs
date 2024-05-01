using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddEmployee(EmployeeRequestDto employeeRequestDto)
	{
		return await _employeeService.AddEmployeeAsync(employeeRequestDto);
	}
}
