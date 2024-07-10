using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
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
	/// access is limited to users with the "Admin,Manager" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddCampagin(CampaginRequestDto campaginRequestDto)
	{
		return await _campaginService.AddCampaginAsync(campaginRequestDto);
	}

    /// <summary>
    /// retrieves all campagin in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of campagin to retrieve</param>
    ///<param name="index">index of campagin to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all campagin.</returns> [HttpGet]

    [HttpGet]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<CampaginResponseDto>>> GetAllCampagins(int itemCount, int index)
	{
		return await _campaginService.GetAllCampaginsAsync( itemCount, index);
	}

    /// <summary>
    /// retrieves a campagin  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the campagin .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the campagin category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
	[ProducesResponseType(typeof(Result<CampaginGetByIdResposeDto>), StatusCodes.Status200OK)]
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
	/// Access is limited to users with the "Manager,Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<CampaginGetByIdResposeDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CampaginGetByIdResposeDto>> UpdateCampagin(int id, CampaginRequestDto campaginRequestDto)
	{
		return await _campaginService.UpdateCampaginAsync(id, campaginRequestDto);
	}

	/// <summary>
	/// update campagin by id to the system.
	/// </summary>
	/// <param name="status">campagin status.</param>
	/// <param name="id">id of campagin.</param>
	/// <remarks>
	/// access is limited to users with the "Admin,Manager" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
	[HttpPut("campaginId/{id}/status/{status}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<CampaginGetByIdResposeDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginStatus(CampaginStatus status, int id)
	{
		return await _campaginService.UpdateCampaginStatusAsync(id, status);
	}
    /// <summary>
    /// searches campagin  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of appoinments to retrieve</param>
    ///<param name="index">index of appoinments to retrieve</param>
	/// <remarks>
	/// access is limited to users with the "Manager,Admin" role.
	/// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of compagin  that match the search criteria.</returns>
    [HttpGet("search/{text}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<CampaginResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<CampaginResponseDto>>> SerachCampaginByText(string text, int itemCount, int index)
	{
		return await _campaginService.SearchCampaginByTextAsync(text, itemCount, index);
	}


    /// <summary>
    /// deletes a campagin from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the campagin to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteCampaginAsycn(int id)
	{
		return await _campaginService.DeleteCampaginAsync(id);
	}

}
