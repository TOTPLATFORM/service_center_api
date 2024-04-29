using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IItemCategoryService : IApplicationService, IScopedService
{
    public Task<Result<List<ItemCategoryResponseDto>>> GetAllCategoriesAsync();

    public Task<Result<ItemCategoryResponseDto>> GetCategoryByIdAsync(int id);

    public Task<Result> AddCategoryAsync(ItemCategoryRequestDto itemCategoryRequestDto);

    public Task<Result<ItemCategoryResponseDto>> UpdateCategoryAsync(int id, ItemCategoryRequestDto itemCategoryRequestDto);

    public Task<Result> DeleteCategoryAsync(int id);
}
