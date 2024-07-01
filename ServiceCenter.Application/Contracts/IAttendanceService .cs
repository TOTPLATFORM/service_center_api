using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// Service interface for handling attendance-related operations.
/// </summary>
public interface IAttendanceService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new attendance asynchronously.
    /// </summary>
    /// <param name="attendanceRequestDto">The data transfer object containing attendance information.</param>
    /// <returns>The result indicating the success of adding the attendance.</returns>
    public Task<Result> AddAttendanceAsync(AttendanceRequestDto attendanceRequestDto);

    /// <summary>
    /// Retrieves all attendances asynchronously.
    /// </summary>
    /// <returns>The result containing a list of attendance response data transfer objects.</returns>
    public Task<Result<PaginationResult<AttendanceResponseDto>>> GetAllAttendancesAsync(int itemCount, int index);

    /// <summary>
    /// Retrieves an attendance by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the attendance to retrieve.</param>
    /// <returns>The result containing the attendance response data transfer object.</returns>
    public Task<Result<AttendanceResponseDto>> GetAttendanceByIdAsync(int id);

    /// <summary>
    /// Updates an attendance by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the attendance to update.</param>
    /// <param name="attendanceRequestDto">The data transfer object containing updated attendance information.</param>
    /// <returns>The result containing the updated attendance response data transfer object.</returns>
    public Task<Result<AttendanceResponseDto>> UpdateAttendanceAsync(int id, AttendanceRequestDto attendanceRequestDto);

    /// <summary>
    /// Removes an attendance by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the attendance to remove.</param>
    /// <returns>The result indicating the success of removing the attendance.</returns>
    public Task<Result> DeleteAttendanceAsync(int id);

    /// <summary>
    /// Gets all attendances for a specific employee asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>A result containing a list of attendance response data transfer objects.</returns>
    public Task<Result<PaginationResult<AttendanceResponseDto>>> GetAllAttendancesForSpecificEmployeeAsync(string employeeId, int itemCount, int index);

    /// <summary>
    /// Adds a clock-in record for a specific employee asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The result indicating the success of the clock-in operation.</returns>
    public Task<Result> AddClockInAsync(string employeeId);

    /// <summary>
    /// Adds a clock-out record for a specific employee asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The result indicating the success of the clock-out operation.</returns>
    public Task<Result> AddClockOutAsync(string employeeId);
}