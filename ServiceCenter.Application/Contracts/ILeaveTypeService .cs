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
/// Service interface for handling leaveType-related operations.
/// </summary>
public interface ILeaveTypeService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new leave type asynchronously.
    /// </summary>
    /// <param name="leaveTypeRequestDto">The data transfer object containing leave type information.</param>
    /// <returns>The result indicating the success of adding the leave type.</returns>
    public Task<Result> AddLeaveTypeAsync(LeaveTypeRequestDto leaveTypeRequestDto);

    /// <summary>
    /// Retrieves all leave types asynchronously.
    /// </summary>
    /// <returns>The result containing a list of leave type response data transfer objects.</returns>
    public Task<Result<PaginationResult<LeaveTypeResponseDto>>> GetAllLeaveTypesAsync(int itemCount, int index);

    /// <summary>
    /// Retrieves a leave type by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave type to retrieve.</param>
    /// <returns>The result containing the leave type response data transfer object.</returns>
    public Task<Result<LeaveTypeResponseDto>> GetLeaveTypeByIdAsync(int id);

    /// <summary>
    /// Updates a leave type by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave type to update.</param>
    /// <param name="leaveTypeRequestDto">The data transfer object containing updated leave type information.</param>
    /// <returns>The result containing the updated leave type response data transfer object.</returns>
    public Task<Result<LeaveTypeResponseDto>> UpdateLeaveTypeAsycn(int id, LeaveTypeRequestDto leaveTypeRequestDto);

    /// <summary>
    /// Removes a leave type by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave type to remove.</param>
    /// <returns>The result indicating the success of removing the leave type.</returns>
    public Task<Result> DeleteLeaveTypeAsync(int id);

    /// <summary>
    /// function to search by leave type name  that take  leave type name asynchronously.
    /// </summary>
    /// <param name="text">leave type name</param>
    /// <returns>leave type response dto </returns>
    public Task<Result<PaginationResult<LeaveTypeResponseDto>>> SearchLeaveTypeByTextAsync(string text, int itemCount, int index);

}