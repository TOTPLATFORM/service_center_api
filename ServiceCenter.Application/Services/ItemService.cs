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
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;

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
        var category = await _dbContext.ItemCategories.FirstOrDefaultAsync(i => i.Id == ItemRequestDto.CategoryId);
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
        result.Category = category;
        result.CreatedBy = _userContext.Email;
        _dbContext.Items.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Item added successfully to the database");
        return Result.SuccessWithMessage("Item added successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<PaginationResult<ItemResponseDto>>> GetAllItemAsync(int itemCount,int index)
    {
        var result = await _dbContext.Items
                 .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
                 .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  Item. Total count: { Item}.", result.Data.Count);

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
        var category = await _dbContext.ItemCategories.FirstOrDefaultAsync(i => i.Id == ItemRequestDto.CategoryId);
        if (result is null)
        {
            _logger.LogWarning("Item Id not found,Id {ItemId}", id);
            return Result.NotFound(["Item not found"]);
        }

        result.ModifiedBy = _userContext.Email;
        result.Category = category;
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

    public async Task<Result<PaginationResult<ItemResponseDto>>> SearchItemByTextAsync(string text,int itemCount,int index)
    {
        var names = await _dbContext.Items
            .ProjectTo<ItemResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.ItemName.Contains(text))
            .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching search Item by name . Total count: {Item}.", names.Data.Count);
        return Result.Success(names);
    }

    
}
