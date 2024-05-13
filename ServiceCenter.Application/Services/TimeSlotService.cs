using AutoMapper;
using AutoMapper.QueryableExtensions;
using Google.Protobuf.WellKnownTypes;
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
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class TimeSlotService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<TimeSlotService> logger, IUserContextService userContext) : ITimeSlotService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<TimeSlotService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>

	public async Task<Result> AddTimeSlotAsync(TimeSlotRequestDto timeSlotRequestDto)
	{
		var result = _mapper.Map<TimeSlot>(timeSlotRequestDto);
		if (result is null)
		{
			_logger.LogError("Failed to map TimeSlotRequestDto to TimeSlot. TimeSlotRequestDto: {@TimeSlotRequestDto}", timeSlotRequestDto);
			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;
		_dbContext.TimeSlots.Add(result);
		await _dbContext.SaveChangesAsync();
		_logger.LogInformation("TimeSlot added successfully to the database");
		return Result.SuccessWithMessage("TimeSlot added successfully");
	}

	///<inheritdoc/>

	public async Task<Result<List<TimeSlotResponseDto>>> GetAllTimeSlotAsync()
	{
		var result = await _dbContext.TimeSlots
				 .ProjectTo<TimeSlotResponseDto>(_mapper.ConfigurationProvider)
				 .ToListAsync();

		_logger.LogInformation("Fetching all TimeSlot. Total count: {TimeSlot}.", result.Count);

		return Result.Success(result);
	}

	///<inheritdoc/>
	public async Task<Result<TimeSlotResponseDto>> GetTimeSlotByIdAsync(int id)
	{
		var result = await _dbContext.TimeSlots
				.ProjectTo<TimeSlotResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(timeslot => timeslot.Id == id);

		if (result is null)
		{
			_logger.LogWarning("TimeSlot Id not found,Id {TimeSlotId}", id);

			return Result.NotFound(["TimeSlot not found"]);
		}

		_logger.LogInformation("Fetching TimeSlot");

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<TimeSlotResponseDto>> UpdateTimeSlotAsync(int id, TimeSlotRequestDto timeSlotRequestDto)
	{
		var result = await _dbContext.TimeSlots.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("TimeSlot Id not found,Id {TimeSlotId}", id);
			return Result.NotFound(["TimeSlot not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(timeSlotRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var timeSlotResponse = _mapper.Map<TimeSlotResponseDto>(result);
		if (timeSlotResponse is null)
		{
			_logger.LogError("Failed to map TimeSlotRequestDto to TimeSlotResponseDto. TimeSlotRequestDto: {@TimeSlotRequestDto}", timeSlotResponse);

			return Result.Invalid(new List<ValidationError>
			{
					new ValidationError
					{
						ErrorMessage = "Validation Errror"
					}
			});
		}

		_logger.LogInformation("Updated TimeSlot , Id {Id}", id);

		return Result.Success(timeSlotResponse);
	}

	///<inheritdoc/>
	public async Task<Result<List<TimeSlotResponseDto>>> SearchTimeSlotByTextAsync(string text)
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

		var Day = await _dbContext.TimeSlots
                       .ProjectTo<TimeSlotResponseDto>(_mapper.ConfigurationProvider)
		               .Where(n => n.Day.Contains(text))
		               .ToListAsync();

		_logger.LogInformation("Fetching search time slot by name . Total count: {time slot}.", Day.Count);

		return Result.Success(Day);

	}

	///<inheritdoc/>
	public async Task<Result> DeleteTimeSlotAsync(int id)
	{
		var timeSlot = await _dbContext.TimeSlots.FindAsync(id);

		if (timeSlot is null)
		{
			_logger.LogWarning("TimeSlot Invaild Id ,Id {TimeSlotId}", id);

			return Result.NotFound(["TimeSlot Invaild Id"]);
		}

		_dbContext.TimeSlots.Remove(timeSlot);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("TimeSlot removed successfully in the database");

		return Result.SuccessWithMessage("TimeSlot removed successfully");
	}

}
