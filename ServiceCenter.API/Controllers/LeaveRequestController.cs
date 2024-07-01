using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

/// <summary>
/// Controller responsible for handling leave request-related HTTP requests.
/// </summary>
/// <param name="leaveRequestService">The service for performing leave request-related operations.</param>
/// <seealso cref="BaseController"/>
public class LeaveRequestController(ILeaveRequestService leaveRequestService) : BaseController
{
    private readonly ILeaveRequestService _leaveRequestService = leaveRequestService;

    /// <summary>
    /// Adds a new leave request asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="leaveRequestDto">The DTO representing the leave request to create.</param>
    /// <returns>A result indicating the outcome of the add operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddLeaveRequest(LeaveRequestRequestDto leaveRequestDto)
    {
        return await _leaveRequestService.AddLeaveRequestAsync(leaveRequestDto);
    }

    /// <summary>
    /// Retrieves all leave requests asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <returns>A result containing a list of leave request response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<LeaveRequestResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequests(int itemCount, int index)
    {
        return await _leaveRequestService.GetAllLeaveRequestsAsync(itemCount, index);
    }

    /// <summary>
    /// Retrieves a leave request by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the leave request to retrieve.</param>
    /// <returns>A result containing the leave request response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<LeaveRequestResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LeaveRequestResponseDto>> GetLeaveRequestById(int id)
    {
        return await _leaveRequestService.GetLeaveRequestByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing leave request by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the roles: Admin, LeaveRequest.</remarks>
    /// <param name="id">The ID of the leave request to update.</param>
    /// <param name="leaveRequestDto">The DTO representing the updated leave request.</param>
    /// <returns>A result containing the updated leave request response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,LeaveRequest")]
    [ProducesResponseType(typeof(Result<LeaveRequestResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LeaveRequestResponseDto>> UpdateLeaveRequest(int id, LeaveRequestRequestDto leaveRequestDto)
    {
        return await _leaveRequestService.UpdateLeaveRequestAsycn(id, leaveRequestDto);
    }

    /// <summary>
    /// Deletes a leave request by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the leave request to delete.</param>
    /// <returns>A result indicating the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteLeaveRequest(int id)
    {
        return await _leaveRequestService.DeleteLeaveRequestAsync(id);
    }

    /// <summary>
    /// Retrieves all leave requests for a specific employee asynchronously.
    /// </summary>
    /// <remarks>Available to users with the roles: Admin, LeaveRequest, Patient.</remarks>
    /// <param name="employeeId">The ID of the employee whose leave requests to retrieve.</param>
    /// <returns>A result containing a list of leave request response DTOs for the specific employee.</returns>
    [HttpGet("employeeId/{employeeId}")]
    [Authorize(Roles = "Admin,LeaveRequest,Patient")]
    [ProducesResponseType(typeof(Result<List<LeaveRequestResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestsForSpecificEmployee(string employeeId, int itemCount, int index)
    {
        return await _leaveRequestService.GetAllLeaveRequestForSpecificEmployee(employeeId, itemCount, index);
    }
}