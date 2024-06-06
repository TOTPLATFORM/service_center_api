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

public class ScheduleService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ScheduleService> logger, IUserContextService userContext) : IScheduleService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ScheduleService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ScheduleResponseDto>>> GetAllSchedulesByServiceProviderIdAsync(string serviceproviderId, int itemCount, int index)
    {
        var schedules = await _dbContext.Schedules
            .Where(s => s.ServiceProviderId == serviceproviderId)
            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > schedules.TotalCount)
        {
            schedules.End = schedules.TotalCount;
        }

        _logger.LogInformation("Fetching schedules. Total count: {count}.", schedules);
        return Result.Success(schedules);
    }

    ///<inheritdoc/>
    public async Task<Result<ScheduleResponseDto>> GetScheduleByIdAsync(int id)
    {
        var schedule = await _dbContext.Schedules
            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (schedule == null)
        {
            _logger.LogWarning("Schedule Id not found, Id {id}", id);
            return Result.NotFound(["The schedule is not found"]);
        }

        _logger.LogInformation("Fetched one schedule");
        return Result.Success(schedule);
    }

    ///<inheritdoc/>
    private async Task<bool> HasConflictAsync(string serviceproviderId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, int? excludeScheduleId = null)
    {
        return await _dbContext.Schedules.AnyAsync(s =>
            s.ServiceProviderId == serviceproviderId &&
            s.DayOfWeek == dayOfWeek &&
            s.Id != excludeScheduleId &&
            ((s.StartTime < endTime && s.StartTime >= startTime) || (s.EndTime > startTime && s.EndTime <= endTime) || (s.StartTime <= startTime && s.EndTime >= endTime)));
    }

    ///<inheritdoc/>
    public async Task<Result> AddScheduleAsync(ScheduleRequestDto scheduleRequestDto)
    {
        if (scheduleRequestDto.StartTime >= scheduleRequestDto.EndTime)
        {
            _logger.LogError("Start time must be less than end time.");
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Start time must be less than end time."
                }
            });
        }

        var totalAvailableMinutes = (scheduleRequestDto.EndTime.ToTimeSpan() - scheduleRequestDto.StartTime.ToTimeSpan()).TotalMinutes;
        if (scheduleRequestDto.Duration.TotalMinutes <= 0 || scheduleRequestDto.Duration.TotalMinutes > totalAvailableMinutes)
        {
            _logger.LogError("Invalid duration provided in the schedule request.");
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Duration must be positive and less than or equal to the time difference between start and end times."
                }
            });
        }

        var serviceproviderExists = await _dbContext.ServiceProviders.AnyAsync(d => d.Id == scheduleRequestDto.ServiceProviderId);
        if (!serviceproviderExists)
        {
            _logger.LogError("ServiceProvider with ID {ServiceProviderId} not found.", scheduleRequestDto.ServiceProviderId);
            return Result.NotFound(new[]
            {
                "ServiceProvider not found."
            });
        }

        var overlappingSchedules = await _dbContext.Schedules
            .Where(s => s.ServiceProviderId == scheduleRequestDto.ServiceProviderId && s.DayOfWeek == scheduleRequestDto.DayOfWeek)
            .ToListAsync();

        TimeOnly startTime = scheduleRequestDto.StartTime;
        TimeOnly endTime = scheduleRequestDto.EndTime;

        while (startTime < endTime)
        {
            var intervalEndTime = startTime.AddMinutes(scheduleRequestDto.Duration.TotalMinutes);

            if (intervalEndTime > endTime)
            {
                break;
            }

            bool isOverlapping = overlappingSchedules.Any(s =>
                (startTime >= s.StartTime && startTime < s.EndTime) ||
                (intervalEndTime > s.StartTime && intervalEndTime <= s.EndTime) ||
                (startTime <= s.StartTime && intervalEndTime >= s.EndTime));

            if (isOverlapping)
            {
                _logger.LogWarning("Overlapping schedule detected for serviceprovider {ServiceProviderId} on day {DayOfWeek}.", scheduleRequestDto.ServiceProviderId, scheduleRequestDto.DayOfWeek);
                return Result.Error(new[]
                {
                    "Overlapping schedule detected for this serviceprovider on the selected day."
                });
            }

            var schedule = new Schedule
            {
                ServiceProviderId = scheduleRequestDto.ServiceProviderId,
                DayOfWeek = scheduleRequestDto.DayOfWeek,
                StartTime = startTime,
                EndTime = intervalEndTime,
                CreatedBy = _userContext.Email
            };

            _dbContext.Schedules.Add(schedule);

            startTime = intervalEndTime;
        }

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Schedules added successfully.");
        return Result.SuccessWithMessage("Schedules added successfully.");
    }

    ///<inheritdoc/>
    public async Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto scheduleRequestDto)
    {
        var schedule = await _dbContext.Schedules.FindAsync(id);

        if (schedule is null)
        {
            _logger.LogWarning("Schedule Id not found, Id {id}", id);
            return Result.NotFound(new[] { "The schedule is not found" });
        }

        if (await HasConflictAsync(scheduleRequestDto.ServiceProviderId, scheduleRequestDto.DayOfWeek, scheduleRequestDto.StartTime, scheduleRequestDto.EndTime, id))
        {
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError { ErrorMessage = "Schedule conflicts with existing schedule." }
            });
        }

        _mapper.Map(scheduleRequestDto, schedule);

        schedule.ModifiedBy = _userContext.Email;
        await _dbContext.SaveChangesAsync();

        var updatedSchedule = _mapper.Map<ScheduleResponseDto>(schedule);

        _logger.LogInformation("Schedule updated successfully");
        return Result.Success(updatedSchedule, "Schedule updated successfully");
    }

    ///<inheritdoc/>
    public async Task<Result> DeleteScheduleAsync(int id)
    {
        var schedule = await _dbContext.Schedules.FindAsync(id);

        if (schedule == null)
        {
            _logger.LogWarning($"Schedule with id {id} was not found while attempting to delete");
            return Result.NotFound(["The schedule is not found"]);
        }

        _dbContext.Schedules.Remove(schedule);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully removed schedule {schedule}");
        return Result.SuccessWithMessage("Schedule removed successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceProviderByWeekAsync(string serviceproviderId)
    {
        var currentDate = DateTime.UtcNow.Date;

        var schedules = await _dbContext.Schedules
            .Where(s => s.ServiceProviderId == serviceproviderId)
            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var bookedSchedules = await _dbContext.Appointments
            .Where(a => a.Schedule.ServiceProviderId == serviceproviderId)
            .Select(a => new { a.ScheduleId, a.AppointmentDate })
            .ToListAsync();

        var availableSchedules = schedules
            .Where(s => !bookedSchedules.Any(b => b.ScheduleId == s.Id && b.AppointmentDate >= currentDate && b.AppointmentDate < currentDate.AddDays(7)))
            .ToList();

        _logger.LogInformation("Fetching available schedules for serviceprovider {serviceproviderId}. Total available count: {count}.", serviceproviderId, availableSchedules.Count);
        return Result.Success(availableSchedules);
    }

    ///<inheritdoc/>
    public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceProviderByDayAsync(string serviceproviderId, DayOfWeek dayOfWeek)
    {
        var currentDate = DateTime.UtcNow.Date;

        var schedules = await _dbContext.Schedules
            .Where(s => s.ServiceProviderId == serviceproviderId && s.DayOfWeek == dayOfWeek)
            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var bookedSchedules = await _dbContext.Appointments
            .Where(a => a.Schedule.ServiceProviderId == serviceproviderId)
            .Select(a => new { a.ScheduleId, a.AppointmentDate })
            .ToListAsync();

        var availableSchedules = schedules
            .Where(s => !bookedSchedules.Any(b => b.ScheduleId == s.Id && b.AppointmentDate >= currentDate && b.AppointmentDate < currentDate.AddDays(7)))
            .ToList();

        _logger.LogInformation("Fetching available schedules for serviceprovider {serviceproviderId}.", serviceproviderId);
        return Result.Success(availableSchedules);
    }

    ///<inheritdoc/>
    public async Task<Result<List<ServiceProviderWeeklyScheduleDto>>> GetServiceProviderWeeklySchedulesAsync(string serviceproviderId)
    {
        var serviceprovider = await _dbContext.ServiceProviders
            .Where(d => d.Id == serviceproviderId)
            .Select(d => new { d.Id, d.FirstName, d.LastName })
            .FirstOrDefaultAsync();

        if (serviceprovider == null)
        {
            _logger.LogWarning("ServiceProvider Id not found, Id {id}", serviceproviderId);
            return Result.NotFound(new[] { "ServiceProvider not found" });
        }

        var schedules = await _dbContext.Schedules
            .Where(s => s.ServiceProviderId == serviceproviderId)
            .GroupBy(s => s.DayOfWeek)
            .Select(g => new ServiceProviderWeeklyScheduleDto
            {
                ServiceProviderId = serviceprovider.Id,
                ServiceProviderName = serviceprovider.FirstName + " " + serviceprovider.LastName,
                DayOfWeek = g.Key,
                Date = DateTime.UtcNow.Date.AddDays(((int)g.Key - (int)DateTime.UtcNow.DayOfWeek + 7) % 7),
                StartTime = g.Min(s => s.StartTime),
                EndTime = g.Max(s => s.EndTime)
            })
            .ToListAsync();

        // Sorting schedules with today as the first day
        var today = DateTime.UtcNow.DayOfWeek;
        var sortedSchedules = schedules
            .OrderBy(s => (int)s.DayOfWeek >= (int)today ? (int)s.DayOfWeek - (int)today : (int)s.DayOfWeek - (int)today + 7)
            .ToList();

        _logger.LogInformation("Fetching schedules summary for serviceprovider {serviceproviderId}. Total count: {count}.", serviceproviderId, sortedSchedules.Count);
        return Result.Success(sortedSchedules);
    }

    //public async Task<Result<List<ScheduleResponseDto>>> GetAvailableSchedulesForServiceProviderByDayAsync(string serviceproviderId, DayOfWeek dayOfWeek)
    //{
    //    var currentDate = DateTime.UtcNow.Date;
    //    var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
    //    var selectedDay = startOfWeek.AddDays((int)dayOfWeek);

    //    var schedules = await _dbContext.Schedules
    //        .Where(s => s.ServiceProviderId == serviceproviderId && s.DayOfWeek == dayOfWeek)
    //        .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
    //        .ToListAsync();

    //    var bookedScheduleIds = await _dbContext.Appointments
    //        .Where(a => a.Schedule.ServiceProviderId == serviceproviderId && a.AppointmentDate.Date == selectedDay && a.Status == AppointmentStatus.Scheduled)
    //        .Select(a => a.ScheduleId)
    //        .ToListAsync();

    //    var availableSchedules = schedules
    //        .Where(s => !bookedScheduleIds.Contains(s.Id))
    //        .ToList();

    //    _logger.LogInformation("Fetching available schedules for serviceprovider {serviceproviderId} on {dayOfWeek}. Total available count: {count}.", serviceproviderId, dayOfWeek, availableSchedules.Count);
    //    return Result.Success(availableSchedules);
    //}
}
