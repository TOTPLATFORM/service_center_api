using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
    public async Task<Result<List<ProductResponseDto>>> GetAllProductAsync()
    {
        var result = await _dbContext.Products
             .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Product. Total count: { Product}.", result.Count);

        return Result.Success(result);
    }
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


}
