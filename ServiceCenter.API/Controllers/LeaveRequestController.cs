using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class LeaveRequestController(ILeaveRequestService leaveRequestService) : BaseController
{
    private readonly ILeaveRequestService _leaveRequestService = leaveRequestService;

    /// <summary>
    /// Adds a new leave request asynchronously.
    /// </summary>
    /// <param name="leaveRequestDto">The DTO representing the leave request to create.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
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
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
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
    /// <param name="id">The ID of the leave request to retrieve.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
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
    /// <param name="id">The ID of the leave request to update.</param>
    /// <param name="leaveRequestDto">The DTO representing the updated leave request.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<LeaveRequestResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LeaveRequestResponseDto>> UpdateLeaveRequest(int id, LeaveRequestRequestDto leaveRequestDto)
    {
        return await _leaveRequestService.UpdateLeaveRequestAsycn(id, leaveRequestDto);
    }

    /// <summary>
    /// Deletes a leave request by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave request to delete.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
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
    /// <param name="employeeId">The ID of the employee whose leave requests to retrieve.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Customer" role.
    /// </remarks>
    /// <returns>A result containing a list of leave request response DTOs for the specific employee.</returns>
    [HttpGet("employeeId/{employeeId}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result<List<LeaveRequestResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestsForSpecificEmployee(string employeeId, int itemCount, int index)
    {
        return await _leaveRequestService.GetAllLeaveRequestForSpecificEmployee(employeeId, itemCount, index);
    }
}