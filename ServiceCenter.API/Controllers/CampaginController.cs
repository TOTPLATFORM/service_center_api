using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class CampaginController(ICampaginService campaginService) : BaseController
{
	private readonly ICampaginService _campaginService = campaginService;

	/// <summary>
	/// Adds a new campagin to the system.
	/// </summary>
	/// <param name="campaginRequestDto">The data transfer object containing developer details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddCampagin(CampaginRequestDto campaginRequestDto)
	{
		return await _campaginService.AddCampaginAsync(campaginRequestDto);
	}

}
