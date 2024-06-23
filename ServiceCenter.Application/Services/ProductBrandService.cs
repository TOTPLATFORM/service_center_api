using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ProductBrandService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ProductBrandService> logger, IUserContextService userContext) : IProductBrandService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ProductBrandService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddProductBrandAsync(ProductBrandRequestDto productBrandRequestDto)
    {
        var result = _mapper.Map<ProductBrand>(productBrandRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ProductBrandRequestDto to ProductBrand. ProductBrandRequestDto: {@ProductBrandRequestDto}", productBrandRequestDto);
            return Result.Invalid(new List<ValidationError>
        {
            new ValidationError
            {
                ErrorMessage = "Validation Errror"
            }
        });
        }
        result.CreatedBy = _userContext.Email;
        _dbContext.ProductBrands.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ProductBrand added successfully to the database");
        return Result.SuccessWithMessage("ProductBrand added successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<PaginationResult< ProductBrandResponseDto>>> GetAllProductBrandAsync(int itemCount, int index)
    {
        var result = await _dbContext.ProductBrands
                 .ProjectTo<ProductBrandResponseDto>(_mapper.ConfigurationProvider)
                 .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching all  productBrand. Total count: { productBrand}.", result.Data.Count);

        return Result.Success(result);
    }
 

    ///<inheritdoc/>
    public async Task<Result<ProductBrandResponseDto>> GetProductBrandByIdAsync(int id)
    {
        var result = await _dbContext.ProductBrands
                .ProjectTo<ProductBrandResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ProductBrand => ProductBrand.Id == id);

        if (result is null)
        {
            _logger.LogWarning("ProductBrand Id not found,Id {ProductBrandId}", id);

            return Result.NotFound(["ProductBrand not found"]);
        }

        _logger.LogInformation("Fetching ProductBrand");

        return Result.Success(result);
    }
    //<inheritdoc/>
    public async Task<Result<ProductBrandResponseDto>> UpdateProductBrandAsync(int id, ProductBrandRequestDto ProductBrandRequestDto)
    {
        var result = await _dbContext.ProductBrands.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("ProductBrand Id not found,Id {ProductBrandId}", id);
            return Result.NotFound(["ProductBrand not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ProductBrandRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ProductBrandResponse = _mapper.Map<ProductBrandResponseDto>(result);
        if (ProductBrandResponse is null)
        {
            _logger.LogError("Failed to map ProductBrandRequestDto to ProductBrandResponseDto. ProductBrandRequestDto: {@ProductBrandRequestDto}", ProductBrandResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated ProductBrand , Id {Id}", id);

        return Result.Success(ProductBrandResponse);
    }
    //<inheritdoc/>
    public async Task<Result> DeleteProductBrandAsync(int id)
    {
        var ProductBrand = await _dbContext.ProductBrands.FindAsync(id);

        if (ProductBrand is null)
        {
            _logger.LogWarning("ProductBrand Invaild Id ,Id {ProductBrandId}", id);
            return Result.NotFound(["ProductBrand Invaild Id"]);
        }

        _dbContext.ProductBrands.Remove(ProductBrand);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ProductBrand removed successfully in the database");
        return Result.SuccessWithMessage("ProductBrand removed successfully");
    }
    //<inheritdoc/>

    public async Task<Result<PaginationResult<ProductBrandResponseDto>>> SearchProductBrandByTextAsync(string text,int itemCount,int index)
    {
        var names = await _dbContext.ProductBrands
            .ProjectTo<ProductBrandResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.BrandName.Contains(text))
            .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching search ProductBrand by name . Total count: {ProductBrand}.", names.Data.Count);
        return Result.Success(names);
    }
    public async Task<Result<List<ProductBrandResponseDto>>> AssignProductBrandToInventoryAsync(int inventoryId, int productBrandId)
    {
        var inventory = await _dbContext.Inventories.FindAsync(inventoryId);

        if (inventory is null)
        {
            _logger.LogWarning("InventoryId Id not found,Id {id}", inventoryId);

            return Result.NotFound(["The Facility is not found"]);
        }

        var productBrand = await _dbContext.ProductBrands.FindAsync(productBrandId);

        if (productBrand is null)
        {
            _logger.LogWarning("Property Id not found,Id {id}", productBrandId);

            return Result.NotFound(["The Property is not found"]);
        }

        inventory.ProductBrands.Add(productBrand);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Successfully assigned productBrand to inventory");

        return Result.SuccessWithMessage("productBrand added successfully to inventory");

    }

}
