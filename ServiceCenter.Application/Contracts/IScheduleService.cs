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
    /// Retrieves all schedules for a specific service by their ID.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose schedules are to be retrieved.</param>
    /// <returns>A Result containing a list of schedule response DTOs.</returns>
    Task<Result<PaginationResult<ScheduleResponseDto>>> GetAllSchedulesByServiceIdAsync(int serviceId, int itemCount, int index);

    /// <summary>
    /// Retrieves a specific schedule by its ID.
    /// </summary>
    /// <param name="id">The ID of the schedule to retrieve.</param>
    /// <returns>A Result containing the schedule response DTO, or an error if not found.</returns>
    Task<Result<ScheduleResponseDto>> GetScheduleByIdAsync(int id);

    /// <summary>
    /// Adds a new schedule for a service.
    /// </summary>
    /// <param name="requestDto">The DTO representing the schedule to create.</param>
    /// <returns>A Result indicating the outcome of the add operation.</returns>
    Task<Result> AddScheduleAsync(ScheduleRequestDto requestDto);

    /// <summary>
    /// Updates an existing schedule.
    /// </summary>
    /// <param name="id">The ID of the schedule to update.</param>
    /// <param name="scheduleRequestDto">The DTO representing the updated schedule.</param>
    /// <returns>A Result containing the updated schedule response DTO.</returns>
    Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto scheduleRequestDto);

    /// <summary>
    /// Deletes a schedule by its ID.
    /// </summary>
    /// <param name="id">The ID of the schedule to delete.</param>
    /// <returns>A Result indicating the outcome of the deletion operation.</returns>
    Task<Result> DeleteScheduleAsync(int id);

    /// <summary>
    /// Retrieves available schedules for a specific service for the entire week.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose available schedules are to be retrieved.</param>
    /// <returns>A Result containing a list of available schedule response DTOs.</returns>
    Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceByWeekAsync(int serviceId);

    /// <summary>
    /// Retrieves available schedules for a specific service on a specific day.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose available schedules are to be retrieved.</param>
    /// <param name="dayOfWeek">The day of the week for which schedules are to be retrieved.</param>
    /// <returns>A Result containing a list of available schedule response DTOs.</returns>
    Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceByDayAsync(int serviceId, DayOfWeek dayOfWeek);

    /// <summary>
    /// Retrieves a summary of schedules for a specific service.
    /// </summary>
    /// <param name="serviceId">The ID of the service.</param>
    /// <returns>A Result containing a list of service schedule summary DTOs.</returns>
    public Task<Result<List<ServiceWeeklyScheduleDto>>> GetServiceWeeklySchedulesAsync(int serviceId);
}
