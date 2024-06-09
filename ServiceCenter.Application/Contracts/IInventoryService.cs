using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for inventory-related services that manages inventory data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IInventoryService : IApplicationService ,IScopedService
{
	/// <summary>
	/// function to add inventory that take inventoryDto   
	/// </summary>
	/// <param name="InventoryRequestDto">inventory request dto</param>
	/// <returns>Inventory added successfully </returns>
	public Task<Result> AddInventoryAsync(InventoryRequestDto inventoryRequestDto);

	/// <summary>
	/// function to get all inventories 
	/// </summary>
	/// <returns>list all inventoryResponseDto </returns>
	public Task<Result<PaginationResult<InventoryResponseDto>>> GetAllInventoriesAsync(int itemCount, int index);

	/// <summary>
	/// function to get inventory by id that take  inventory id
	/// </summary>
	/// <param name="id">inventory id</param>
	/// <returns>inventory response dto</returns>
	public Task<Result<InventoryResponseDto>> GetInventoryByIdAsync(int id);

	/// <summary>
	/// function to update inventory that take InventoryRequestDto   
	/// </summary>
	/// <param name="id">inventory id</param>
	/// <param name="InventoryRequestDto">inventory dto</param>
	/// <returns>Updated Inventory </returns>
	public Task<Result<InventoryResponseDto>> UpdateInventoryAsync(int id, InventoryRequestDto InventoryRequestDto);


	/// <summary>
	/// function to search inventory by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all inventory that contain this text </returns>
	public Task<Result<PaginationResult<InventoryResponseDto>>> SearchInventoryByTextAsync(string text, int itemCount, int index);

	/// <summary>
	/// function to delete Inventory that take InventoryDto   
	/// </summary>
	/// <param name="id">inventory id</param>
	/// <returns>Inventory removed successfully </returns>
	public Task<Result> DeleteInventoryAsync(int id);
}
