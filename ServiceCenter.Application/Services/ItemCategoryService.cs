using ServiceCenter.Core.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using ServiceCenter.Core.Entities;
using ServiceCenter.Application.ExtensionForServices;


namespace ServiceCenter.Application.Services;

public class ItemCategoryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ItemCategoryService> logger, IUserContextService userContext) : IItemCategoryService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ItemCategoryService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddItemCategoryAsync(ItemCategoryRequestDto ItemCategoryRequestDto)
    {
        var inventory = new List<Inventory>();
        foreach (var item in ItemCategoryRequestDto.inventoryIds)
        {
            inventory.Add(await _dbContext.Inventories.FirstOrDefaultAsync(i => i.Id == item));
        } 
       
        var result = _mapper.Map<ItemCategory>(ItemCategoryRequestDto);
        result.Inventories = inventory;
        result.CreatedBy = _userContext.Email;
        _dbContext.ItemCategories.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ItemCategory added successfully to the database");
        return Result.SuccessWithMessage("ItemCategory added successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemCategoryAsync(int itemCount, int index)
    {
        var result = await _dbContext.ItemCategories
                 .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
                 .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  ItemCategory. Total count: { ItemCategory}.", result.Data.Count);

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<ItemCategoryResponseDto>> GetItemCategoryByIdAsync(int id)
    {
        var result = await _dbContext.ItemCategories
                .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ItemCategory => ItemCategory.Id == id);

        if (result is null)
        {
            _logger.LogWarning("ItemCategory Id not found,Id {ItemCategoryId}", id);

            return Result.NotFound(["ItemCategory not found"]);
        }

        _logger.LogInformation("Fetching ItemCategory");

        return Result.Success(result);
    }
    //<inheritdoc/>
    public async Task<Result<ItemCategoryResponseDto>> UpdateItemCategoryAsync(int id, ItemCategoryRequestDto ItemCategoryRequestDto)
    {
        var result = await _dbContext.ItemCategories.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("ItemCategory Id not found,Id {ItemCategoryId}", id);
            return Result.NotFound(["ItemCategory not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ItemCategoryRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ItemCategoryResponse = _mapper.Map<ItemCategoryResponseDto>(result);

        _logger.LogInformation("Updated ItemCategory , Id {Id}", id);

        return Result.Success(ItemCategoryResponse);
    }
    //<inheritdoc/>
    public async Task<Result> DeleteItemCategoryAsync(int id)
    {
        var ItemCategory = await _dbContext.ItemCategories.FindAsync(id);

        if (ItemCategory is null)
        {
            _logger.LogWarning("ItemCategory Invaild Id ,Id {ItemCategoryId}", id);
            return Result.NotFound(["ItemCategory Invaild Id"]);
        }

        _dbContext.ItemCategories.Remove(ItemCategory);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ItemCategory removed successfully in the database");
        return Result.SuccessWithMessage("ItemCategory removed successfully");
    }
    //<inheritdoc/>

    public async Task<Result<PaginationResult<ItemCategoryResponseDto>>> SearchItemCategoryByTextAsync(string text, int itemCount, int index)
    {
        var names = await _dbContext.ItemCategories
            .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.CategoryName.Contains(text))
            .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching search ItemCategory by name . Total count: {ItemCategory}.", names.Data.Count);
        return Result.Success(names);
    }
    //<inheritdoc/>

    public async Task<Result<PaginationResult<ItemCategoryResponseDto>>> GetAllItemsCategoryForSpecificInventory(int inventoryId, int itemCount, int index)
    {
        var items = await _dbContext.ItemCategories
            .Where(s => s.Inventories.Select(i => i.Id).FirstOrDefault()== inventoryId)
            .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching items. Total count: {items}.", items.Data.Count);
        return Result.Success(items);
    }
}
