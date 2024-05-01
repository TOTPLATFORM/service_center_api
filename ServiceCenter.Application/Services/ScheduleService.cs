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

public class ScheduleService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ScheduleService> logger, IUserContextService userContext) : IScheduleService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ScheduleService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddScheduleAsync(ScheduleRequestDto ScheduleRequestDto)
    {
        var result = _mapper.Map<Schedule>(ScheduleRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ScheduleRequestDto to Schedule. ScheduleRequestDto: {@ScheduleRequestDto}", ScheduleRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Schedules.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Schedule added successfully to the database");
        return Result.SuccessWithMessage("Schedule added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<ScheduleResponseDto>>> GetAllScheduleAsync()
    {
        var result = await _dbContext.Schedules
             .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Schedule. Total count: { Schedule}.", result.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ScheduleResponseDto>> GetScheduleByIdAsync(int id)
    {
        var result = await _dbContext.Schedules
            .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Schedule Id not found,Id {ScheduleId}", id);

            return Result.NotFound(["Schedule not found"]);
        }

        _logger.LogInformation("Fetching Schedule");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto ScheduleRequestDto)
    {
        var result = await _dbContext.Schedules.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Schedule Id not found,Id {ScheduleId}", id);
            return Result.NotFound(["Schedule not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ScheduleRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ScheduleResponse = _mapper.Map<ScheduleResponseDto>(result);
        if (ScheduleResponse is null)
        {
            _logger.LogError("Failed to map ScheduleRequestDto to ScheduleResponseDto. ScheduleRequestDto: {@ScheduleRequestDto}", ScheduleResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Schedule , Id {Id}", id);

        return Result.Success(ScheduleResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteScheduleAsync(int id)
    {
        var Schedule = await _dbContext.Schedules.FindAsync(id);

        if (Schedule is null)
        {
            _logger.LogWarning("Schedule Invaild Id ,Id {ScheduleId}", id);
            return Result.NotFound(["Schedule Invaild Id"]);
        }

        _dbContext.Schedules.Remove(Schedule);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Schedule removed successfully in the database");
        return Result.SuccessWithMessage("Schedule removed successfully");
    }
    public async Task<Result<List<ScheduleResponseDto>>> GetAllSchedulesForSpecificEmployee(string employeeId)
    {
        var Schedules = await _dbContext.Schedules
              .Where(s => s.Employee.Id == employeeId)
              .ProjectTo<ScheduleResponseDto>(_mapper.ConfigurationProvider)
              .ToListAsync();

        _logger.LogInformation("Fetching Schedules. Total count: {Schedules}.", Schedules.Count);
        return Result.Success(Schedules);
    }
}
