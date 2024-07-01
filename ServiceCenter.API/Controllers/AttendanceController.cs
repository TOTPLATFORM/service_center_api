using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

/// <summary>
/// Controller responsible for handling attendance-related HTTP requests.
/// </summary>
/// <param name="attendanceService">The service for performing attendance-related operations.</param>
/// <seealso cref="BaseController"/>
public class AttendanceController(IAttendanceService attendanceService) : BaseController
{
    private readonly IAttendanceService _attendanceService = attendanceService;

    /// <summary>
    /// Retrieves all attendances asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <returns>A result containing a list of attendance response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<AttendanceResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AttendanceResponseDto>>> GetAllAttendances(int itemCount, int index)
    {
        return await _attendanceService.GetAllAttendancesAsync(itemCount, index);
    }

    /// <summary>
    /// Retrieves an attendance by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the attendance to retrieve.</param>
    /// <returns>A result containing the attendance response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<AttendanceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<AttendanceResponseDto>> GetAttendanceById(int id)
    {
        return await _attendanceService.GetAttendanceByIdAsync(id);
    }

    /// <summary>
    /// Adds a new attendance asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="attendanceDto">The DTO representing the attendance to create.</param>
    /// <returns>A result indicating the outcome of the add operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddAttendance(AttendanceRequestDto attendanceDto)
    {
        return await _attendanceService.AddAttendanceAsync(attendanceDto);
    }

    /// <summary>
    /// Records a clock-in time for an employee asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="employeeId">The ID of the employee clocking in.</param>
    /// <returns>A result indicating the outcome of the clock-in operation.</returns>
    [HttpPost("in/{employeeId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddClockIn(string employeeId)
    {
        return await _attendanceService.AddClockInAsync(employeeId);
    }

    /// <summary>
    /// Records a clock-out time for an employee asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="employeeId">The ID of the employee clocking out.</param>
    /// <returns>A result indicating the outcome of the clock-out operation.</returns>
    [HttpPost("out/{employeeId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddClockOut(string employeeId)
    {
        return await _attendanceService.AddClockOutAsync(employeeId);
    }

    /// <summary>
    /// Updates an existing attendance by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the attendance to update.</param>
    /// <param name="attendanceDto">The DTO representing the updated attendance.</param>
    /// <returns>A result containing the updated attendance response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<AttendanceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<AttendanceResponseDto>> UpdateAttendance(int id, AttendanceRequestDto attendanceDto)
    {
        return await _attendanceService.UpdateAttendanceAsync(id, attendanceDto);
    }

    /// <summary>
    /// Deletes an attendance by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the attendance to delete.</param>
    /// <returns>A result indicating the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Admin")]
    public async Task<Result> DeleteAttendance(int id)
    {
        return await _attendanceService.DeleteAttendanceAsync(id);
    }

    /// <summary>
    /// Retrieves all attendances for a specific employee asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="employeeId">The ID of the employee whose attendances to retrieve.</param>
    /// <returns>A result containing a list of attendance response DTOs for the specific employee.</returns>
    [HttpGet("employeeId/{employeeId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<AttendanceResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<AttendanceResponseDto>>> GetAllAttendancesForSpecificEmployee(string employeeId, int itemCount, int index)
    {
        return await _attendanceService.GetAllAttendancesForSpecificEmployeeAsync(employeeId, itemCount, index);
    }
}