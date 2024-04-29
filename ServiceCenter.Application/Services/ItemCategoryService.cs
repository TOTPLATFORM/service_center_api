using ServiceCenter.Core.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;


namespace ServiceCenter.Application.Services;

public class ItemCategoryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ItemCategoryService> logger, IUserContextService userContext) : IItemCategoryService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ItemCategoryService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;



    /// <summary>
    /// Retrieves all categories asynchronously.
    /// </summary>
    /// <returns>A result containg list of item category response DTOs.</returns>
    public async Task<Result<List<ItemCategoryResponseDto>>> GetAllCategoriesAsync()
    {
        var categoriesResponseDto = await _dbContext.ItemCategories
            .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        _logger.LogInformation("Fetching all item categories. Total count: {ItemCategories}.", categoriesResponseDto.Count);
        return Result.Success(categoriesResponseDto);
    }

    /// <summary>
    /// Retrieves a category by its ID asynchronously.
    /// </summary>
    /// <param name="Id">The ID of the category to retrieve.</param>
    /// <returns>The Result containg item category response DTO, or NotFound Result.</returns>
    public async Task<Result<ItemCategoryResponseDto>> GetCategoryByIdAsync(int id)
    {
        var itemCategoryResponseDto = await _dbContext.ItemCategories
            .ProjectTo<ItemCategoryResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(category => category.Id == id);

        if (itemCategoryResponseDto is null)
        {
            _logger.LogWarning("Category Id not found,Id {id}", id);
            return Result.NotFound(["The category is not found"]);
        }

        _logger.LogInformation("Fetched one item category");
        return Result.Success(itemCategoryResponseDto);
    }

    /// <summary>
    /// Adds a new category asynchronously.
    /// </summary>
    /// <param name="itemCategoryDto">The DTO representing the category to add.</param>
    /// <returns>Result of the add attempt.</returns>
    public async Task<Result> AddCategoryAsync(ItemCategoryRequestDto itemCategoryDto)
    {
        var category = _mapper.Map<ItemCategory>(itemCategoryDto);

        if (category is null)
        {
            _logger.LogError("Failed to map ItemCategoryDto to ItemCategory. ItemCategoryDto: {@ItemCategoryDto}", itemCategoryDto);
            return Result.Invalid(new List<ValidationError>
                {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
                });
        }
        category.CreatedBy = _userContext.Email;
        _dbContext.ItemCategories.Add(category);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("ItemCategory added successfully to the database");
        return Result.SuccessWithMessage("Item Category added successfully");
    }

    /// <summary>
    /// Updates an existing category asynchronously.
    /// </summary>
    /// <param name="itemCategoryDto">The DTO representing the category to update.</param>
    /// <param name="id">The ID of the category to update.</param>
    /// <returns>The Result of the update attempt.</returns>
    public async Task<Result<ItemCategoryResponseDto>> UpdateCategoryAsync(int id,ItemCategoryRequestDto itemCategoryDto)
    {
        var itemCategory = await _dbContext.ItemCategories.FindAsync(id);

        if (itemCategory is null)
        {
            _logger.LogWarning("category id not found,Id {categoryId}", id);
            return Result.NotFound(["The category is not found"]);
        }

        itemCategory.ModifiedBy = _userContext.Email;
        _mapper.Map(itemCategoryDto, itemCategory);

        await _dbContext.SaveChangesAsync();

        var updatedItemCategory = _mapper.Map<ItemCategoryResponseDto>(itemCategory);

        _logger.LogInformation("ItemCategory updated successfully");
        return Result.Success(updatedItemCategory, "Successfully updated item category");
    }

    /// <summary>
    /// Deletes a category asynchronously.
    /// </summary>
    /// <param name="Id">The ID of the category to delete.</param>
    /// <returns>The Result of the delete attempt</returns>
    public async Task<Result> DeleteCategoryAsync(int id)
    {
        var category = await _dbContext.ItemCategories.FindAsync(id);

        if (category is null)
        {
            _logger.LogWarning("category id not found,Id {categoryId}", id);
            return Result.NotFound(["The category is not found"]);
        }

        _dbContext.ItemCategories.Remove(category);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("laboratorist remove successfully in the database");
        return Result.SuccessWithMessage("category remove successfully");
    }
}
