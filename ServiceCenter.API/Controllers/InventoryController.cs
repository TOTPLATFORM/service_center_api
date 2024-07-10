﻿using Microsoft.AspNetCore.Authorization;
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
	/// Access is limited to users with the "Admin,Manager" role.
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
    /// retrieves all inventory in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of inventory to retrieve</param>
    ///<param name="index">index of inventory to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all inventory.</returns> [HttpGet]
    [HttpGet]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<PaginationResult<InventoryResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<InventoryResponseDto>>> GetAllInventories(int itemCount, int index)
	{
		return await _inventoryService.GetAllInventoriesAsync( itemCount,  index);
	}
    /// <summary>
    /// retrieves a inventory  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the inventory .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,WarehouseManager,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the inventory category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
	[Authorize(Roles = "Admin,WarehouseManager,Manager")]
	[ProducesResponseType(typeof(Result<InventoryGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<InventoryGetByIdResponseDto>> GetInventoryById(int id)
	{
		return await _inventoryService.GetInventoryByIdAsync(id);
	}

    /// <summary>
    /// update  inventory in the system.
    /// </summary>
    ///<param name="id">id of inventory.</param>
    ///<param name="inventoryRequestDto">inventory dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<InventoryResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<InventoryResponseDto>> UpdateInventory(int id, InventoryUpdatedRequestDto inventoryRequestDto)
	{
		return await _inventoryService.UpdateInventoryAsync(id, inventoryRequestDto);
	}
    /// <summary>
    /// searches inventory  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of inventorys to retrieve</param>
    ///<param name="index">index of inventorys to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of inventory  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
	[Authorize(Roles = "Admin,Manager")]
	[ProducesResponseType(typeof(Result<PaginationResult<InventoryResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<InventoryResponseDto>>> SerachInventoryByText(string text, int itemCount, int index)
	{
		return await _inventoryService.SearchInventoryByTextAsync(text,  itemCount,  index);
	}

    /// <summary>
    /// deletes a inventory from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the inventory to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
	[HttpDelete("{id}")]
	[Authorize(Roles = "Manager,Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteInventoryAsycn(int id)
	{
		return await _inventoryService.DeleteInventoryAsync(id);
	}
}
