using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;

namespace ServiceCenter.API.Controllers;

public class ItemController(IItemService itemService) : BaseController
{
    private readonly IItemService _ItemService = itemService;

    /// <summary>
    /// action for add item action that take  Item dto   
    /// </summary>
    /// <param name="itemDto">item dto</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "WarehouseManager,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddItem(ItemRequestDto itemDto)
    {
        return await _ItemService.AddItemAsync(itemDto);
    }
    /// <summary>
    /// retrieves all item  in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of item  to retrieve</param>
    ///<param name="index">index of item  to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "WarehouseManager,Admin,Manager,ServiceProvider" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all item .</returns> [HttpGet]

    [HttpGet]
    [Authorize(Roles = "ServiceProvider,Manager,Admin,WarehouseManager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ItemResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ItemResponseDto>>> GetAllItems(int itemCount,int index)
    {
        return await _ItemService.GetAllItemAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a item   by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the item  .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,WarehouseManager,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the item  category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ItemResponseDto>> GetItemById(int id)
    {
        return await _ItemService.GetItemByIdAsync(id);
    }
    /// <summary>
      /// action for update Item by id that take Item id.  
      /// </summary> 
      /// <param name="id">Item id</param>
      /// <param name="itemDto">Item dto</param>
      /// <remarks>
      /// Access is limited to users with the "WarehouseManager,Manager" role.
      /// </remarks>
      /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "WarehouseManager,Manager")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemResponseDto>> UpdateItem(int id, ItemRequestDto itemDto)
    {
        return await _ItemService.UpdateItemAsync(id, itemDto);
    }

    /// <summary>
    /// deletes a item  from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the item  to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "WarehouseManager,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteItem(int id)
    {
        return await _ItemService.DeleteItemAsync(id);
    }
    /// <summary>
    /// searches item   based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of item s to retrieve</param>
    ///<param name="index">index of item s to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "WarehouseManager,Admin,Manager,ServiceProvider" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item   that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "ServiceProvider,Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ItemResponseDto>>> SearchItemByText(string text,int itemCount,int index)
    {
        return await _ItemService.SearchItemByTextAsync(text,itemCount,index);
    }



}