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
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class AttendanceService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<AttendanceService> logger, IUserContextService userContext) : IAttendanceService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<AttendanceService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result> AddAttendanceAsync(AttendanceRequestDto attendanceRequestDto)
    {
        var attendance = _mapper.Map<Attendance>(attendanceRequestDto);
        var employee = await _dbContext.Employees.FindAsync(attendanceRequestDto.EmployeeId);

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", attendanceRequestDto.EmployeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        attendance.Employee = employee;

        attendance.CreatedBy = _userContext.Email;
        await _dbContext.Attendances.AddAsync(attendance);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Attendance added successfully in the database");
        return Result.SuccessWithMessage("Attendance added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> AddClockInAsync(string employeeId)
    {
        var employee = await _dbContext.Employees.FindAsync(employeeId);

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", employeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        var existingClockIn = _dbContext.Attendances
            .Any(a => a.EmployeeId == employeeId && a.AttendanceDate == DateOnly.FromDateTime(DateTime.Today) && a.ClockOutTime == default);

        if (existingClockIn)
        {
            _logger.LogError("Cannot clock in again. Employee has already clocked in for today.");
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Cannot clock in again. Employee has already clocked in for today."
                }
            });
        }

        var attendance = new Attendance
        {
            ClockInTime = TimeOnly.FromDateTime(DateTime.Now),
            EmployeeId = employeeId
        };

        attendance.CreatedBy = _userContext.Email;
        await _dbContext.Attendances.AddAsync(attendance);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("ClockIn added successfully in the database");
        return Result.SuccessWithMessage("ClockIn added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> AddClockOutAsync(string employeeId)
    {
        var employee = await _dbContext.Employees.FindAsync(employeeId);

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", employeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        var lastAttendance = _dbContext.Attendances
                .Where(a => a.EmployeeId == employeeId && a.AttendanceDate == DateOnly.FromDateTime(DateTime.Today))
                .OrderByDescending(a => a.ClockInTime)
                .FirstOrDefault();

        if (lastAttendance == null || lastAttendance.ClockOutTime != default)
        {
            _logger.LogError("Cannot clock out. No clock in record found for today or already clocked out.");
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Cannot clock out. No clock in record found for today or already clocked out."
                }
            });
        }

        lastAttendance.ClockOutTime = TimeOnly.FromDateTime(DateTime.Now);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("ClockOut added successfully in the database");
        return Result.SuccessWithMessage("ClockOut added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteAttendanceAsync(int id)
    {
        var attendance = await _dbContext.Attendances.FindAsync(id);

        if (attendance is null)
        {
            _logger.LogWarning("attendance Invaild Id ,Id {id}", id);
            return Result.NotFound(["attendance Invaild Id"]);
        }

        _dbContext.Attendances.Remove(attendance);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("attendance removed successfully in the database");
        return Result.SuccessWithMessage("Attendance removed successfully ");
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<AttendanceResponseDto>>> GetAllAttendancesAsync(int itemCount, int index)
    {
        var attendances = await _dbContext.Attendances.ProjectTo<AttendanceResponseDto>(_mapper.ConfigurationProvider).GetAllWithPagination(itemCount, index);
        if (index > attendances.TotalCount)
        {
            attendances.End = attendances.TotalCount;
        }
        _logger.LogInformation("Fetching all Attendance with pagination. Total count: {Attendance}.", attendances);

        return Result.Success(attendances);
    }

    /// <inheritdoc/>
    public async Task<Result<AttendanceResponseDto>> GetAttendanceByIdAsync(int id)
    {
        var attendance = await _dbContext.Attendances
            .ProjectTo<AttendanceResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (attendance is null)
        {
            _logger.LogWarning("attendance Id not found,Id {id}", id);
            return Result.NotFound(["attendance not found"]);
        }
        _logger.LogInformation("Fetching attendance");
        return Result.Success(attendance);
    }

    /// <inheritdoc/>
    public async Task<Result<AttendanceResponseDto>> UpdateAttendanceAsync(int id, AttendanceRequestDto attendanceRequestDto)
    {
        var attendance = await _dbContext.Attendances.FindAsync(id);
        var employee = _dbContext.Employees.Find(attendanceRequestDto.EmployeeId);

        if (attendance is null)
        {
            _logger.LogWarning("attendance Id not found,Id {id}", id);
            return Result.NotFound(["Attendance not found"]);
        }

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", attendanceRequestDto.EmployeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        attendance.ModifiedBy = _userContext.Email;

        _mapper.Map(attendanceRequestDto, attendance);

        await _dbContext.SaveChangesAsync();

        var attendanceResponse = _mapper.Map<AttendanceResponseDto>(attendance);

        _logger.LogInformation("Updated attendance , Id {id}", id);

        return Result.Success(attendanceResponse);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<AttendanceResponseDto>>> GetAllAttendancesForSpecificEmployeeAsync(string employeeId, int itemCount, int index)
    {
        var attendances = await _dbContext.Attendances
            .Where(s => s.EmployeeId == employeeId)
            .ProjectTo<AttendanceResponseDto>(_mapper.ConfigurationProvider).GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching attendances. Total count: {attendance}.", attendances.TotalCount);
        return Result.Success(attendances);
    }
}