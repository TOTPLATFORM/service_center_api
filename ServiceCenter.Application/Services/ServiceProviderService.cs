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

public class ServiceProviderService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ServiceProviderService> logger, IUserContextService userContext, IAuthService authService) : IServiceProviderService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ServiceProviderService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;
    private readonly IAuthService _authService = authService;


    ///<inheritdoc/>
    public async Task<Result> AddServiceProviderAsync(ServiceProviderRequestDto serviceproviderRequestDto)
    {

        var service = new List<Service>();
        foreach (var item in serviceproviderRequestDto.ServiceIds)
        {
            service.Add(await _dbContext.Services.FirstOrDefaultAsync(i => i.Id == item));
        }
        var role = "ServiceProvider";
        var serviceprovider = _mapper.Map<ServiceProvider>(serviceproviderRequestDto);

        var department = await _dbContext.Departments.FindAsync(serviceproviderRequestDto.DepartmentId);

        if (department is null)
        {
            _logger.LogWarning("Department Invaild Id ,Id {departmentId}", serviceproviderRequestDto.DepartmentId);

            return Result.NotFound(["Department Invaild Id"]);
        }
        serviceprovider.Department = department;
        serviceprovider.Services = service;
        var serviceproviderAdded = await _authService.RegisterUserWithRoleAsync(serviceprovider, serviceproviderRequestDto.Password, role);

        if (!serviceproviderAdded.IsSuccess)
        {
            return Result.Error(serviceproviderAdded.Errors.FirstOrDefault());
        }

        _logger.LogInformation("ServiceProvider added successfully in the database");

        return Result.SuccessWithMessage("ServiceProvider added successfully");
    }

    public async Task<Result<PaginationResult<ServiceProviderResponseDto>>> GetAllServiceProviderAsync(int itemCount, int index)
    {
        var serviceproviders = await _dbContext.Users.OfType<ServiceProvider>()
                  .ProjectTo<ServiceProviderResponseDto>(_mapper.ConfigurationProvider)
                  .GetAllWithPagination(itemCount, index);
        _logger.LogInformation("Fetching all serviceprovider. Total count: {serviceprovider}.", serviceproviders.Data.Count);
        return Result.Success(serviceproviders);
    }

    ///<inheritdoc/>
    public async Task<Result<ServiceProviderResponseDto>> GetServiceProviderByIdAsync(string Id)
    {
        var serviceprovider = await _dbContext.ServiceProviders
            .ProjectTo<ServiceProviderResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == Id);

        if (serviceprovider is null)
        {
            _logger.LogWarning("serviceprovider Id not found,Id {serviceproviderId}", Id);
            return Result.NotFound(["serviceprovider not found"]);
        }
        _logger.LogInformation("Fetching serviceprovider");

        return Result.Success(serviceprovider);
    }

    ///<inheritdoc/>

    public async Task<Result<ServiceProviderResponseDto>> UpdateServiceProviderAsync(string id, ServiceProviderRequestDto serviceproviderRequestDto)
    {
        var serviceprovider = await _dbContext.ServiceProviders.FindAsync(id);

        if (serviceprovider is null)
        {
            _logger.LogWarning("serviceprovider Id not found,Id {serviceproviderId}", id);

            return Result.NotFound(["serviceprovider not found"]);
        }

        _mapper.Map(serviceproviderRequestDto, serviceprovider);

        await _dbContext.SaveChangesAsync();

        var serviceproviderResponse = _mapper.Map<ServiceProviderResponseDto>(serviceprovider);

        if (serviceproviderResponse is null)
        {
            _logger.LogError("Failed to map serviceproviderRequestDto to serviceproviderResponseDto. serviceproviderRequestDto: {@serviceproviderRequestDto}", serviceproviderRequestDto);

            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }

        _logger.LogInformation("Updated serviceprovider , Id {Id}", id);

        return Result.Success(serviceproviderResponse);
    }

    ///<inheritdoc/>

    public async Task<Result<PaginationResult<ServiceProviderResponseDto>>> SearchServiceProviderByTextAsync(string text, int itemCount, int index)
    {

        var serviceprovider = await _dbContext.ServiceProviders
                       .ProjectTo<ServiceProviderResponseDto>(_mapper.ConfigurationProvider)
                       .Where(n => n.ServiceProviderFirstName.Contains(text))
                       .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching search branch by name . Total count: {branch}.", serviceprovider.Data.Count);

        return Result.Success(serviceprovider);
    }

    ///<inheritdoc/>

    public async Task<Result> DeleteServiceProviderAsync(string id)
    {
        var serviceprovider = await _dbContext.ServiceProviders.FindAsync(id);

        if (serviceprovider is null)
        {
            _logger.LogWarning("serviceprovider Invaild Id ,Id {serviceproviderId}", id);

            return Result.NotFound(["serviceprovider Invaild Id"]);
        }

        _dbContext.ServiceProviders.Remove(serviceprovider);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("serviceprovider remove successfully in the database");

        return Result.SuccessWithMessage("serviceprovider remove successfully ");
    }
}