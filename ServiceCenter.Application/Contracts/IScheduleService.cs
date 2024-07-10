using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for schedule-related services that manages schedule data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IScheduleService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously retrieves all schedules in the system.
	/// </summary>
	///  <param name="serviceId">The ID of the service whose schedules are to be retrieved.</param>
	/// <param name = "itemCount" > item count of schedulees to retrieve</param>
	///<param name="index">index of schedulees to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of schedule response DTOs.</returns>
	Task<Result<PaginationResult<ScheduleResponseDto>>> GetAllSchedulesByServiceIdAsync(int serviceId, int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a schedule by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the schedule to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule response DTO.</returns>
	Task<Result<ScheduleResponseDto>> GetScheduleByIdAsync(int id);

	/// <summary>
	/// asynchronously retrieves a schedule by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the schedule to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule response DTO.</returns>
	Task<Result> AddScheduleAsync(ScheduleRequestDto requestDto);

	/// <summary>
	/// asynchronously updates the data of an existing schedule.
	/// </summary>
	/// <param name="id">the unique identifier of the schedule to update.</param>
	/// <param name="scheduleRequestDto">the schedule data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto scheduleRequestDto);

	/// <summary>
	/// asynchronously deletes a schedule from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the schedule to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	Task<Result> DeleteScheduleAsync(int id);

	/// <summary>
	/// Retrieves available schedules for a specific service for the entire week.
	/// </summary>
	/// <param name="serviceId">The ID of the service whose available schedules are to be retrieved.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule response DTO.</returns>
	Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceByWeekAsync(int serviceId);

	/// <summary>
	/// Retrieves available schedules for a specific service on a specific day.
	/// </summary>
	/// <param name="serviceId">The ID of the service whose available schedules are to be retrieved.</param>
	/// <param name="dayOfWeek">The day of the week for which schedules are to be retrieved.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule response DTO.</returns>
	Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceByDayAsync(int serviceId, DayOfWeek dayOfWeek);

	/// <summary>
	/// Retrieves a summary of schedules for a specific service.
	/// </summary>
	/// <param name="serviceId">The ID of the service.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule response DTO.</returns>
	public Task<Result<List<ServiceWeeklyScheduleDto>>> GetServiceWeeklySchedulesAsync(int serviceId);
}
