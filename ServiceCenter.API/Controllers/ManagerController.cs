using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ManagerController(IManagerService managerService) : BaseController
{
	private readonly IManagerService _managerService = managerService;

	/// <summary>
	/// Adds a new manager to the system.
	/// </summary>
	/// <param name="managerRequestDto">The data transfer object containing manager details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddManager(ManagerRequestDto managerRequestDto)
	{
		return await _managerService.AddManagerAsync(managerRequestDto);
	}
}
