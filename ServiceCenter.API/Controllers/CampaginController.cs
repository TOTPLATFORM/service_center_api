using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;

public class CampaginController(ICampaginService campaginService) : BaseController
{
	private readonly ICampaginService _campaginService = campaginService;

	/// <summary>
	/// adds a new campagin to the system.
	/// </summary>
	/// <param name="campaginRequestDto">the data transfer object containing developer details for creation.</param>
	/// <remarks>
	/// access is limited to users with the "manager" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Manager")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddCampagin(CampaginRequestDto campaginRequestDto)
	{
		return await _campaginService.AddCampaginAsync(campaginRequestDto);
	}

	/// <summary>
	/// get all campagins to the system.
	/// </summary>
	/// <remarks>
	/// access is limited to users with the "manager , sales" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet]
	[Authorize(Roles = "Manager,Sales")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CampaginResponseDto>>> GetAllCampagins()
	{
		return await _campaginService.GetAllCampaginsAsync();
	}

	/// <summary>
	/// get campagin by id to the system.
	/// </summary>
	/// <param name="id">id of campagin</param>
	/// <remarks>
	/// access is limited to users with the "manager , sales" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("{id}")]
	[Authorize(Roles = "Manager,Sales")]
	[ProducesResponseType(typeof(Result< CampaginGetByIdResposeDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CampaginGetByIdResposeDto>> GetCampaginById(int id)
	{
		return await _campaginService.GetCampaginByIdAsync(id);
	}

	/// <summary>
	/// update campagin by id to the system.
	/// </summary>
	/// <param name="campaginRequestDto">the data transfer object containing campagin details for creation.</param>
	/// <param name="id">id of campagin.</param>
	/// <remarks>
	/// Access is limited to users with the "Manager" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpPut("{id}")]
	[Authorize(Roles = "Manager")]
	[ProducesResponseType(typeof(Result<CampaginGetByIdResposeDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CampaginGetByIdResposeDto>> UpdateCampagin(int id, CampaginRequestDto campaginRequestDto)
	{
		return await _campaginService.UpdateCampaginAsync(id, campaginRequestDto);
	}

    /// <summary>
    /// update campagin by id to the system.
    /// </summary>
    /// <param name="status">The data transfer object containing campagin details for creation.</param>
    /// <param name="id">id of campagin.</param>
    /// <remarks>
    /// access is limited to users with the "manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPut("campaginId/{id}/status/{status}")]
	[Authorize(Roles = "Manager")]
	[ProducesResponseType(typeof(Result<CampaginGetByIdResposeDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginStatus(CampaginStatus status, int id)
	{
		return await _campaginService.UpdateCampaginStatusAsync(id, status);
	}
	/// <summary>
	/// search  campagin by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// access is limited to users with the "Manager,Sales" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search/{text}")]
	[Authorize(Roles = "Manager,Sales")]
	[ProducesResponseType(typeof(Result<CampaginResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CampaginResponseDto>>> SerachCampaginByText(string text)
	{
		return await _campaginService.SearchCampaginByTextAsync(text);
	}

    /// <summary>
    /// delete  campagin by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteCampaginAsycn(int id)
	{
		return await _campaginService.DeleteCampaginAsync(id);
	}

}
