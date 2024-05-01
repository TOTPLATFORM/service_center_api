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

public class ServiceService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ServiceService> logger, IUserContextService userContext) : IServiceService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ServiceService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddServiceAsync(ServiceRequestDto ServiceRequestDto)
    {
        var result = _mapper.Map<Service>(ServiceRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ServiceRequestDto to Service. ServiceRequestDto: {@ServiceRequestDto}", ServiceRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Services.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Service added successfully to the database");
        return Result.SuccessWithMessage("Service added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<ServiceResponseDto>>> GetAllServiceAsync()
    {
        var result = await _dbContext.Services
             .ProjectTo<ServiceResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Service. Total count: { Service}.", result.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ServiceResponseDto>> GetServiceByIdAsync(int id)
    {
        var result = await _dbContext.Services
            .ProjectTo<ServiceResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Service Id not found,Id {ServiceId}", id);

            return Result.NotFound(["Service not found"]);
        }

        _logger.LogInformation("Fetching Service");

        return Result.Success(result);
    }
}