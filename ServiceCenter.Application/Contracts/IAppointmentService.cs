using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
public interface IAppointmentService : IApplicationService, IScopedService
{
    /// <summary>
    /// Asynchronously deletes an appointment.
    /// </summary>
    /// <param name="id">The ID of the appointment to be deleted.</param>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the delete operation.</returns>
    Task<Result> DeleteAppointmentAsync(int id);

    /// <summary>
    /// Asynchronously retrieves an appointment by its ID.
    /// </summary>
    /// <param name="id">The ID of the appointment to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result containing the appointment response DTO, or a NotFound result if the appointment is not found.</returns>
    Task<Result<AppointmentResponseDto>> GetAppointmentByIdAsync(int id);

    /// <summary>
    /// Asynchronously retrieves all appointments.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result containing a list of appointment response DTOs.</returns>
    Task<Result<PaginationResult<AppointmentResponseDto>>> GetAllAppointmentsAsync(int itemCount, int index);

    /// <summary>
    /// Asynchronously retrieves all appointments for a specific contact by their ID.
    /// </summary>
    /// <param name="contactId">The ID of the contact whose appointments are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result containing a list of appointment response DTOs.</returns>
    Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByContactIdAsync(string contactId, int itemCount, int index);

    /// <summary>
    /// Asynchronously retrieves all appointments for a specific service by their ID.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose appointments are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result containing a list of appointment response DTOs.</returns>
    Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByServiceIdAsync(int serviceId, int itemCount, int index);

    /// <summary>
    /// Asynchronously books an appointment.
    /// </summary>
    /// <param name="appointmentRequestDto">The DTO representing the appointment to be booked.</param>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the booking operation.</returns>
    Task<Result> BookAppointmentAsync(AppointmentRequestDto appointmentRequestDto);

    /// <summary>
    /// Asynchronously cancels an appointment.
    /// </summary>
    /// <param name="id">The ID of the appointment to be canceled.</param>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the cancellation operation.</returns>
    Task<Result> CancelAppointmentAsync(int id);

    /// <summary>
    /// Changes the status of an appointment.
    /// </summary>
    /// <param name="id">The ID of the appointment to change the status of.</param>
    /// <param name="status">The new status of the appointment.</param>
    /// <returns>A Result indicating the outcome of the update operation.</returns>
    Task<Result> ChangeAppointmentStatusAsync(int id, AppointmentStatus status);

    /// <summary>
    /// Retrieves appointments by service ID and status.
    /// </summary>
    /// <param name="serviceId">The ID of the service whose appointments to retrieve.</param>
    /// <param name="status">The status of the appointments to filter by.</param>
    /// <returns>A Result containing a list of appointment response DTOs.</returns>
    Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByServiceIdAndStatusAsync(int serviceId, AppointmentStatus status, int itemCount, int index);

    /// <summary>
    /// Retrieves appointments by contact ID and status.
    /// </summary>
    /// <param name="contactId">The ID of the contact whose appointments to retrieve.</param>
    /// <param name="status">The status of the appointments to filter by.</param>
    /// <returns>A Result containing a list of appointment response DTOs.</returns>
    Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByContactIdAndStatusAsync(string contactId, AppointmentStatus status, int itemCount, int index);
}
