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
}
