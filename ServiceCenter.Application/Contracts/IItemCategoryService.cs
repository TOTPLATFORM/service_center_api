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
/// provides an interface for itemCategory-related services that manages itemCategory data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IItemCategoryService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new item category to the database.
	/// </summary>
	/// <param name="item categoryRequestDto">the item category data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the item category addition.</returns>
	public Task<Result> AddItemCategoryAsync(ItemCategoryRequestDto itemCategoryRequestDto);

	/// <summary>
	/// asynchronously retrieves all item categorys in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of item categoryes to retrieve</param>
	///<param name="index">index of item categories to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item category response DTOs.</returns>
	public Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemCategoryAsync(int itemCount, int index);

	/// <summary>
	/// <summary>
	/// asynchronously retrieves a item category by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the item category to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the item category response DTO.</returns>
	public Task<Result<ItemCategoryResponseDto>> GetItemCategoryByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing item category.
	/// </summary>
	/// <param name="id">the unique identifier of the item category to update.</param>
	/// <param name="item categoryRequestDto">the item category data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>

	public Task<Result<ItemCategoryResponseDto>> UpdateItemCategoryAsync(int id, ItemCategoryRequestDto ItemCategoryRequestDto);

	/// <summary>
	/// asynchronously deletes a item category from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the item category to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>

	public Task<Result> DeleteItemCategoryAsync(int id);

	/// <summary>
	/// asynchronously searches for item categorys based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within item category data.</param>
	/// <param name = "itemCount" > item count of item categoryes to retrieve</param>
	///<param name="index">index of item categoryes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item category response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ItemCategoryResponseDto>>> SearchItemCategoryByTextAsync(string text, int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves all item categorys in the system.
	/// </summary>
	/// <param name = "inventoryId" > id of inventory</param>
	/// <param name = "itemCount" > item count of item categoryes to retrieve</param>
	///<param name="index">index of item categories to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item category response DTOs.</returns>
	public Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemsCategoryForSpecificInventory(int inventoryId, int itemCount, int index);
}
