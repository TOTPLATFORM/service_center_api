using ServiceCenter.Core.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Logging;
using ServiceCenter.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;

namespace ServiceCenter.Application.Services;
public class ItemService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ItemService> logger, IUserContextService userContext) : IItemService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ItemService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;



    /// <summary>
    /// Gets all items asynchronously.
    /// </summary>
    /// <returns>A Result containing item response DTOs.</returns>
    public async Task<Result<List<ItemResponseDto>>> GetAllItemsAsync()
    {
        var itemsResponseDto = await _dbContext.Items
            .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        _logger.LogInformation("Fetching all items. Total count: {itemsResponseDto}.", itemsResponseDto.Count);
        return Result.Success(itemsResponseDto);
    }

    /// <summary>
    /// Gets an item by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the item to retrieve.</param>
    /// <returns>A Result containing item response DTO or NotFound Result if item is not found.</returns>
    public async Task<Result<ItemResponseDto>> GetItemByIdAsync(int id)
    {
        var itemResponseDto = await _dbContext.Items
            .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (itemResponseDto is null)
        {
            _logger.LogWarning("Item Id not found,Id {id}", id);
            return Result.NotFound(["The item is not found"]);
        }

        _logger.LogInformation("Fetched one item");
        return Result.Success(itemResponseDto);
    }

    /// <summary>
    /// Adds a new item asynchronously.
    /// </summary>
    /// <param name="itemRequestDto">The DTO representing the item to add.</param>
    /// <returns>Result of the add attempt.</returns>
    public async Task<Result> AddItemAsync(ItemRequestDto itemRequestDto)
    {
        var item = _mapper.Map<Item>(itemRequestDto);

        if (item is null)
        {
            _logger.LogError("Failed to map ItemRequestDto to Item. ItemCategoryDto: {@ItemCategoryDto}", itemRequestDto);
            return Result.Invalid(new List<ValidationError>
                {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
                });
        }
        item.CreatedBy = _userContext.Email;

        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Item added successfully to the database");
        return Result.SuccessWithMessage("Item added successfully");
    }

    /// <summary>
    /// Updates an existing item asynchronously.
    /// </summary>
    /// <param name="itemRequestDto">The DTO representing the item to update.</param>
    /// <param name="id">The ID of the item to update.</param>
    /// <returns>The Result of the update attempt.</returns>
    public async Task<Result<ItemResponseDto>> UpdateItemAsync(int id,ItemRequestDto itemRequestDto)
    {
        var item = await _dbContext.Items.FindAsync(id);

        if (item is null)
        {
            _logger.LogWarning("Item Id not found,Id {id}", id);
            return Result.NotFound(["The item is not found"]);
        }
        item.ModifiedBy = _userContext.Email;
        _mapper.Map(itemRequestDto, item);

        await _dbContext.SaveChangesAsync();

        var updatedItem = _mapper.Map<ItemResponseDto>(item);

        _logger.LogInformation("ItemCategory updated successfully");
        return Result.Success(updatedItem, "Laboratorist updated successfully");
    }

    /// <summary>
    /// Deletes an item asynchronously.
    /// </summary>
    /// <param name="Id">The ID of the item to delete.</param>
    /// <returns>The Result of the delete attempt</returns>
    public async Task<Result> DeleteItemAsync(int id)
    {
        var item = await _dbContext.Items.FindAsync(id);

        if (item is null)
        {
            _logger.LogWarning($"Item with id {id} was not found while attempting to delete");
            return Result.NotFound(["The item is not found"]);
        }

        _dbContext.Items.Remove(item);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfulle removed item {item}");
        return Result.SuccessWithMessage("Item removed successfully");
    }

    /// <summary>
    /// Increases items quantity asynchronously.
    /// </summary>
    /// <param name="orderedItems">Collection of The ordered items with ordered quantity.</param>
    /// <returns>The Result of the increase items quantity attempt</returns>
    public async Task<Result> IncreaseItemsQuantity(ICollection<ItemOrderRequestDto> orderedItems)
    {
        var itemsIds = orderedItems.Select(item => item.ItemId).ToList();

        var items = await _dbContext.Items.Where(item => itemsIds.Contains(item.Id)).ToListAsync();

        foreach (var item in items)
        {
            var stockLevel = item.ItemStock;
            var quantity = orderedItems.Where(orderedItem => orderedItem.ItemId == item.Id).Select(orderedItem => orderedItem.Quantity).Single();
            item.ItemStock += quantity;
        }

        return Result.Success();
    }

    /// <summary>
    /// Decreases items quantity asynchronously.
    /// </summary>
    /// <param name="orderedItems">Collection of The ordered items with ordered quantity.</param>
    /// <returns>The Result of the decrease items quantity attempt</returns>
    public async Task<Result> DecreaseItemsQuantity(ICollection<ItemOrderRequestDto> orderedItems)
    {
        var itemsIds = orderedItems.Select(item => item.ItemId).ToList();

        var items = await _dbContext.Items.Where(item => itemsIds.Contains(item.Id)).ToListAsync();

        foreach (var item in items)
        {
            var stockLevel = item.ItemStock;
            var quantity = orderedItems.Where(orderedItem => orderedItem.ItemId == item.Id).Select(orderedItem => orderedItem.Quantity).Single();

            item.ItemStock -= quantity;

            if (item.ItemStock < 0)
            {
                _logger.LogWarning($"item with id {item.Id} does not have sufficient quantity available in stock, only {stockLevel} available");
                return Result.Error([$"{item.ItemStock} does not have sufficient quantity available in stock"]);
            }
        }

        return Result.Success();
    }
}
