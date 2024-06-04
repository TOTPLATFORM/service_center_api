//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Core.Result;
//using ServiceCenter.Domain.Entities;
//using ServiceCenter.Infrastructure.BaseContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Application.Services;

//public class ScheduleService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ScheduleService> logger, IUserContextService userContext) : IScheduleService
//{
//    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
//    private readonly IMapper _mapper = mapper;
//    private readonly ILogger<ScheduleService> _logger = logger;
//    private readonly IUserContextService _userContext = userContext;
//    /// <inheritdoc/>
//    public async Task<Result> AddScheduleAsync(ScheduleRequestDto scheduleRequestDto)
//    {
//        var schedule = _mapper.Map<Schedule>(scheduleRequestDto);
//        var employee = _dbContext.Employees.FirstOrDefault(C => C.Id == scheduleRequestDto.EmployeeId);

//        if (employee is null)
//        {
//            _logger.LogInformation("serviceProvider not found");
//            return Result.Error("schedule added failed to the database");
//        }
//        schedule.CreatedBy = _userContext.Email;
//        schedule.ServiceProvider = employee;
     

//        _dbContext.Schedules.Add(schedule);
//        await _dbContext.SaveChangesAsync();

//        _logger.LogInformation("schedule added successfully to the database");
//        return Result.SuccessWithMessage("schedule added successfully");
//    }
//    /// <inheritdoc/>
//    public async Task<Result> DeleteScheduleAsync(int id)
//    {
//        var schedule = await _dbContext.Schedules.FindAsync(id);

//        if (schedule is null)
//        {
//            _logger.LogWarning($"schedule  with id {id} was not found while attempting to delete");
//            return Result.NotFound(["The schedule  is not found"]);
//        }

//        _dbContext.Schedules.Remove(schedule);
//        await _dbContext.SaveChangesAsync();

//        _logger.LogInformation($"Successfully removed schedule  {schedule}");
//        return Result.SuccessWithMessage("schedule  removed successfully");
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<ScheduleResponseDto>>> GetScheduleByEmployeeAsync(string id)
//    {
//        var scheduleForAgentResponseDto = await _dbContext.Schedules.Where(A => A.ServiceProvider.Id == id)
//            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
//            .ToListAsync();

//        _logger.LogInformation("Fetching all schedule for specific agent . Total count: {agent}.", scheduleForAgentResponseDto.Count);

//        return Result.Success(scheduleForAgentResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<ScheduleResponseDto>>> GetAllSchedulesAsync()
//    {
//        var scheduleResponseDto = await _dbContext.Schedules
//            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
//            .ToListAsync();

//        _logger.LogInformation("Fetching all schedule . Total count: {schedule}.", scheduleResponseDto.Count);

//        return Result.Success(scheduleResponseDto);
//    }

//    /// <inheritdoc/>
//    public async Task<Result<ScheduleGetByIdResponseDto>> GetScheduleByIdAsync(int id)
//    {
//        var scheduleResponseDto = await _dbContext.Schedules
//            .ProjectTo<ScheduleGetByIdResponseDto>(_mapper.ConfigurationProvider)
//            .FirstOrDefaultAsync(c => c.Id == id);

//        if (scheduleResponseDto is null)
//        {
//            _logger.LogWarning("schedule  Id not found,Id {id}", id);
//            return Result.NotFound(["The schedule  is not found"]);
//        }

//        _logger.LogInformation("Fetched schedule  details");
//        return Result.Success(scheduleResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<ScheduleResponseDto>>> SearchSchedulesByTextAsync(string text)
//    {
//        var scheduleResponseDto = await _dbContext.Schedules
//            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
//            .Where(d => d.EmployeeName.Contains(text) || d.Day.Contains(text))
//            .ToListAsync();

//        _logger.LogInformation("Searching schedule . Total count: {schedule}.", scheduleResponseDto.Count);

//        return Result.Success(scheduleResponseDto);
//    }

//    /// <inheritdoc/>
//    public async Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto scheduleRequestDto)
//    {
//        var schedule = await _dbContext.Schedules.FindAsync(id);
//        var employee = _dbContext.Employees.FirstOrDefault(C => C.Id == scheduleRequestDto.EmployeeId);

//        if (employee is null)
//        {
//            _logger.LogInformation("employee or timeSlot not found");
//            return Result.Error("schedule added failed to the database");
//        }

//        if (schedule is null)
//        {
//            _logger.LogWarning("schedule  Id not found,Id {id}", id);
//            return Result.NotFound(["The schedule  is not found"]);
//        }
//        schedule.CreatedBy = _userContext.Email;
//        schedule.ServiceProvider = employee;
//        schedule.ModifiedBy = _userContext.Email;

//        await _dbContext.SaveChangesAsync();

//        var updatedSchedule = _mapper.Map<ScheduleResponseDto>(schedule);

//        _logger.LogInformation("schedule  updated successfully");
//        return Result.Success(updatedSchedule, "schedule  updated successfully");
//    }
//}
