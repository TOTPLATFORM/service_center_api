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
    /// function to add  item category that take  item category Dto   
    /// </summary>
    /// <param name="itemCategoryRequestDto">item category request dto</param>
    /// <returns> ItemCategory added successfully </returns>
    public Task<Result> AddItemCategoryAsync(ItemCategoryRequestDto itemCategoryRequestDto);
    /// <summary>
    /// function to get all item category 
    /// </summary>
    /// <returns>list all item category response dto </returns>
    public Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemCategoryAsync(int itemCount, int index);
    /// <summary>
    /// function to get  item category by id that take   item category id
    /// </summary>
    /// <param name="id"> item category id</param>
    /// <returns> item category response dto</returns>
    public Task<Result<ItemCategoryResponseDto>> GetItemCategoryByIdAsync(int id);
    /// <summary>
    /// function to update item category that take item category request dto   
    /// </summary>
    /// <param name="id">item category id</param>
    /// <param name="ItemCategoryRequestDto">item category dto</param>
    /// <returns>Updated ItemCategory </returns>
    public Task<Result<ItemCategoryResponseDto>> UpdateItemCategoryAsync(int id, ItemCategoryRequestDto ItemCategoryRequestDto);
    /// <summary>
    /// function to delete item category that take item category id   
    /// </summary>
    /// <param name="id">item category id</param>
    /// <returns>item category removed successfully </returns>
    public Task<Result> DeleteItemCategoryAsync(int id);
    /// <summary>
    /// function to search by item category name  that take  item category name
    /// </summary>
    /// <param name="text">item category name</param>
    /// <returns>item category response dto </returns>
    public Task<Result<PaginationResult<ItemCategoryResponseDto>>> SearchItemCategoryByTextAsync(string text, int itemCount, int index);
    /// <summary>
    /// function to search by inventory  that take  inventory name
    /// </summary>
    /// <param name="inventoryId">inventory name</param>
    /// <returns>item category response dto </returns
    public Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemsCategoryForSpecificInventory(int inventoryId, int itemCount, int index);
}
