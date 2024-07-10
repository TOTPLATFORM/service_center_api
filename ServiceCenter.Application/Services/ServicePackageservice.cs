using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

public class ServicePackageService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ServicePackageService> logger, IUserContextService userContext) : IServicePackageService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ServicePackageService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddServicePackageAsync(ServicePackageRequestDto ServicePackageRequestDto)
    {
        var result = _mapper.Map<ServicePackage>(ServicePackageRequestDto);

        result.CreatedBy = _userContext.Email;

        _dbContext.ServicePackages.Add(result);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("ServicePackage added successfully to the database");

        return Result.SuccessWithMessage("ServicePackage added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ServicePackageResponseDto>>> GetAllServicePackageAsync(int itemCount,int index)
    {
        var result = await _dbContext.ServicePackages
             .ProjectTo<ServicePackageResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  ServicePackage. Total count: { ServicePackage}.", result.Data.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteServicePackageAsync(int id)
    {
        var ServicePackage = await _dbContext.ServicePackages.FindAsync(id);

        if (ServicePackage is null)
        {
            _logger.LogWarning("ServicePackage Invaild Id ,Id {ServicePackageId}", id);
            return Result.NotFound(["ServicePackage Invaild Id"]);
        }

        _dbContext.ServicePackages.Remove(ServicePackage);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ServicePackage removed successfully in the database");
        return Result.SuccessWithMessage("ServicePackage removed successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<ServicePackageResponseDto>> UpdateServicePackageAsync(int id, ServicePackageRequestDto ServicePackageRequestDto)
    {
        var result = await _dbContext.ServicePackages.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("ServicePackage Id not found,Id {ServicePackageId}", id);
            return Result.NotFound(["ServicePackage not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ServicePackageRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ServicePackageResponse = _mapper.Map<ServicePackageResponseDto>(result);

        _logger.LogInformation("Updated ServicePackage , Id {Id}", id);

        return Result.Success(ServicePackageResponse);
    }
    ///<inheritdoc/>
    public async Task<Result<ServicePackageGetByIdResponseDto>> GetServicePackageByIdAsync(int id)
    {
        var result = await _dbContext.ServicePackages
            .ProjectTo<ServicePackageGetByIdResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("ServicePackage Id not found,Id {ServicePackageId}", id);

            return Result.NotFound(["ServicePackage not found"]);
        }

        _logger.LogInformation("Fetching ServicePackage");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ServicePackageResponseDto>>> SearchServicePackageByTextAsync(string text, int itemCount, int index)
    {
        var names = await _dbContext.ServicePackages
        .ProjectTo<ServicePackageResponseDto>(_mapper.ConfigurationProvider)
        .Where(n => n.PackageName.Contains(text))
        .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching search ServicePackage by name . Total count: {Prouct}.", names.Data.Count);
        return Result.Success(names);
    }

    /// <summary>
    /// function to get specific services by their services ids
    /// </summary>
    /// <param name="servicesIds">The ids of the services to retrieve</param>
    /// <returns>Service response dto </returns>
    public async Task<Result<List<Service>>> GetServicesByIds(List<int> servicesIds)
    {
        var services = await _dbContext.Services.Where(s => servicesIds.Contains(s.Id))
            .ToListAsync();

        _logger.LogInformation("Fetching Services by their ids . Total count: {Service}.", services.Count);
        return Result.Success(services);
    }
}