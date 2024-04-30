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
        var result = _mapper.Map<ItemCategory>(ItemCategoryRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ItemCategoryRequestDto to ItemCategory. ItemCategoryRequestDto: {@ItemCategoryRequestDto}", ItemCategoryRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }
        result.CreatedBy = _userContext.Email;
        _dbContext.ItemCategories.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ItemCategory added successfully to the database");
        return Result.SuccessWithMessage("ItemCategory added successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<List<ItemCategoryResponseDto>>> GetAllItemCategoryAsync()
    {
        var result = await _dbContext.ItemCategories
                 .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();

        _logger.LogInformation("Fetching all  ItemCategory. Total count: { ItemCategory}.", result.Count);

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
        if (ItemCategoryResponse is null)
        {
            _logger.LogError("Failed to map ItemCategoryRequestDto to ItemCategoryResponseDto. ItemCategoryRequestDto: {@ItemCategoryRequestDto}", ItemCategoryResponse);

            return Result.Invalid(new List<ValidationError>
            {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }

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

    public async Task<Result<List<ItemCategoryResponseDto>>> SearchItemCategoryByTextAsync(string text)
    {
        var names = await _dbContext.ItemCategories
            .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.CategoryName.Contains(text))
            .ToListAsync();
        _logger.LogInformation("Fetching search ItemCategory by name . Total count: {ItemCategory}.", names.Count);
        return Result.Success(names);
    }
    //<inheritdoc/>

    public async Task<Result<List<ItemCategoryResponseDto>>> GetAllItemsCategoryForSpecificInventory(string inventoryName)
    {
        var items = await _dbContext.ItemCategories
            .Where(s => s.Inventory.InventoryName == inventoryName)
            .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        _logger.LogInformation("Fetching items. Total count: {items}.", items.Count);
        return Result.Success(items);
    }
}
