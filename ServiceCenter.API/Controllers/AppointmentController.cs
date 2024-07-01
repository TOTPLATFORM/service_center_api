using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;


public class AppointmentController(IAppointmentService appointmentService) : BaseController
{
    private readonly IAppointmentService _appointmentService = appointmentService;

    /// <summary>
    /// Retrieves all appointments asynchronously.
    /// </summary>
    /// <returns>A result containing a list of appointment response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAllAppointments(int itemCount, int index)
    {
        return await _appointmentService.GetAllAppointmentsAsync(itemCount, index);
    }

    /// <summary>
    /// Retrieves appointments by service ID asynchronously.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose appointments to retrieve.</param>
    /// <returns>A result containing a list of appointment response DTOs.</returns>
    [HttpGet("service/{serviceId}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByServiceId(int serviceId, int itemCount, int index)
    {
        return await _appointmentService.GetAppointmentsByServiceIdAsync(serviceId, itemCount, index);
    }

    /// <summary>
    /// Retrieves appointments by contact ID asynchronously.
    /// </summary>
    /// <param name="contactId">The ID of the contact whose appointments to retrieve.</param>
    /// <returns>A result containing a list of appointment response DTOs.</returns>
    [HttpGet("contact/{contactId}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByCustomerId(string contactId, int itemCount, int index)
    {
        return await _appointmentService.GetAppointmentsByContactIdAsync(contactId, itemCount, index);
    }

    /// <summary>
    /// Retrieves an appointment by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the appointment to retrieve.</param>
    /// <returns>A result containing the appointment response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result<AppointmentResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<AppointmentResponseDto>> GetAppointmentById(int id)
    {
        return await _appointmentService.GetAppointmentByIdAsync(id);
    }

    /// <summary>
    /// Books a new appointment asynchronously.
    /// </summary>
    /// <param name="appointmentRequestDto">The DTO representing the appointment to book.</param>
    /// <returns>A result indicating the outcome of the booking operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> BookAppointment(AppointmentRequestDto appointmentRequestDto)
    {
        return await _appointmentService.BookAppointmentAsync(appointmentRequestDto);
    }

    /// <summary>
    /// Cancels an appointment by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the appointment to cancel.</param>
    /// <returns>A result indicating the outcome of the cancellation operation.</returns>
    [HttpPost("cancel/{id}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> CancelAppointment(int id)
    {
        return await _appointmentService.CancelAppointmentAsync(id);
    }

    /// <summary>
    /// Changes the status of an appointment.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, Service.</remarks>
    /// <param name="id">The ID of the appointment to change the status of.</param>
    /// <param name="status">The new status of the appointment.</param>
    /// <returns>A Result indicating the outcome of the update operation.</returns>
    [HttpPut("{id}/status/{status}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> ChangeAppointmentStatus(int id, AppointmentStatus status)
    {
        return await _appointmentService.ChangeAppointmentStatusAsync(id, status);
    }

    /// <summary>
    /// Retrieves appointments by service ID and status.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, Service.</remarks>
    /// <param name="serviceId">The ID of the service whose appointments to retrieve.</param>
    /// <param name="status">The status of the appointments to filter by.</param>
    /// <returns>A Result containing a list of appointment response DTOs.</returns>
    [HttpGet("service/{serviceId}/status/{status}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByServiceAndStatus(int serviceId, AppointmentStatus status, int itemCount, int index)
    {
        return await _appointmentService.GetAppointmentsByServiceIdAndStatusAsync(serviceId, status, itemCount, index);
    }

    /// <summary>
    /// Retrieves appointments by contact ID and status.
    /// </summary>
    /// <remarks>Available to users with roles: Admin, Service, Customer.</remarks>
    /// <param name="contactId">The ID of the contact whose appointments to retrieve.</param>
    /// <param name="status">The status of the appointments to filter by.</param>
    /// <returns>A Result containing a list of appointment response DTOs.</returns>
    [HttpGet("contact/{contactId}/status/{status}")]
    [Authorize(Roles = "Admin, ServiceProvider, Customer")]
    [ProducesResponseType(typeof(Result<List<AppointmentResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByCustomerAndStatus(string contactId, AppointmentStatus status, int itemCount, int index)
    {
        return await _appointmentService.GetAppointmentsByContactIdAndStatusAsync(contactId, status, itemCount, index);
    }

    /// <summary>
    /// Deletes an appointment by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the appointment to delete.</param>
    /// <returns>A result indicating the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteAppointment(int id)
    {
        return await _appointmentService.DeleteAppointmentAsync(id);
    }
}
