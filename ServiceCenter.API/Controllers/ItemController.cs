using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServiceCenter.API.Controllers;

public class ItemController(IItemService itemService) : BaseController
{
    private readonly IItemService _itemService = itemService;

    /// <summary>
    /// action for get all items.
    /// </summary>
    /// <returns>result of list from item response dtos</returns>
    [HttpGet]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result<List<ItemResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ItemResponseDto>>> GetAllItemsAsync()
    {
        return await _itemService.GetAllItemsAsync();
    }

    /// <summary>
    /// action for get an item by id.
    /// </summary>
    /// <param name="id">id for item </param>
    /// <returns>a result of item response dto</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemResponseDto>> GetItemByIdAsync(int id)
    {
        return await _itemService.GetItemByIdAsync(id);
    }

    /// <summary>
    /// action for add new item that take item request dto.
    /// </summary>
    /// <param name="itemDto"> item request dto </param>
    /// <returns>result of an added item successfully</returns>
    [HttpPost]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddItemAsync(ItemRequestDto itemDto)
    {
        return await _itemService.AddItemAsync(itemDto);
    }

    /// <summary>
    /// action for update an existing item that take item request dto and item id.
    /// </summary>
    /// <param name="itemDto">item request dto</param>
    /// <param name="id">id of the item to update</param>
    /// <returns>result of item response dto after updated</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemResponseDto>> UpdateItemAsync(ItemRequestDto itemDto, int id)
    {
        return await _itemService.UpdateItemAsync(id,itemDto);
    }

    /// <summary>
    /// action for delete an item that take item id.
    /// </summary>
    /// <param name="id">id of the item</param>
    /// <returns>result of an remove item successfully</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin , Employee")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteItemAsync(int id)
    {
        return await _itemService.DeleteItemAsync(id);
    }
}
