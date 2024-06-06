﻿using ServiceCenter.Application.Contracts;
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
    /// <returns>result for item added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "WarehouseManager,Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddItem(ItemRequestDto itemDto)
    {
        return await _ItemService.AddItemAsync(itemDto);
    }

    [HttpGet]
    [Authorize(Roles = "ServiceProvider,Manager,Admin,WarehouseManager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ItemResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ItemResponseDto>>> GetAllItems(int itemCount,int index)
    {
        return await _ItemService.GetAllItemAsync(itemCount,index);
    }
    /// <summary>
    /// action for get item by id that take item id.  
    /// </summary>
    /// <returns>result of item response dto</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ItemResponseDto>> GetItemById(int id)
    {
        return await _ItemService.GetItemByIdAsync(id);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ItemResponseDto>> UpdateItem(int id, ItemRequestDto itemDto)
    {
        return await _ItemService.UpdateItemAsync(id, itemDto);
    }
    /// <summary>
    ///  action for remove item that take item id   
    /// </summary>
    /// <param name="id">item id</param>
    /// <returns>result of item removed successfully </returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "WarehouseManager,Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteItem(int id)
    {
        return await _ItemService.DeleteItemAsync(id);
    }

    /// <summary>
    /// function to search by Item name  that take  Item name
    /// </summary>
    /// <param name="text">Item name</param>
    /// <param name="itemCount">item count in page</param>
    /// <param name="index">Item index</param>
    /// <returns>Item response dto </returns>

    [HttpGet("search")]
    [Authorize(Roles = "ServiceProvider,Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ItemResponseDto>>> SearchItemByText(string text,int itemCount,int index)
    {
        return await _ItemService.SearchItemByTextAsync(text,itemCount,index);
    }



}