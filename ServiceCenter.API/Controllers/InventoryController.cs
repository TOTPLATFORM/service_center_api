using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class InventoryController(IInventoryService inventoryService) : BaseController
{
	private readonly IInventoryService _inventoryService = inventoryService;

	/// <summary>
	/// Adds a new inventory to the system.
	/// </summary>
	/// <param name="inventoryRequestDto">The data transfer object containing developer details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin,Manager")]
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
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<PaginationResult<InventoryResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<InventoryResponseDto>>> GetAllInventories(int itemCount, int index)
	{
		return await _inventoryService.GetAllInventoriesAsync( itemCount,  index);
	}
	/// <summary>
	/// get all inventories in the system.
	/// </summary>
	///<param name="id">id of inventory.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
	[Authorize(Roles = "Admin,WarehouseManager,Manager")]
	[ProducesResponseType(typeof(Result<InventoryGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<InventoryGetByIdResponseDto>> GetInventoryById(int id)
	{
		return await _inventoryService.GetInventoryByIdAsync(id);
	}

	/// <summary>
	/// get  inventory by id in the system.
	/// </summary>
	///<param name="id">id of inventory.</param>
	///<param name="InventoryRequestDto">inventory dto.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<InventoryResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<InventoryResponseDto>> UpdateInventory(int id, InventoryUpdatedRequestDto inventoryRequestDto)
	{
		return await _inventoryService.UpdateInventoryAsync(id, inventoryRequestDto);
	}
	/// <summary>
	/// search  inventory by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search/{text}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<PaginationResult<InventoryResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<InventoryResponseDto>>> SerachInventoryByText(string text, int itemCount, int index)
	{
		return await _inventoryService.SearchInventoryByTextAsync(text,  itemCount,  index);
	}
	/// <summary>
	/// delete  inventory by id from the system.
	/// </summary>
	///<param name="id">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpDelete("{id}")]
	[Authorize(Roles = "Manager,Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteInventoryAsycn(int id)
	{
		return await _inventoryService.DeleteInventoryAsync(id);
	}
}
