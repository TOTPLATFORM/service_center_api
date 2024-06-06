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
    /// Adds a new schedule for a serviceprovider.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider.</remarks>
    /// <param name="scheduleRequestDto">The DTO representing the schedule to create.</param>
    /// <returns>A Result indicating the outcome of the add operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin, ServiceProvider")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSchedule(ScheduleRequestDto scheduleRequestDto)
    {
        return await _scheduleService.AddScheduleAsync(scheduleRequestDto);
    }

    /// <summary>
    /// Retrieves all schedules for a specific serviceprovider.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider, Contact.</remarks>
    /// <param name="serviceproviderId">The ID of the serviceprovider whose schedules to retrieve.</param>
    /// <returns>A Result containing a list of schedule response DTOs.</returns>
    [HttpGet("serviceprovider/{serviceproviderId}")]
    [Authorize(Roles = "Admin, ServiceProvider, Contact")]
    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ScheduleResponseDto>>> GetAllSchedulesForSpecificServiceProvider(string serviceproviderId, int itemCount, int index)
    {
        return await _scheduleService.GetAllSchedulesByServiceProviderIdAsync(serviceproviderId, itemCount, index);
    }

    /// <summary>
    /// Retrieves a schedule by its ID.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider, Contact.</remarks>
    /// <param name="id">The ID of the schedule to retrieve.</param>
    /// <returns>A Result containing the schedule response DTO, or error if not found.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, ServiceProvider, Contact")]
    [ProducesResponseType(typeof(Result<ScheduleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ScheduleResponseDto>> GetScheduleById(int id)
    {
        return await _scheduleService.GetScheduleByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing schedule.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider.</remarks>
    /// <param name="scheduleRequestDto">The DTO representing the updated schedule.</param>
    /// <param name="id">The ID of the schedule to update.</param>
    /// <returns>A Result containing the updated schedule response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, ServiceProvider")]
    [ProducesResponseType(typeof(Result<ScheduleResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ScheduleResponseDto>> UpdateSchedule(int id, ScheduleRequestDto scheduleRequestDto)
    {
        return await _scheduleService.UpdateScheduleAsync(id, scheduleRequestDto);
    }

    /// <summary>
    /// Deletes a schedule by its ID.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider.</remarks>
    /// <param name="id">The ID of the schedule to delete.</param>
    /// <returns>A Result indicating the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin, ServiceProvider")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteSchedule(int id)
    {
        return await _scheduleService.DeleteScheduleAsync(id);
    }

    /// <summary>
    /// Retrieves all available schedules for a specific serviceprovider.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider, Contact.</remarks>
    /// <param name="serviceproviderId">The ID of the serviceprovider whose available schedules to retrieve.</param>
    /// <returns>A Result containing a list of available schedule response DTOs.</returns>
    [HttpGet("available/serviceprovider/{serviceproviderId}")]
    [Authorize(Roles = "Admin, ServiceProvider, Contact")]
    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceProviderByWeek(string serviceproviderId)
    {
        return await _scheduleService.GetAvailableSchedulesForServiceProviderByWeekAsync(serviceproviderId);
    }

    /// <summary>
    /// Retrieves all available schedules for a specific serviceprovider on a specific day of the week.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider, Contact.</remarks>
    /// <param name="serviceproviderId">The ID of the serviceprovider whose available schedules to retrieve.</param>
    /// <param name="dayOfWeek">The day of the week to filter schedules by.</param>
    /// <returns>A Result containing a list of available schedule response DTOs.</returns>
    [HttpGet("available/serviceprovider/{serviceproviderId}/day/{dayOfWeek}")]
    [Authorize(Roles = "Admin, ServiceProvider, Contact")]
    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceProviderByDay(string serviceproviderId, DayOfWeek dayOfWeek)
    {
        return await _scheduleService.GetAvailableSchedulesForServiceProviderByDayAsync(serviceproviderId, dayOfWeek);
    }

    /// <summary>
    /// Retrieves a summary of schedules for a specific serviceprovider.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, ServiceProvider, Contact.</remarks>
    /// <param name="serviceproviderId">The ID of the serviceprovider.</param>
    /// <returns>A Result containing a list of serviceprovider schedule summary DTOs.</returns>
    [HttpGet("serviceprovider/weeklySchedule/{serviceproviderId}")]
    [Authorize(Roles = "Admin, ServiceProvider, Contact")]
    [ProducesResponseType(typeof(Result<List<ServiceProviderWeeklyScheduleDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ServiceProviderWeeklyScheduleDto>>> GetServiceProviderSchedulesSummary(string serviceproviderId)
    {
        return await _scheduleService.GetServiceProviderWeeklySchedulesAsync(serviceproviderId);
    }
}