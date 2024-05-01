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
        if (result is null)
        {
            _logger.LogError("Failed to map ServicePackageRequestDto to ServicePackage. ServicePackageRequestDto: {@ServicePackageRequestDto}", ServicePackageRequestDto);
            return Result.Invalid(new List<ValidationError>
{
    new ValidationError
    {
        ErrorMessage = "Validation Errror"
    }
});
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.ServicePackages.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("ServicePackage added successfully to the database");
        return Result.SuccessWithMessage("ServicePackage added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<ServicePackageResponseDto>>> GetAllServicePackageAsync()
    {
        var result = await _dbContext.ServicePackages
             .ProjectTo<ServicePackageResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  ServicePackage. Total count: { ServicePackage}.", result.Count);

        return Result.Success(result);
    }
}