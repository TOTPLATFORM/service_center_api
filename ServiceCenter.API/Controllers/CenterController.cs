using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;
namespace ServiceCenter.API.Controllers;

public class CenterController(ICenterService centerService) : BaseController

{
	private readonly ICenterService _centerService = centerService;

	/// <summary>
	/// Adds a new center to the system.
	/// </summary>
	/// <param name="CenterRequestDto">The data transfer object containing center details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddCenter(CenterRequestDto centerRequestDto)
	{
		return await _centerService.AddCenterAsync(centerRequestDto);
	}

	/// <summary>
	/// get all centers in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<CenterResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CenterResponseDto>>> GetAllCenters()
	{
		return await _centerService.GetAllCentersAsync();
	}
	/// <summary>
	/// get center by id  from the system.
	/// </summary>
	///<param name="id">id of center.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CenterResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CenterResponseDto>> GetCenterById(int id)
	{
		return await _centerService.GetCenterByIdAsync(id);
	}

	/// <summary>
	/// get  inventory by id in the system.
	/// </summary>
	///<param name="id">id of inventory.</param>
	///<param name="CenterRequestDto">inventory dto.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPut("{id}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CenterResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CenterResponseDto>> UpdateCenter(int id, CenterRequestDto centerRequestDto)
	{
		return await _centerService.UpdateCenterAsync(id, centerRequestDto);
	}
	/// <summary>
	/// search  center by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search/{text}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CenterResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CenterResponseDto>>> SerachCenterByText(string text)
	{
		return await _centerService.SearchCenterByTextAsync(text);
	}
	/// <summary>
	/// delete  center by id from the system.
	/// </summary>
	///<param name="id">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpDelete]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteCenterAsycn(int id)
	{
		return await _centerService.DeleteCenterAsync(id);
	}
}

