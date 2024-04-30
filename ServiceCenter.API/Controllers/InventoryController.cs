using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class InventoryController(IInventoryService inventoryService) : BaseController
{
	private readonly IInventoryService _inventoryService = inventoryService;

	/// <summary>
	/// Adds a new inventory to the system.
	/// </summary>
	/// <param name="InventoryRequestDto">The data transfer object containing developer details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddInventory(InventoryRequestDto inventoryRequestDto)
	{
		return await _inventoryService.AddInventoryAsync(inventoryRequestDto);
	}

	/// <summary>
	/// get all inventories in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<TimeSlotResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<InventoryResponseDto>>> GetAllInventories()
	{
		return await _inventoryService.GetAllInventoriesAsync();
	}
}
