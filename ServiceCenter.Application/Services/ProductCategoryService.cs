using AutoMapper;
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
            if (result is null)
            {
                _logger.LogError("Failed to map ProductCategoryRequestDto to ProductCategory. ProductCategoryRequestDto: {@ProductCategoryRequestDto}", productCategoryRequestDto);
                return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
            }
            result.CreatedBy = _userContext.Email;
            _dbContext.ProductCategories.Add(result);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("ProductCategory added successfully to the database");
            return Result.SuccessWithMessage("ProductCategory added successfully");
        }
       
    
    }
}
