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

public class AppointmentService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<AppointmentService> logger, IUserContextService userContext) : IAppointmentService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AppointmentService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddAppointmentAsync(AppointmentRequestDto AppointmentRequestDto)
    {
        var result = _mapper.Map<Appointment>(AppointmentRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map AppointmentRequestDto to Appointment. AppointmentRequestDto: {@AppointmentRequestDto}", AppointmentRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Appointments.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Appointment added successfully to the database");
        return Result.SuccessWithMessage("Appointment added successfully");
    }


    ///<inheritdoc/>
    public async Task<Result<List<AppointmentResponseDto>>> GetAllAppointmentAsync()
    {
        var result = await _dbContext.Appointments
             .ProjectTo<AppointmentResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Appointment. Total count: { Appointment}.", result.Count);

        return Result.Success(result);
    }

}
