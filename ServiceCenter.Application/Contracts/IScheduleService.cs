using ServiceCenter.Application.DTOS;
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
    /// asynchronously adds a new schedule to the database.
    /// </summary>
    /// <param name="scheduleRequestDto">the schedule data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the schedule addition.</returns>
    public Task<Result> AddScheduleAsync(ScheduleRequestDto scheduleRequestDto);
    /// <summary>
    /// asynchronously retrieves all schedules in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of schedule response DTOs.</returns>
    public Task<Result<List<ScheduleResponseDto>>> GetAllSchedulesAsync();

    /// <summary>
    /// asynchronously searches for schedules based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within schedule data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of schedule response DTOs that match the search criteria.</returns>
    public Task<Result<List<ScheduleResponseDto>>> SearchSchedulesByTextAsync(string text);
    /// <summary>
    /// asynchronously retrieves a schedule by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the schedule to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule response DTO.</returns>
    public Task<Result<ScheduleGetByIdResponseDto>> GetScheduleByIdAsync(int id);
    /// <summary>
    /// asynchronously updates the data of an existing schedule.
    /// </summary>
    /// <param name="id">the unique identifier of the schedule to update.</param>
    /// <param name="scheduleRequestDto">the schedule data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto scheduleRequestDto);
    /// <summary>
    /// asynchronously deletes a schedule from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the schedule to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteScheduleAsync(int id);

    /// <summary>
    /// asynchronously get all a schedule by agent from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the agent to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all schedule by agent operation.</returns>

    public Task<Result<List<ScheduleResponseDto>>> GetScheduleByEmployeeAsync(string id);
}
