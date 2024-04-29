using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IItemService : IApplicationService, IScopedService
{
    Task<Result<List<ItemResponseDto>>> GetAllItemsAsync();
    Task<Result<ItemResponseDto>> GetItemByIdAsync(int id);
    Task<Result> AddItemAsync(ItemRequestDto itemRequestDto);
    Task<Result<ItemResponseDto>> UpdateItemAsync(int id,ItemRequestDto itemRequestDto);
    Task<Result> DeleteItemAsync(int id);
    Task<Result> IncreaseItemsQuantity(ICollection<ItemOrderRequestDto> orderedItems);
    Task<Result> DecreaseItemsQuantity(ICollection<ItemOrderRequestDto> orderedItems);
}
