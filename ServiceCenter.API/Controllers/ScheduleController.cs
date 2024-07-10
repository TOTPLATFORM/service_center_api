using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ScheduleController(IScheduleService scheduleService) : BaseController
{
    private readonly IScheduleService _scheduleService = scheduleService;

    /// <summary>
    /// Adds a new schedule for a service.
    /// </summary>
    /// <param name="scheduleRequestDto">The DTO representing the schedule to create.</param>
    /// <remarks>
    /// Access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSchedule(ScheduleRequestDto scheduleRequestDto)
    {
        return await _scheduleService.AddScheduleAsync(scheduleRequestDto);
    }

    /// <summary>
    /// Retrieves all schedules for a specific service.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose schedules to retrieve.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,Customer"  role.
    /// </remarks>
    /// <returns>A Result containing a list of schedule response DTOs.</returns>
    [HttpGet("service/{serviceId}")]
    [Authorize(Roles = "Admin, Manager, Customer")]
    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ScheduleResponseDto>>> GetAllSchedulesForSpecificService(int serviceId, int itemCount, int index)
    {
        return await _scheduleService.GetAllSchedulesByServiceIdAsync(serviceId, itemCount, index);
    }

    /// <summary>
    /// Retrieves a schedule by its ID.
    /// </summary>
    /// <param name="id">The ID of the schedule to retrieve.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,Customer" role.
    /// </remarks>
    /// <returns>A Result containing the schedule response DTO, or error if not found.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Manager, Customer")]
    [ProducesResponseType(typeof(Result<ScheduleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ScheduleResponseDto>> GetScheduleById(int id)
    {
        return await _scheduleService.GetScheduleByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing scedule by its ID.
    /// </summary>
    /// <param name="scheduleRequestDto">The DTO representing the updated schedule.</param>
    /// <param name="id">The ID of the schedule to update.</param>
    /// <remarks>
    /// Access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<ScheduleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ScheduleResponseDto>> UpdateSchedule(int id, ScheduleRequestDto scheduleRequestDto)
    {
        return await _scheduleService.UpdateScheduleAsync(id, scheduleRequestDto);
    }

    /// <summary>
    /// Deletes a schedule by its ID.
    /// </summary>
    /// <param name="id">The ID of the schedule to delete.</param>
    /// <remarks>
    /// Access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>A Result indicating the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteSchedule(int id)
    {
        return await _scheduleService.DeleteScheduleAsync(id);
    }

    /// <summary>
    /// Retrieves all available schedules for a specific service.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose available schedules to retrieve.</param>
    /// <remarks>
    /// Access is limited to users with the "Manager,Customer" role.
    /// </remarks>
    /// <returns>A Result containing a list of available schedule response DTOs.</returns>
    [HttpGet("available/service/{serviceId}")]
    [Authorize(Roles = " Manager, Customer")]
    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceByWeek(int serviceId)
    {
        return await _scheduleService.GetAvailableSchedulesForServiceByWeekAsync(serviceId);
    }

    /// <summary>
    /// Retrieves all available schedules for a specific service on a specific day of the week.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose available schedules to retrieve.</param>
    /// <param name="dayOfWeek">The day of the week to filter schedules by.</param>
    /// 
    /// <remarks>
    /// Access is limited to users with the "Manager,Customer" role.
    /// </remarks>
    /// <returns>A Result containing a list of available schedule response DTOs.</returns>
    [HttpGet("available/service/{serviceId}/day/{dayOfWeek}")]
    [Authorize(Roles = "Manager, Customer")]
    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceByDay(int serviceId, DayOfWeek dayOfWeek)
    {
        return await _scheduleService.GetAvailableSchedulesForServiceByDayAsync(serviceId, dayOfWeek);
    }

    /// <summary>
    /// Retrieves a summary of schedules for a specific service.
    /// </summary>
    /// <param name="serviceId">The ID of the service.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,Customer" role.
    /// </remarks>
    /// <returns>A Result containing a list of service schedule summary DTOs.</returns>
    [HttpGet("service/weeklySchedule/{serviceId}")]
    [Authorize(Roles = "Admin, Manager,Customer")]
    [ProducesResponseType(typeof(Result<List<ServiceWeeklyScheduleDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ServiceWeeklyScheduleDto>>> GetServiceSchedulesSummary(int serviceId)
    {
        return await _scheduleService.GetServiceWeeklySchedulesAsync(serviceId);
    }
}