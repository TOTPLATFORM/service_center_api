using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ProductCategoryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ProductCategoryService> logger, IUserContextService userContext) : IProductCategoryService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ProductCategoryService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddProductCategoryAsync(ProductCategoryRequestDto productCategoryRequestDto)
    {
        var result = _mapper.Map<ProductCategory>(productCategoryRequestDto);
        result.CreatedBy = _userContext.Email;
        _dbContext.ProductCategories.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ProductCategory added successfully to the database");
        return Result.SuccessWithMessage("ProductCategory added successfully");
    }

  
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ProductCategoryResponseDto>>> GetAllProductCategoryAsync(int itemCount, int index)
    {
        var result = await _dbContext.ProductCategories
             .ProjectTo<ProductCategoryResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount, index);
        _logger.LogInformation("Fetching all  ProductCategory. Total count: { ProductCategory}.", result.Data.Count);

        return Result.Success(result);
    }
  

    ///<inheritdoc/>
    public async  Task<Result<ProductCategoryResponseDto>> GetProductCategoryByIdAsync(int id)
    {
        var result = await _dbContext.ProductCategories
            .ProjectTo<ProductCategoryResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("ProductCategory Id not found,Id {ProductCategoryId}", id);

            return Result.NotFound(["ProductCategory not found"]);
        }

        _logger.LogInformation("Fetching ProductCategory");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ProductCategoryResponseDto>> UpdateProductCategoryAsync(int id, ProductCategoryRequestDto productCategoryRequestDto)
    {
        var result = await _dbContext.ProductCategories.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("ProductCategory Id not found,Id {ProductCategoryId}", id);
            return Result.NotFound(["ProductCategory not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(productCategoryRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ProductCategoryResponse = _mapper.Map<ProductCategoryResponseDto>(result);

        _logger.LogInformation("Updated ProductCategory , Id {Id}", id);

        return Result.Success(ProductCategoryResponse);
    }



    ///<inheritdoc/>
    public async Task<Result> DeleteProductCategoryAsync(int id)
    {
        var ProductCategory = await _dbContext.ProductCategories.FindAsync(id);

        if (ProductCategory is null)
        {
            _logger.LogWarning("ProductCategory Invaild Id ,Id {ProductCategoryId}", id);
            return Result.NotFound(["ProductCategory Invaild Id"]);
        }

        _dbContext.ProductCategories.Remove(ProductCategory);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ProductCategory removed successfully in the database");
        return Result.SuccessWithMessage("ProductCategory removed successfully");
    }

    ///<inheritdoc/>
    public async  Task<Result<PaginationResult<ProductCategoryResponseDto>>> SearchProductCategoryByTextAsync(string text,int itemCount,int index)
    {
        var names = await _dbContext.ProductCategories
        .ProjectTo<ProductCategoryResponseDto>(_mapper.ConfigurationProvider)
        .Where(n => n.CategoryName.Contains(text))
        .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching search ProductCategory by name . Total count: {ProductCategory}.", names.Data.Count);
        return Result.Success(names);
    }
    public async Task<Result<List<ProductCategoryResponseDto>>> AssignProductCategoryToProductBrandAsync(int productCategoryId, int productBrandId)
    {
        var productCategory = await _dbContext.ProductCategories.FindAsync(productCategoryId);

        if (productCategory is null)
        {
            _logger.LogWarning("ProductCategoryId Id not found,Id {id}", productCategoryId);

            return Result.NotFound(["The ProductCategory is not found"]);
        }

        var productBrand = await _dbContext.ProductBrands.FindAsync(productBrandId);

        if (productBrand is null)
        {
            _logger.LogWarning("productBrand Id not found,Id {id}", productBrandId);

            return Result.NotFound(["The productBrand is not found"]);
        }

        if (!productBrand.ProductCategories.Any(pc => pc.Id == productCategoryId))
        {
            productBrand.ProductCategories.Add(productCategory);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Successfully assigned productCategory to productBrand");
        }
        else
        {
            _logger.LogInformation("ProductCategory already assigned to productBrand");
        }

        return Result.SuccessWithMessage("productCategory added successfully to productBrand");

    }
}
