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

public class ProductService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ProductService> logger, IUserContextService userContext) : IProductService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ProductService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddProductAsync(ProductRequestDto productRequestDto)
    {
        var result = _mapper.Map<Product>(productRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ProductRequestDto to Product. ProductRequestDto: {@ProductRequestDto}", productRequestDto);
            return Result.Invalid(new List<ValidationError>
        {
            new ValidationError
            {
                ErrorMessage = "Validation Errror"
            }
        });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Products.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Product added successfully to the database");
        return Result.SuccessWithMessage("Product added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ProductResponseDto>>> GetAllProductAsync(int itemCount, int index)
    {
        var result = await _dbContext.Products
             .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  Product. Total count: { Product}.", result.Data.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ProductResponseDto>> GetProductByIdAsync(int id)
    {
        var result = await _dbContext.Products
            .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Product Id not found,Id {ProductId}", id);

            return Result.NotFound(["Product not found"]);
        }

        _logger.LogInformation("Fetching Product");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ProductResponseDto>> UpdateProductAsync(int id, ProductRequestDto productRequestDto)
    {
        var result = await _dbContext.Products.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Product Id not found,Id {ProductId}", id);
            return Result.NotFound(["Product not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(productRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ProductResponse = _mapper.Map<ProductResponseDto>(result);
        if (ProductResponse is null)
        {
            _logger.LogError("Failed to map ProductRequestDto to ProductResponseDto. ProductRequestDto: {@ProductRequestDto}", ProductResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Product , Id {Id}", id);

        return Result.Success(ProductResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteProductAsync(int id)
    {
        var Product = await _dbContext.Products.FindAsync(id);

        if (Product is null)
        {
            _logger.LogWarning("Product Invaild Id ,Id {ProductId}", id);
            return Result.NotFound(["Product Invaild Id"]);
        }

        _dbContext.Products.Remove(Product);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Product removed successfully in the database");
        return Result.SuccessWithMessage("Product removed successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ProductResponseDto>>> SearchProductByTextAsync(string text, int itemCount, int index)
    {
        var names = await _dbContext.Products
        .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
        .Where(n => n.ProductName.Contains(text))
        .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching search Product by name . Total count: {Prouct}.", names.Data.Count);
        return Result.Success(names);
    }

    //public async Task<Result<List<ProductResponseDto>>> GetProductsForProductCategoryAsync(int categoryId)
    //{
    //    var products = await _dbContext.Products
    //          .Where(s => s.ProductCategories.Id == categoryId)
    //          .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
    //          .ToListAsync();

    //    _logger.LogInformation("Fetching products. Total count: {products}.", products.Count);
    //    return Result.Success(products);
    //}

    //public async Task<Result<List<ProductResponseDto>>> GetProductsForProductBrandAsync(int brandId)
    //{
    //    var products = await _dbContext.Products
    //          .Where(s => s.ProductBrand.Id == brandId)
    //          .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
    //          .ToListAsync();

    //    _logger.LogInformation("Fetching products. Total count: {products}.", products.Count);
    //    return Result.Success(products);
    //}

}
