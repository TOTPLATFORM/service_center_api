using ServiceCenter.Application.DTOS;
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
{/// <summary>
 /// function to add  item that take  ItemDto   
 /// </summary>
 /// <param name="itemRequestDto">item request dto</param>
 /// <returns> item added successfully </returns>
    public Task<Result> AddItemAsync(ItemRequestDto itemRequestDto);
    /// <summary>
    /// function to get all item 
    /// </summary>
    /// <returns>list all item response dto </returns>
    public Task<Result<List<ItemResponseDto>>> GetAllItemAsync();
    /// <summary>
    /// function to get  item by id that take   item id
    /// </summary>
    /// <param name="id"> item id</param>
    /// <returns> item response dto</returns>
    public Task<Result<ItemResponseDto>> GetItemByIdAsync(int id);
    /// <summary>
    /// function to update item that take ItemRequestDto   
    /// </summary>
    /// <param name="id">item id</param>
    /// <param name="itemRequestDto">item dto</param>
    /// <returns>Updated item </returns>
    public Task<Result<ItemResponseDto>> UpdateItemAsync(int id, ItemRequestDto itemRequestDto);
    /// <summary>
    /// function to delete Item that take item id   
    /// </summary>
    /// <param name="id">item id</param>
    /// <returns>Item removed successfully </returns>
    public Task<Result> DeleteItemAsync(int id);
    /// <summary>
    /// function to search by item name  that take  item name
    /// </summary>
    /// <param name="text">item name</param>
    /// <returns>Item response dto </returns>
    public Task<Result<List<ItemResponseDto>>> SearchItemByTextAsync(string text);
    /// <summary>
    /// Decreases items quantity asynchronously.
    /// </summary>
    /// <param name="orderedItems">Collection of The ordered items with ordered quantity.</param>
    /// <returns>The Result of the decrease items quantity attempt</returns>
    public  Task<Result> DecreaseItemsQuantity(ICollection<ItemOrderRequestDto> orderedItems);

    /// <summary>
    /// Increases items quantity asynchronously.
    /// </summary>
    /// <param name="orderedItems">Collection of The ordered items with ordered quantity.</param>
    /// <returns>The Result of the increase items quantity attempt</returns>
    public Task<Result> IncreaseItemsQuantity(ICollection<ItemOrderRequestDto> orderedItems);


}
