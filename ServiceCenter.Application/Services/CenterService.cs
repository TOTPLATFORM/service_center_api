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

public class CenterService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CenterService> logger, IUserContextService userContext) : ICenterService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<CenterService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddCenterAsync(CenterRequestDto centerRequestDto)
	{
		var result = _mapper.Map<Center>(centerRequestDto);

		if (result is null)
		{
			_logger.LogError("Failed to map CenterRequestDto to Center. CenterRequestDto: {@CenterRequestDto}", centerRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

		_dbContext.Centers.Add(result);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Center added successfully to the database");

		return Result.SuccessWithMessage("Center added successfully");
	}
	///<inheritdoc/>
	public async Task<Result<List<CenterResponseDto>>> GetAllCentersAsync()
	{
		var result = await _dbContext.Centers
				 .ProjectTo<CenterResponseDto>(_mapper.ConfigurationProvider)
				 .ToListAsync();

		_logger.LogInformation("Fetching all Centers. Total count: {Center}.", result.Count);

		return Result.Success(result);
	}
	///<inheritdoc/>
	public async Task<Result<CenterResponseDto>> GetCenterByIdAsync(int id)
	{
		var result = await _dbContext.Centers
				.ProjectTo<CenterResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(timeslot => timeslot.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Center Id not found,Id {CenterId}", id);

			return Result.NotFound(["Center not found"]);
		}

		_logger.LogInformation("Fetching Center");

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<CenterResponseDto>> UpdateCenterAsync(int id, CenterRequestDto timeSlotRequestDto)
	{
		var result = await _dbContext.Centers.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Center Id not found,Id {CenterId}", id);
			return Result.NotFound(["Center not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(timeSlotRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var inventory = _mapper.Map<CenterResponseDto>(result);

		if (inventory is null)
		{
			_logger.LogError("Failed to map CenterRequestDto to CenterResponseDto. CenterRequestDto: {@CenterRequestDto}", inventory);

			return Result.Invalid(new List<ValidationError>
			{
					new ValidationError
					{
						ErrorMessage = "Validation Errror"
					}
			});
		}

		_logger.LogInformation("Updated Center , Id {Id}", id);

		return Result.Success(inventory);
	}

	///<inheritdoc/>
	public async Task<Result<List<CenterResponseDto>>> SearchCenterByTextAsync(string text)
	{


		//if (string.IsNullOrWhiteSpace(text))
		//{
		//	_logger.LogError("Search text cannot be empty", text);

		//	return new Result.Invalid(new List<ValidationError>
		//	{
		//		new ValidationError
		//		{
		//			ErrorMessage = "Validation Errror : Search text cannot be empty"
		//		}
		//	});
		//}

		var Days = await _dbContext.Centers
					   .ProjectTo<CenterResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.CenterName.Contains(text) || n.Specialty.Contains(text))
					   .ToListAsync();

		_logger.LogInformation("Fetching search center by name . Total count: {center}.", Days.Count);

		return Result.Success(Days);

	}

	///<inheritdoc/>
	public async Task<Result> DeleteCenterAsync(int id)
	{
		var timeSlot = await _dbContext.Centers.FindAsync(id);

		if (timeSlot is null)
		{
			_logger.LogWarning("Center Invaild Id ,Id {CenterId}", id);

			return Result.NotFound(["Center Invaild Id"]);
		}

		_dbContext.Centers.Remove(timeSlot);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Center removed successfully in the database");

		return Result.SuccessWithMessage("Center removed successfully");
	}

}