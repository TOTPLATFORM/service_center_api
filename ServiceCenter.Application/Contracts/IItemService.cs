﻿using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for item-related services that manages item data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IItemService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new item to the database.
	/// </summary>
	/// <param name="itemRequestDto">item request dto</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the item addition.</returns>
	public Task<Result> AddItemAsync(ItemRequestDto itemRequestDto);
	/// <summary>
	/// asynchronously retrieves all items in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of items to retrieve</param>
	///<param name="index">index of items to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item response DTOs.</returns>
	public Task<Result<PaginationResult<ItemResponseDto>>> GetAllItemAsync(int itemCount,int index);

	/// <summary>
	/// asynchronously retrieves a item by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the item to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the item response DTO.</returns>
	public Task<Result<ItemResponseDto>> GetItemByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing item.
	/// </summary>
	/// <param name="id">the unique identifier of the item to update.</param>
	/// <param name="itemRequestDto">the item data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ItemResponseDto>> UpdateItemAsync(int id, ItemRequestDto itemRequestDto);

	/// <summary>
	/// asynchronously deletes a item from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the item to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteItemAsync(int id);

	
	/// <summary>
	/// asynchronously searches for items based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within item data.</param>
	/// <param name = "itemCount" > item count of items to retrieve</param>
	///<param name="index">index of items to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ItemResponseDto>>> SearchItemByTextAsync(string text,int itemCount , int index);



}
