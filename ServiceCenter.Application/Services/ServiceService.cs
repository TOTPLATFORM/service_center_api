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
			_logger.LogError("Failed to map DepartmentRequestDto to Department. DepartmentRequestDto: {@DepartmentRequestDto}", ServiceRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

        if (ServiceRequestDto.ServicePackageId > 0)
        {
            var servicePackage = await _dbContext.ServicePackages.FindAsync(ServiceRequestDto.ServicePackageId);

            if (servicePackage is not null)
            {
                result.ServicePackages.Add(servicePackage);
            }
            else
            {
                _logger.LogWarning("ServicePackage with ID {ServicePackageId} not found.", ServiceRequestDto.ServicePackageId);
            }
        }

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
	///<inheritdoc/>
	public async Task<Result<ServiceResponseDto>> UpdateServiceAsync(int id, ServiceRequestDto ServiceRequestDto)
	{
		var result = await _dbContext.Services.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Service Id not found,Id {ServiceId}", id);
			return Result.NotFound(["Service not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(ServiceRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var ServiceResponse = _mapper.Map<ServiceResponseDto>(result);
		if (ServiceResponse is null)
		{
			_logger.LogError("Failed to map ServiceRequestDto to ServiceResponseDto. ServiceRequestDto: {@ServiceRequestDto}", ServiceResponse);

			return Result.Invalid(new List<ValidationError>
		{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
		});
		}

		_logger.LogInformation("Updated Service , Id {Id}", id);

		return Result.Success(ServiceResponse);
	}
	///<inheritdoc/>
	public async Task<Result> DeleteServiceAsync(int id)
	{
		var Service = await _dbContext.Services.FindAsync(id);

		if (Service is null)
		{
			_logger.LogWarning("Service Invaild Id ,Id {ServiceId}", id);
			return Result.NotFound(["Service Invaild Id"]);
		}

		_dbContext.Services.Remove(Service);
		await _dbContext.SaveChangesAsync();
		_logger.LogInformation("Service removed successfully in the database");
		return Result.SuccessWithMessage("Service removed successfully");
	}
	///<inheritdoc/>
	public async Task<Result<List<ServiceResponseDto>>> SearchServiceByTextAsync(string text)
	{
		var names = await _dbContext.Services
		.ProjectTo<ServiceResponseDto>(_mapper.ConfigurationProvider)
		.Where(n => n.ServiceName.Contains(text))
		.ToListAsync();
		_logger.LogInformation("Fetching search Service by name . Total count: {Prouct}.", names.Count);
		return Result.Success(names);
	}
}