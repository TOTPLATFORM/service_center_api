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

public class ItemCategoryController(IItemCategoryService itemCategoryService) : BaseController
{
    private readonly IItemCategoryService _itemCategoryService = itemCategoryService;

    /// <summary>
    /// action for add ItemCategory action that take  ItemCategory dto   
    /// </summary>
    /// <param name="itemCategoryDto">ItemCategory dto</param>
    /// <remarks>
    /// Access is limited to users with the "WarehouseManager,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddItemCategory(ItemCategoryRequestDto itemCategoryDto)
    {
        return await _itemCategoryService.AddItemCategoryAsync(itemCategoryDto);
    }
    /// <summary>
    /// retrieves all item category in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of item category to retrieve</param>
    ///<param name="index">index of item category to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "WarehouseManager,Admin,Manager,ServiceProvider" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all item category.</returns> [HttpGet]

    [HttpGet]
    [Authorize(Roles = "WarehouseManager,Admin,Manager,ServiceProvider")]
    [ProducesResponseType(typeof(Result<List<ItemCategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemCategorys(int itemCount, int index)
    {
        return await _itemCategoryService.GetAllItemCategoryAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a item category  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the item category .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,WarehouseManager,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the item category category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ItemCategoryResponseDto>> GetItemCategoryById(int id)
    {
        return await _itemCategoryService.GetItemCategoryByIdAsync(id);
    }
    /// <summary>
    /// action for update ItemCategory by id that take ItemCategory id.  
    /// </summary> 
    /// <param name="id">ItemCategory id</param>
    /// <param name="itemCategoryDto">ItemCategory dto</param>
    /// <remarks>
    /// Access is limited to users with the "WarehouseManager,Manager,Admin,ServiceProvider" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "WarehouseManager,Manager")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemCategoryResponseDto>> UpdateItemCategory(int id, ItemCategoryRequestDto itemCategoryDto)
    {
        return await _itemCategoryService.UpdateItemCategoryAsync(id, itemCategoryDto);
    }

    /// <summary>
    /// deletes a item category from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the item category to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "WarehouseManager,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteItemCategory(int id)
    {
        return await _itemCategoryService.DeleteItemCategoryAsync(id);
    }
    /// <summary>
    /// searches item category  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of item categorys to retrieve</param>
    ///<param name="index">index of item categorys to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "WarehouseManager,Admin,Manager,ServiceProvider" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of item category  that match the search criteria.</returns>


    [HttpGet("search/{text}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager,ServiceProvider")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ItemCategoryResponseDto>>> SearchItemCategoryByText(string text, int itemCount, int index)
    {
        return await _itemCategoryService.SearchItemCategoryByTextAsync(text,itemCount,index);
    }
    /// <summary>
    /// retrieves facilities by their property unique identifier.
    /// </summary>
    ///<param name="id">the unique identifier of the property</param>  
    /// <param name = "itemCount" > item count of item category to retrieve</param>
    ///<param name="index">index of item category to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the property's facilities.</returns>

    [HttpGet("searchByRelation/{id}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager,ServiceProvider")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ItemCategoryResponseDto>>> SearchItemCategoryByRelation(int id, int itemCount, int index)
    {
        return await _itemCategoryService.GetAllItemsCategoryForSpecificInventory(id, itemCount, index);
    }
}






