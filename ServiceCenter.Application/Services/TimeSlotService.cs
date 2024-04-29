using AutoMapper;
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

public class TimeSlotService(ServiceCenterBaseDbContext dbContext , IMapper mapper, ILogger<TimeSlotService> logger, IUserContextService userContext) : ITimeSlotService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<TimeSlotService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;

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


	//public Task<Result> DeleteTimeSlotAsync(int id)
	//{
	//	throw new NotImplementedException();
	//}

	//public Task<Result<List<TimeSlotResponseDto>>> GetAllTimeSlotAsync()
	//{
	//	throw new NotImplementedException();
	//}

	//public Task<Result<TimeSlotResponseDto>> GetTimeSlotByIdAsync(int id)
	//{
	//	throw new NotImplementedException();
	//}

	//public Task<Result<TimeSlotResponseDto>> SearchTimeSlotByTextAsync(string text)
	//{
	//	throw new NotImplementedException();
	//}

	//public Task<Result<TimeSlotResponseDto>> UpdateTimeSlotAsync(int id, TimeSlotRequestDto timeSlotRequestDto)
	//{
	//	throw new NotImplementedException();
	//}
}
