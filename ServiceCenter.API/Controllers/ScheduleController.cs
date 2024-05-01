using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ScheduleController(IScheduleService ScheduleService) : BaseController
{
    private readonly IScheduleService _ScheduleService = ScheduleService;

    /// <summary>
    /// action for add Schedule  action that take  Schedule dto   
    /// </summary>
    /// <param name="ScheduleDto">Schedule  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Schedule  added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSchedule(ScheduleRequestDto ScheduleDto)
    {
        return await _ScheduleService.AddScheduleAsync(ScheduleDto);
    }

/// <summary>
/// get all Schedule  in the system.
/// </summary>
/// <remarks>
/// Access is limited to users with the "Admin" role.
/// </remarks>
/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
[HttpGet]
//[Authorize(Roles = "Admin")]
[ProducesResponseType(typeof(Result<List<ScheduleResponseDto>>), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
public async Task<Result<List<ScheduleResponseDto>>> GetAllSchedule()
{
    return await _ScheduleService.GetAllScheduleAsync();
}
}