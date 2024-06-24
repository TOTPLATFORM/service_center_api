using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class AppointmentService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<AppointmentService> logger, IUserContextService userContext) : IAppointmentService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AppointmentService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAllAppointmentsAsync(int itemCount, int index)
    {
        var appointments = await _dbContext.Appointments
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > appointments.TotalCount)
        {
            appointments.End = appointments.TotalCount;
        }

        _logger.LogInformation("Fetching all appointments. Total count: {appointments}.", appointments);
        return Result.Success(appointments);
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByServiceIdAsync(int serviceId, int itemCount, int index)
    {
        var appointments = await _dbContext.Appointments
            .Where(a => a.Schedule.ServiceId == serviceId)
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > appointments.TotalCount)
        {
            appointments.End = appointments.TotalCount;
        }

        _logger.LogInformation("Fetching appointments for service {serviceId}. Total count: {count}.", serviceId, appointments);
        return Result.Success(appointments);
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByContactIdAsync(string contactId, int itemCount, int index)
    {
        var appointments = await _dbContext.Appointments
            .Where(a => a.ContactId == contactId)
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > appointments.TotalCount)
        {
            appointments.End = appointments.TotalCount;
        }

        _logger.LogInformation("Fetching appointments for contact {contactId}. Total count: {count}.", contactId, appointments);
        return Result.Success(appointments);
    }

    ///<inheritdoc/>
    public async Task<Result<AppointmentResponseDto>> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _dbContext.Appointments
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment is null)
        {
            _logger.LogWarning("Appointment Id not found, Id {id}", id);
            return Result.NotFound(new[] { "The appointment is not found" });
        }

        _logger.LogInformation("Fetched one appointment");
        return Result.Success(appointment);
    }

    ///<inheritdoc/>
    public async Task<Result> BookAppointmentAsync(AppointmentRequestDto appointmentRequestDto)
    {
        var schedule = await _dbContext.Schedules.FindAsync(appointmentRequestDto.ScheduleId);
        if (schedule == null)
        {
            _logger.LogError("Schedule Id not found, Id {id}", appointmentRequestDto.ScheduleId);
            return Result.NotFound(new[] { "The schedule is not found" });
        }

        var today = DateTime.UtcNow.Date;
        var appointmentDate = today.AddDays((int)schedule.DayOfWeek - (int)today.DayOfWeek);

        if (appointmentDate < today)
        {
            appointmentDate = appointmentDate.AddDays(7);
        }

        var existingAppointment = await _dbContext.Appointments
            .FirstOrDefaultAsync(a => a.ScheduleId == appointmentRequestDto.ScheduleId && a.AppointmentDate == appointmentDate);

        if (existingAppointment != null)
        {
            _logger.LogError("Appointment already exists for schedule Id {id} on date {date}", appointmentRequestDto.ScheduleId, appointmentDate);
            return Result.Error(new[] { "An appointment has already been booked for this schedule on the specified date." });
        }

        var appointment = _mapper.Map<Appointment>(appointmentRequestDto);
        appointment.AppointmentDate = appointmentDate;
        appointment.Status = AppointmentStatus.Scheduled;
        appointment.CreatedBy = _userContext.Email;

        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Appointment booked successfully for schedule Id {id} on date {date}", appointmentRequestDto.ScheduleId, appointmentDate);
        return Result.SuccessWithMessage("Appointment booked successfully");
    }

    ///<inheritdoc/>
    public async Task<Result> CancelAppointmentAsync(int id)
    {
        var appointment = await _dbContext.Appointments.FindAsync(id);

        if (appointment is null)
        {
            _logger.LogWarning($"Appointment with id {id} was not found while attempting to cancel");
            return Result.NotFound(new[] { "The appointment is not found" });
        }

        appointment.Status = AppointmentStatus.Canceled;
        appointment.ModifiedBy = _userContext.Email;

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully canceled appointment {appointment}");
        return Result.SuccessWithMessage("Appointment canceled successfully");
    }

    ///<inheritdoc/>
    public async Task<Result> ChangeAppointmentStatusAsync(int id, AppointmentStatus status)
    {
        var appointment = await _dbContext.Appointments.FindAsync(id);
        if (appointment == null)
        {
            _logger.LogError("Appointment Id not found, Id {id}", id);
            return Result.NotFound(new[] { "The appointment is not found" });
        }
        
        if (status == AppointmentStatus.Completed)
        {
            var itemService = await _dbContext.ItemServices.Where(i => i.Service.Id == appointment.Schedule.ServiceId)
                .Select(i => new Item { ItemStock = i.Item.ItemStock - i.QuantityItem }).ToListAsync();

            /*foreach (var item in itemService)
            {
                item.Item.ItemStock = item.Item.ItemStock-item.QuantityItem;
            }*/

            //itemService.Select(i => i.Item.ItemStock - i.QuantityItem);
             _dbContext.Items.UpdateRange(itemService);
        }

        appointment.Status = status;
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Appointment status updated successfully for Id {id} to status {status}", id, status);
        return Result.SuccessWithMessage("Appointment status updated successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByServiceIdAndStatusAsync(int serviceId, AppointmentStatus status, int itemCount, int index)
    {
        var appointments = await _dbContext.Appointments
            .Where(a => a.Schedule.ServiceId == serviceId && a.Status == status)
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > appointments.TotalCount)
        {
            appointments.End = appointments.TotalCount;
        }

        return Result.Success(appointments);
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<AppointmentResponseDto>>> GetAppointmentsByContactIdAndStatusAsync(string contactId, AppointmentStatus status, int itemCount, int index)
    {
        var appointments = await _dbContext.Appointments
            .Where(a => a.ContactId == contactId && a.Status == status)
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > appointments.TotalCount)
        {
            appointments.End = appointments.TotalCount;
        }

        return Result.Success(appointments);
    }

    ///<inheritdoc/>
    public async Task<Result> DeleteAppointmentAsync(int id)
    {
        var appointment = await _dbContext.Appointments.FindAsync(id);

        if (appointment is null)
        {
            _logger.LogWarning("Appointment Invalid Id, Id {AppointmentId}", id);
            return Result.NotFound(new[] { "Appointment Invalid Id" });
        }

        _dbContext.Appointments.Remove(appointment);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Appointment removed successfully from the database");
        return Result.SuccessWithMessage("Appointment removed successfully");
    }
}
