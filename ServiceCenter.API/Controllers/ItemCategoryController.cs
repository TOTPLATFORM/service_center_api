using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServiceCenter.API.Controllers;

public class ItemCategoryController(IItemCategoryService itemCategoryService) : BaseController
{
    private readonly IItemCategoryService _itemCategoryService = itemCategoryService;


    /// <summary>
    /// action for get all item categories .
    /// </summary>
    /// <returns>result of list from  item category response dtos</returns>
    [HttpGet]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result<List<ItemCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ItemCategoryResponseDto>>> GetAllCategoriesAsync()
    {
        return await _itemCategoryService.GetAllCategoriesAsync();
    }

    /// <summary>
    /// action for get item category by id.
    /// </summary>
    /// <param name="id">id for item category</param>
    /// <returns>result of item category response dto</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemCategoryResponseDto>> GetCategoryByIdAsync(int id)
    {
        return await _itemCategoryService.GetCategoryByIdAsync(id);
    }

    /// <summary>
    /// action for add new item category that take item category request dto.
    /// </summary>
    /// <param name="categoryDto">item category request dto</param>
    /// <returns>result of an added item category successfully</returns>

    [HttpPost]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddCategoryAsync(ItemCategoryRequestDto categoryDto)
    {
        return await _itemCategoryService.AddCategoryAsync(categoryDto);
    }

    /// <summary>
    /// action for update an existing item category that take item category request dto and id item category.
    /// </summary>
    /// <param name="id">id of the item category</param>
    /// <returns>result of item category after updated</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result<ItemCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemCategoryResponseDto>> UpdateCategoryAsync(ItemCategoryRequestDto categoryDto, int id)
    {
        return await _itemCategoryService.UpdateCategoryAsync(id, categoryDto);
    }

    /// <summary>
    /// action for remove an item category that take item category id.
    /// </summary>
    /// <param name="id">id of the item category</param>
    /// <returns>result of an remove item category successfully</returns>

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteCategoryAsync(int id)
    {
        return await _itemCategoryService.DeleteCategoryAsync(id);
    }
}
