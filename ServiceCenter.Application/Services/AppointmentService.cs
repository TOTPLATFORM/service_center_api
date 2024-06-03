using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class AppointmentService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<AppointmentService> logger, IUserContextService userContext) :IAppointmentService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AppointmentService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;
    /// <inheritdoc/>
    public async Task<Result> AddAppointmentAsync(AppointmentRequestDto appointmentRequestDto)
    {
        var appointment = _mapper.Map<Appointment>(appointmentRequestDto);
        var customer = _dbContext.Customers.FirstOrDefault(C => C.Id == appointmentRequestDto.CustomerId);
        var schedule = _dbContext.Schedules.FirstOrDefault(C => C.Id == appointmentRequestDto.ScheduleId);

        if (customer is null || schedule is null)
        {
            _logger.LogInformation("customer or schedule not found");
            return Result.Error("appointment added failed to the database");
        }
        appointment.CreatedBy = _userContext.Email;
        appointment.Customer = customer;
        appointment.Schedule = schedule;

        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("appointment added successfully to the database");
        return Result.SuccessWithMessage("appointment added successfully");
    }
    /// <inheritdoc/>
    public async Task<Result> DeleteAppointmentAsync(int id)
    {
        var appointment = await _dbContext.Appointments.FindAsync(id);

        if (appointment is null)
        {
            _logger.LogWarning($"appointment  with id {id} was not found while attempting to delete");
            return Result.NotFound(["The appointment  is not found"]);
        }

        _dbContext.Appointments.Remove(appointment);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully removed appointment  {appointment}");
        return Result.SuccessWithMessage("appointment  removed successfully");
    }
    /// <inheritdoc/>
    public async Task<Result<List<AppointmentResponseDto>>> GetsAppointmentsByEmployeeAsync(string id)
    {
        var appointmentForEmployee = await _dbContext.Appointments.Where(A => A.Schedule.Employee.Id == id)
                    .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        _logger.LogInformation("Fetching all appointment for specific agent . Total count: {appointment}.", appointmentForEmployee.Count);

        return Result.Success(appointmentForEmployee);
    }
    /// <inheritdoc/>
    public async Task<Result<List<AppointmentResponseDto>>> GetAppointmentsByCustomerAsync(string id)
    {
        var appointmentForCustomer = await _dbContext.Appointments.Where(A => A.Customer.Id == id)
            .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        _logger.LogInformation("Fetching all appointment for specific client . Total count: {appointment}.", appointmentForCustomer.Count);

        return Result.Success(appointmentForCustomer);
    }
    /// <inheritdoc/>
    public async Task<Result<List<AppointmentResponseDto>>> GetAllAppointmentsAsync()
    {
        var appointmentResponseDto = await _dbContext.Appointments
                    .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        _logger.LogInformation("Fetching all appointment . Total count: {appointment}.", appointmentResponseDto.Count);

        return Result.Success(appointmentResponseDto);
    }
    /// <inheritdoc/>
    public async Task<Result<AppointmentResponseDto>> GetAppointmentByIdAsync(int id)
    {
        var appointmentResponseDto = await _dbContext.Appointments
                    .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(c => c.Id == id);

        if (appointmentResponseDto is null)
        {
            _logger.LogWarning("appointment  Id not found,Id {id}", id);
            return Result.NotFound(["The appointment  is not found"]);
        }

        _logger.LogInformation("Fetched appointment  details");
        return Result.Success(appointmentResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<AppointmentResponseDto>> UpdateAppointmentAsync(int id, AppointmentRequestDto appointmentRequestDto)
    {
        var appointment = await _dbContext.Appointments.FindAsync(id);
        var customer = _dbContext.Customers.FirstOrDefault(C => C.Id == appointmentRequestDto.CustomerId);
        var schedule = _dbContext.Schedules.FirstOrDefault(C => C.Id == appointmentRequestDto.ScheduleId);

        if (customer is null || schedule is null)
        {
            _logger.LogInformation("customer or schedule not found");
            return Result.Error("appointment added failed to the database");
        }

        if (appointment is null)
        {
            _logger.LogWarning("appointment  Id not found,Id {id}", id);
            return Result.NotFound(["The appointment  is not found"]);
        }
        appointment.Customer = customer;
        appointment.Schedule = schedule;
        appointment.ModifiedBy = _userContext.Email;

        await _dbContext.SaveChangesAsync();

        var updatedAppointment = _mapper.Map<AppointmentResponseDto>(appointment);

        _logger.LogInformation("appointment  updated successfully");
        return Result.Success(updatedAppointment, "appointment  updated successfully");
    }
}
