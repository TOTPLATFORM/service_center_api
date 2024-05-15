using ServiceCenter.Application.DTOS;
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
    /// asynchronously adds a new appointment to the database.
    /// </summary>
    /// <param name="appointmentRequestDto">the appointment data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the appointment addition.</returns>
    public Task<Result> AddAppointmentAsync(AppointmentRequestDto appointmentRequestDto);
    /// <summary>
    /// asynchronously retrieves all appointments in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of appointment response DTOs.</returns>
    public Task<Result<List<AppointmentResponseDto>>> GetAllAppointmentsAsync();
    /// <summary>
    /// asynchronously retrieves a appointment by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the appointment to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the appointment response DTO.</returns>
    public Task<Result<AppointmentResponseDto>> GetAppointmentByIdAsync(int id);
    /// <summary>
    /// asynchronously updates the data of an existing appointment.
    /// </summary>
    /// <param name="id">the unique identifier of the appointment to update.</param>
    /// <param name="appointmentRequestDto">the appointment data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<AppointmentResponseDto>> UpdateAppointmentAsync(int id, AppointmentRequestDto appointmentRequestDto);
    /// <summary>
    /// asynchronously deletes a appointment from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the appointment to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteAppointmentAsync(int id);

    /// <summary>
    /// asynchronously get all a appointment by customer from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the customer to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all appointment by agent operation.</returns>

    public Task<Result<List<AppointmentResponseDto>>> GetAppointmentsByCustomerAsync(string id);

    /// <summary>
    /// asynchronously get all a appointment by employee from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the employee to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all appointment by client operation.</returns>

    public Task<Result<List<AppointmentResponseDto>>> GetsAppointmentsByEmployeeAsync(string id);
}
