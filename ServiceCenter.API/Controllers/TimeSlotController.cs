using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class TimeSlotController(ITimeSlotService timeSlotService) : BaseController
{
	private readonly ITimeSlotService _timeSlotService = timeSlotService;

	/// <summary>
	/// action for add time slot action that take timeSlot dto   
	/// </summary>
	/// <param name="timeSlotDto">time slot dto</param>
	/// <returns>result for time slot added successfully.</returns>
	[HttpPost]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddTimeSlot(TimeSlotRequestDto timeSlotDto)
	{
		return await _timeSlotService.AddTimeSlotAsync(timeSlotDto);
	}
	[HttpGet]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<List<TimeSlotResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<List<TimeSlotResponseDto>>> GetAllTimeSlots()
	{
		return await _timeSlotService.GetAllTimeSlotAsync();
	}

	/// <summary>
	/// action for get time slot by id that take time slot id.  
	/// </summary>
	/// <returns>result of time slot response dto</returns>
	[HttpGet("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<TimeSlotResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<TimeSlotResponseDto>> GetTimeSlotById(int id)
	{
		return await _timeSlotService.GetTimeSlotByIdAsync(id);
	}

	[HttpPut("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<TimeSlotResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<TimeSlotResponseDto>> UpdateTimeSlot(int id, TimeSlotRequestDto timeSlotDto)
	{
		return await _timeSlotService.UpdateTimeSlotAsync(id, timeSlotDto);
	}

	[HttpGet("search/{text}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<TimeSlotResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<List<TimeSlotResponseDto>>> SerachTimeSlotByText(string text)
	{
		return await _timeSlotService.SearchTimeSlotByTextAsync(text);
	}
	/// <summary>
	///  action for remove TimeSlot that take timeSlot id   
	/// </summary>
	/// <param name="Id">time slot id</param>
	/// <returns>result of TimeSlot removed successfully </returns>
	[HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result> DeleteTimeSlotAsycn(int id)
	{
		return await _timeSlotService.DeleteTimeSlotAsync(id);
	}
}
