using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services; 

public class CampaginService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CampaginService> logger, IUserContextService userContext) : ICampaginService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<CampaginService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddCampaginAsync(CampaginRequestDto campaginRequestDto)
	{
		var result = _mapper.Map<Campagin>(campaginRequestDto);

		if (result is null)
		{
			_logger.LogError("Failed to map CampaginRequestDto to Campagin. CampaginRequestDto: {@CampaginRequestDto}", campaginRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

		_dbContext.Campagins.Add(result);

		var contactsToUpdate = await _dbContext.Contacts
										   .Where(c => c.Status == ContactStatus.Opportunity)
										   .ToListAsync();

		foreach (var contact in contactsToUpdate)
		{
			contact.Status = ContactStatus.Lead;
		}

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Campagin added successfully to the database");

		return Result.SuccessWithMessage("Campagin added successfully");
	}


	///<inheritdoc/>
	public async Task<Result<List<CampaginResponseDto>>> GetAllCampaginsAsync()
	{
		var result = await _dbContext.Campagins
				 .ProjectTo<CampaginResponseDto>(_mapper.ConfigurationProvider)
				 .ToListAsync();

		_logger.LogInformation("Fetching all Campagins. Total count: {campagin}.", result.Count);

		return Result.Success(result);
	}

	///<inheritdoc/>
	public async Task<Result<CampaginGetByIdResposeDto>> GetCampaginByIdAsync(int id)
	{
		var result = await _dbContext.Campagins
				.ProjectTo<CampaginGetByIdResposeDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(c=> c.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Camapgin Id not found,Id {CampaginId}", id);

			return Result.NotFound(["Campagin not found"]);
		}

		_logger.LogInformation("Fetching Camapgin");

		return Result.Success(result);
	}

	///<inheritdoc/>
	public async Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginAsync(int id, CampaginRequestDto campaginRequestDto)
	{
		var result = await _dbContext.Campagins.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Camapgin Id not found,Id {CampaginId}", id);
			return Result.NotFound(["Campagin not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(campaginRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var campagin = _mapper.Map<CampaginGetByIdResposeDto>(result);

		if (campagin is null)
		{
			_logger.LogError("Failed to map CampaginRequestDto to CampaginResponseDto. CampaginRequestDto: {@CampaginRequestDto}", campagin);

			return Result.Invalid(new List<ValidationError>
			{
					new ValidationError
					{
						ErrorMessage = "Validation Errror"
					}
			});
		}

		_logger.LogInformation("Updated Campagin , Id {Id}", id);

		return Result.Success(campagin);
	}

	///<inheritdoc/>

	public async Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginStatusAsync(int id, CampaginStatus status)
	{
		var campagins = await _dbContext.Campagins.FindAsync(id);

		if (campagins is null)
		{
			_logger.LogWarning($"Campagin with id {id} was not found while attempting to update contact status by id");

			return Result.NotFound(["The contact is not found"]);
		}

		if (status == CampaginStatus.Cancelled)
		{
			_dbContext.Campagins.Remove(campagins);
			await _dbContext.SaveChangesAsync();
		}

		var previousCampaginStatus = campagins.Status;
		campagins.Status = status;
		campagins.ModifiedBy = _userContext.Email;
		await _dbContext.SaveChangesAsync();

		var campaginResponseDto = _mapper.Map<CampaginGetByIdResposeDto>(campagins);

		_logger.LogInformation($"Successfully update campagin status to: {campagins.Status} from: {previousCampaginStatus}");

		return Result.Success(campaginResponseDto, "Successfully updated campagin");
	}
	///<inheritdoc/>
	public async Task<Result<List<CampaginResponseDto>>> SearchCampaginByTextAsync(string text)
	{
		var campagins = await _dbContext.Campagins
					   .ProjectTo<CampaginResponseDto>(_mapper.ConfigurationProvider)
					   .Where(c => c.CampaginDescription.Contains(text) || c.CampaginDescription.Contains(text))
					   .ToListAsync();

		_logger.LogInformation("Fetching search campagin by name . Total count: {campagin}.", campagins.Count);

		return Result.Success(campagins);
	}

	///<inheritdoc/>
	public async Task<Result> DeleteCampaginAsync(int id)
	{
		var campagins = await _dbContext.Campagins.FindAsync(id);

		if (campagins is null)
		{
			_logger.LogWarning("Campagin Invaild Id ,Id {CampaginId}", id);

			return Result.NotFound(["Campagin Invaild Id"]);
		}

		_dbContext.Campagins.Remove(campagins);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Campagin removed successfully in the database");

		return Result.SuccessWithMessage("Campagin removed successfully");
	}

	
}
