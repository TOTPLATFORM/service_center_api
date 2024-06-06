﻿using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

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
	public async Task<Result<PaginationResult<ServiceResponseDto>>> GetAllServiceAsync(int itemCount , int index)
	{
		var result = await _dbContext.Services
			 .ProjectTo<ServiceResponseDto>(_mapper.ConfigurationProvider)
			 .GetAllWithPagination(itemCount,index);

		_logger.LogInformation("Fetching all  Service. Total count: { Service}.", result.Data.Count);

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

	/// <summary>
	/// function to get specific service packages by their service packages ids
	/// </summary>
	/// <param name="packagesIds">The ids of the service packages to retrieve</param>
	/// <returns>Service response dto </returns>
	public async Task<Result<List<ServicePackage>>> GetServicePackagesByIds(List<int> packagesIds)
	{
		var packages = await _dbContext.ServicePackages.Where(s => packagesIds.Contains(s.Id))
			.ToListAsync();

		_logger.LogInformation("Fetching Service packages by their ids . Total count: {Service}.", packages.Count);
		return Result.Success(packages);
	}

	public async Task<Result<List<ServiceResponseDto>>> AssignServiceToPackagesAsync(int serviceId, int servicePackageId)
	{
		var service = await _dbContext.Services.FindAsync(serviceId);

		if (service is null)
		{
			_logger.LogWarning("ServiceId Id not found,Id {id}", serviceId);

			return Result.NotFound(["The Facility is not found"]);
		}

		var package = await _dbContext.ServicePackages.FindAsync(servicePackageId);

		if (package is null)
		{
			_logger.LogWarning("Property Id not found,Id {id}", servicePackageId);

			return Result.NotFound(["The Property is not found"]);
		}

		service.ServicePackages.Add(package);
		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Successfully assigned service to package");

		return Result.SuccessWithMessage("Service added successfully to Package");

	}

	public async Task<Result<List<ServiceResponseDto>>> GetServicesByPackageAsync(int servicePackageId)
	{
		var pacakge = await _dbContext.ServicePackages.FindAsync(servicePackageId);

		if (pacakge is null)
		{
			_logger.LogWarning("ServicePackageId Id not found,Id {id}", servicePackageId);
			return Result.NotFound(["The package is not found"]);
		}

		var services = _mapper.Map<List<ServiceResponseDto>>(pacakge.Services);

		_logger.LogInformation($"Successfully retrieved services  by their package id");
		return Result.Success(services);

	}

}