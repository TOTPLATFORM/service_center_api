using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class AppointmentController(IAppointmentService AppointmentService) : BaseController
{
    private readonly IAppointmentService _AppointmentService = AppointmentService;

    /// <summary>
    /// action for add Appointment  action that take  Appointment dto   
    /// </summary>
    /// <param name="AppointmentDto">Appointment  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Appointment  added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddAppointment(AppointmentRequestDto AppointmentDto)
    {
        return await _AppointmentService.AddAppointmentAsync(AppointmentDto);
    }


    /// <summary>
    /// get all Appointment categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<AppointmentResponseDto>>> GetAllAppointment()
    {
        return await _AppointmentService.GetAllAppointmentAsync();
    }
}
