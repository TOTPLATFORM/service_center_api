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

namespace ServiceCenter.Application.Services
{
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

        public async Task<Result<List< ProductBrandResponseDto>>> GetAllProductBrandAsync()
        {
            var result = await _dbContext.ProductBrands
                     .ProjectTo<ProductBrandResponseDto>(_mapper.ConfigurationProvider)
                     .ToListAsync();

            _logger.LogInformation("Fetching all  productBrand. Total count: { productBrand}.", result.Count);

            return Result.Success(result);
        }




    }
}
