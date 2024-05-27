  using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class AppointmentController(IAppointmentService appointmentService) : BaseController
{
	private readonly IAppointmentService _appointmentService = appointmentService;

	/// <summary>
	/// adds a new appointment to the system.
	/// </summary>
	/// <param name="appointmentRequestDto">the data transfer object containing appointment details for creation.</param>
	/// <remarks>
	/// access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpPost]
	[Authorize(Roles = "Admin,Customer,Manager,Employee")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	public async Task<Result> AddAppointment(AppointmentRequestDto appointmentRequestDto)
	{
		return await _appointmentService.AddAppointmentAsync(appointmentRequestDto);
	}


	/// <summary>
	/// retrieves all appointment for spicific employee in the system.
	/// </summary>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all appointment for spicific employee.</returns>
	[HttpGet("SearchByEmployee/{id}")]
	[Authorize(Roles = "Admin,Customer,Manager,Employee")]
	[ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<List<AppointmentResponseDto>>> GetAllappointmentForEmployee(string id)
	{
		return await _appointmentService.GetsAppointmentsByEmployeeAsync(id);
	}
	/// <summary>
	/// retrieves all appointment for spicific customer in the system.
	/// </summary>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all appointment for spicific customer.</returns>
	[HttpGet("SearchByCustomer/{id}")]
	[Authorize(Roles = "Admin,Customer,Manager,Employee")]
	[ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<List<AppointmentResponseDto>>> GetAllappointmentForCustomer(string id)
	{
		return await _appointmentService.GetAppointmentsByCustomerAsync(id);
	}
	/// <summary>
	/// retrieves all appointment in the system.
	/// </summary>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all appointment.</returns>
	[HttpGet]
	//[Authorize(Roles = "Customer,Manager,Employee")]
	[ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<List<AppointmentResponseDto>>> GetAllappointment()
	{
		return await _appointmentService.GetAllAppointmentsAsync();
	}

	/// <summary>
	/// retrieves a appointment  by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the appointment .</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the appointment category details.</returns>
	[HttpGet("{id:int}")]
	[Authorize(Roles = "Admin,Customer,Manager,Employee")]
	[ProducesResponseType(typeof(Result<AppointmentResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result<AppointmentResponseDto>), StatusCodes.Status400BadRequest)]
	public async Task<Result<AppointmentResponseDto>> GetAppointmentById(int id)
	{
		return await _appointmentService.GetAppointmentByIdAsync(id);
	}

	/// <summary>
	/// updates an existing appointment's information.
	/// </summary>
	/// <param name="id">the unique identifier of the appointment  to update.</param>
	/// <param name="appointmentRequestDto">the data transfer object containing updated details for the appointment.</param>
	/// <remarks>
	/// access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
	[HttpPut("{id}")]
	[Authorize(Roles = "Admin,Customer,Manager,Employee")]
	[ProducesResponseType(typeof(Result<AppointmentResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<AppointmentResponseDto>> Updateappointment(int id, AppointmentRequestDto appointmentRequestDto)
	{
		return await _appointmentService.UpdateAppointmentAsync(id, appointmentRequestDto);
	}

	/// <summary>
	/// deletes a appointment  from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the appointment  to delete.</param>
	/// <remarks>
	/// access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
	[HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer,Manager,Employee")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result> DeleteAppointment(int id)
	{
		return await _appointmentService.DeleteAppointmentAsync(id);
	}
}
