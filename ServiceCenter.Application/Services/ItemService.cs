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

    ///<inheritdoc/>
    public async Task<Result> AddItemAsync(ItemRequestDto ItemRequestDto)
    {
        var result = _mapper.Map<Item>(ItemRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ItemRequestDto to Item. ItemRequestDto: {@ItemRequestDto}", ItemRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }
        result.CreatedBy = _userContext.Email;
        _dbContext.Items.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Item added successfully to the database");
        return Result.SuccessWithMessage("Item added successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<List<ItemResponseDto>>> GetAllItemAsync()
    {
        var result = await _dbContext.Items
                 .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();

        _logger.LogInformation("Fetching all  Item. Total count: { Item}.", result.Count);

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<ItemResponseDto>> GetItemByIdAsync(int id)
    {
        var result = await _dbContext.Items
                .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(Item => Item.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Item Id not found,Id {ItemId}", id);

            return Result.NotFound(["Item not found"]);
        }

        _logger.LogInformation("Fetching Item");

        return Result.Success(result);
    }
    //<inheritdoc/>
    public async Task<Result<ItemResponseDto>> UpdateItemAsync(int id, ItemRequestDto ItemRequestDto)
    {
        var result = await _dbContext.Items.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Item Id not found,Id {ItemId}", id);
            return Result.NotFound(["Item not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ItemRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ItemResponse = _mapper.Map<ItemResponseDto>(result);
        if (ItemResponse is null)
        {
            _logger.LogError("Failed to map ItemRequestDto to ItemResponseDto. ItemRequestDto: {@ItemRequestDto}", ItemResponse);

            return Result.Invalid(new List<ValidationError>
            {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }

        _logger.LogInformation("Updated Item , Id {Id}", id);

        return Result.Success(ItemResponse);
    }
    //<inheritdoc/>
    public async Task<Result> DeleteItemAsync(int id)
    {
        var Item = await _dbContext.Items.FindAsync(id);

        if (Item is null)
        {
            _logger.LogWarning("Item Invaild Id ,Id {ItemId}", id);
            return Result.NotFound(["Item Invaild Id"]);
        }

        _dbContext.Items.Remove(Item);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Item removed successfully in the database");
        return Result.SuccessWithMessage("Item removed successfully");
    }
    //<inheritdoc/>

    public async Task<Result<List<ItemResponseDto>>> SearchItemByTextAsync(string text)
    {
        var names = await _dbContext.Items
            .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.ItemName.Contains(text))
            .ToListAsync();
        _logger.LogInformation("Fetching search Item by name . Total count: {Item}.", names.Count);
        return Result.Success(names);
    }

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
                return Result.Error([$"{item.ItemName} does not have sufficient quantity available in stock"]);
            }
        }

        return Result.Success();
    }

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
}
