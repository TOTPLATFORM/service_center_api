//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Application.Services;
//using ServiceCenter.Core.Result;

//namespace ServiceCenter.API.Controllers;

//public class ScheduleController(IScheduleService scheduleService) : BaseController
//{
//    private readonly IScheduleService _scheduleService = scheduleService;

//    /// <summary>
//    /// adds a new schedule to the system.
//    /// </summary>
//    /// <param name="scheduleRequestDto">the data transfer object containing schedule details for creation.</param>
//    /// <remarks>
//    /// access is limited to users with the "Admin" role.
//    /// </remarks>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//    [HttpPost]
//    [Authorize(Roles = "Manager")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//    public async Task<Result> AddSchedule(ScheduleRequestDto scheduleRequestDto)
//    {
//        return await _scheduleService.AddScheduleAsync(scheduleRequestDto);
//    }


//    /// <summary>
//    /// retrieves all schedule for spicific agent in the system.
//    /// </summary>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all schedule for spicific agent.</returns>
//    [HttpGet("SearchByEmployee/{id}")]
//    [Authorize(Roles = "Manager,Employee")]
//    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
//    public async Task<Result<List<ScheduleResponseDto>>> GetAllscheduleForEmployee(string id)
//    {
//        return await _scheduleService.GetScheduleByEmployeeAsync(id);
//    }
//    /// <summary>
//    /// retrieves all schedule in the system.
//    /// </summary>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all schedule.</returns>
//    [HttpGet]
//    [Authorize(Roles = "Manager")]
//    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
//    public async Task<Result<List<ScheduleResponseDto>>> GetAllschedule()
//    {
//        return await _scheduleService.GetAllSchedulesAsync();
//    }

//    /// <summary>
//    /// retrieves a schedule  by their unique identifier.
//    /// </summary>
//    /// <param name="id">the unique identifier of the schedule .</param>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the schedule category details.</returns>
//    [HttpGet("{id:int}")]
//    [Authorize(Roles = "Manager,Employee")]
//    [ProducesResponseType(typeof(Result<ScheduleGetByIdResponseDto>), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result<ScheduleGetByIdResponseDto>), StatusCodes.Status404NotFound)]
//    public async Task<Result<ScheduleGetByIdResponseDto>> GetscheduleById(int id)
//    {
//        return await _scheduleService.GetScheduleByIdAsync(id);
//    }

//    /// <summary>
//    /// searches schedule  based on a query text.
//    /// </summary>
//    /// <param name="text">the search query text.</param>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of schedule  that match the search criteria.</returns>
//    [HttpGet("Search/{text}")]
//    [Authorize(Roles = "Manager")]
//    [ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
//    public async Task<Result<List<ScheduleResponseDto>>> SearchSchedule(string text)
//    {
//        return await _scheduleService.SearchSchedulesByTextAsync(text);
//    }

//    /// <summary>
//    /// updates an existing schedule's information.
//    /// </summary>
//    /// <param name="id">the unique identifier of the schedule  to update.</param>
//    /// <param name="scheduleRequestDto">the data transfer object containing updated details for the schedule.</param>
//    /// <remarks>
//    /// access is limited to users with the "Admin" role.
//    /// </remarks>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
//    [HttpPut("{id}")]
//    [Authorize(Roles = "Manager")]
//    [ProducesResponseType(typeof(Result<ScheduleResponseDto>), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//    public async Task<Result<ScheduleResponseDto>> UpdateSchedule(int id, ScheduleRequestDto scheduleRequestDto)
//    {
//        return await _scheduleService.UpdateScheduleAsync(id, scheduleRequestDto);
//    }

//    /// <summary>
//    /// deletes a schedule  from the system by their unique identifier.
//    /// </summary>
//    /// <param name="id">the unique identifier of the schedule  to delete.</param>
//    /// <remarks>
//    /// access is limited to users with the "Admin" role.
//    /// </remarks>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
//    [HttpDelete("{id}")]
//    [Authorize(Roles = "Manager")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//    public async Task<Result> DeleteSchedule(int id)
//    {
//        return await _scheduleService.DeleteScheduleAsync(id);
//    }
//}