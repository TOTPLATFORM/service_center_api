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

public class ServiceCategoryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ServiceCategoryService> logger, IUserContextService userContext) : IServiceCategoryService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ServiceCategoryService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddServiceCategoryAsync(ServiceCategoryRequestDto ServiceCategoryRequestDto)
    {
        var result = _mapper.Map<ServiceCategory>(ServiceCategoryRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ServiceCategoryRequestDto to ServiceCategory. ServiceCategoryRequestDto: {@ServiceCategoryRequestDto}", ServiceCategoryRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }
        result.CreatedBy = _userContext.Email;
        _dbContext.ServiceCategories.Add(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ServiceCategory added successfully to the database");
        return Result.SuccessWithMessage("ServiceCategory added successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<PaginationResult<ServiceCategoryResponseDto>>> GetAllServiceCategoryAsync(int itemCount, int index)
    {
        var result = await _dbContext.ServiceCategories
                 .ProjectTo<ServiceCategoryResponseDto>(_mapper.ConfigurationProvider)
                 .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  ServiceCategory. Total count: { ServiceCategory}.", result.Data.Count);

		return Result.Success(result);
	}

	///<inheritdoc/>
	public async Task<Result<ServiceCategoryGetByIdResponseDto>> GetServiceCategoryByIdAsync(int id)
    {
        var result = await _dbContext.ServiceCategories
                .ProjectTo<ServiceCategoryGetByIdResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ServiceCategory => ServiceCategory.Id == id);

        if (result is null)
        {
            _logger.LogWarning("ServiceCategory Id not found,Id {ServiceCategoryId}", id);

            return Result.NotFound(["ServiceCategory not found"]);
        }

        _logger.LogInformation("Fetching ServiceCategory");

        return Result.Success(result);
    }
    //<inheritdoc/>
    public async Task<Result<ServiceCategoryResponseDto>> UpdateServiceCategoryAsync(int id, ServiceCategoryRequestDto ServiceCategoryRequestDto)
    {
        var result = await _dbContext.ServiceCategories.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("ServiceCategory Id not found,Id {ServiceCategoryId}", id);
            return Result.NotFound(["ServiceCategory not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ServiceCategoryRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ServiceCategoryResponse = _mapper.Map<ServiceCategoryResponseDto>(result);
        if (ServiceCategoryResponse is null)
        {
            _logger.LogError("Failed to map ServiceCategoryRequestDto to ServiceCategoryResponseDto. ServiceCategoryRequestDto: {@ServiceCategoryRequestDto}", ServiceCategoryResponse);

            return Result.Invalid(new List<ValidationError>
            {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }

        _logger.LogInformation("Updated ServiceCategory , Id {Id}", id);

        return Result.Success(ServiceCategoryResponse);
    }
    //<inheritdoc/>
    public async Task<Result> DeleteServiceCategoryAsync(int id)
    {
        var ServiceCategory = await _dbContext.ServiceCategories.FindAsync(id);

        if (ServiceCategory is null)
        {
            _logger.LogWarning("ServiceCategory Invaild Id ,Id {ServiceCategoryId}", id);
            return Result.NotFound(["ServiceCategory Invaild Id"]);
        }

        _dbContext.ServiceCategories.Remove(ServiceCategory);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ServiceCategory removed successfully in the database");
        return Result.SuccessWithMessage("ServiceCategory removed successfully");
    }

    //<inheritdoc/>
    public async Task<Result<PaginationResult<ServiceCategoryResponseDto>>> SearchServiceCategoryByTextAsync(string text, int itemCount, int index)
    {
        var names = await _dbContext.ServiceCategories
            .ProjectTo<ServiceCategoryResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.ServiceCategoryName.Contains(text))
            .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching search ServiceCategory by name . Total count: {ServiceCategory}.", names.Data.Count);

        return Result.Success(names);
    }
}
