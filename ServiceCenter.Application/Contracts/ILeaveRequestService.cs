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
/// Service interface for handling leaveRequest-related operations.
/// </summary>
public interface ILeaveRequestService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new leave request asynchronously.
    /// </summary>
    /// <param name="leave request RequestDto">The data transfer object containing leave request  information.</param>
    /// <returns>The result indicating the success of adding the leave request .</returns>
    public Task<Result> AddLeaveRequestAsync(LeaveRequestRequestDto leaveRequestDto);

    /// <summary>
    /// Retrieves all leave request asynchronously.
    /// </summary>
    /// <returns>The result containing a list of leave request  response data transfer objects.</returns>
    public Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestsAsync(int itemCount, int index);

    /// <summary>
    /// Retrieves a leave request  by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave request  to retrieve.</param>
    /// <returns>The result containing the leave request  response data transfer object.</returns>
    public Task<Result<LeaveRequestResponseDto>> GetLeaveRequestByIdAsync(int id);

    /// <summary>
    /// Updates a leave request  by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave request  to update.</param>
    /// <param name="leave request RequestDto">The data transfer object containing updated leave request  information.</param>
    /// <returns>The result containing the updated leave request  response data transfer object.</returns>
    public Task<Result<LeaveRequestResponseDto>> UpdateLeaveRequestAsycn(int id, LeaveRequestRequestDto leaveRequestDto);

    /// <summary>
    /// Removes a leave request  by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave request  to remove.</param>
    /// <returns>The result indicating the success of removing the leave request .</returns>
    public Task<Result> DeleteLeaveRequestAsync(int id);


    /// <summary>
    /// Gets all leave requests for a specific employee asynchronously.
    /// </summary>
    /// /// <param name="employeeId">leaveType id</param>
    /// <returns>A Result containing leave request s response DTOs.</returns>
    public Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestForSpecificEmployee(string employeeId, int itemCount, int index);
}