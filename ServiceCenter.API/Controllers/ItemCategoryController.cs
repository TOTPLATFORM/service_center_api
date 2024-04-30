using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceCenter.Application.Services;

namespace ServiceCenter.API.Controllers;

public class ItemCategoryController(IItemCategoryService itemCategoryService) : BaseController
{
    private readonly IItemCategoryService _itemCategoryService = itemCategoryService;

    /// <summary>
    /// action for add ItemCategory action that take  ItemCategory dto   
    /// </summary>
    /// <param name="itemCategoryDto">ItemCategory dto</param>
    /// <returns>result for ItemCategory added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddItemCategory(ItemCategoryRequestDto itemCategoryDto)
    {
        return await _itemCategoryService.AddItemCategoryAsync(itemCategoryDto);
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ItemCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ItemCategoryResponseDto>>> GetAllItemCategorys()
    {
        return await _itemCategoryService.GetAllItemCategoryAsync();
    }
    /// <summary>
    /// action for get ItemCategory by id that take ItemCategory id.  
    /// </summary>
    /// <returns>result of ItemCategory response dto</returns>
    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemCategoryResponseDto>> GetItemCategoryById(int id)
    {
        return await _itemCategoryService.GetItemCategoryByIdAsync(id);
    }
    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemCategoryResponseDto>> UpdateItemCategory(int id, ItemCategoryRequestDto itemCategoryDto)
    {
        return await _itemCategoryService.UpdateItemCategoryAsync(id, itemCategoryDto);
    }
    /// <summary>
    ///  action for remove ItemCategory that take ItemCategory id   
    /// </summary>
    /// <param name="id">ItemCategory id</param>
    /// <returns>result of ItemCategory removed successfully </returns>
    [HttpDelete]
    //  [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteItemCategoryAsycn(int id)
    {
        return await _itemCategoryService.DeleteItemCategoryAsync(id);
    }

    /// <summary>
    /// function to search by ItemCategory name  that take  ItemCategory name
    /// </summary>
    /// <param name="text">ItemCategory name</param>
    /// <returns>ItemCategory response dto </returns>
    [HttpGet("search")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ItemCategoryResponseDto>>> SearchItemCategoryByTextAsync(string text)
    {
        return await _itemCategoryService.SearchItemCategoryByTextAsync(text);
    }
}



