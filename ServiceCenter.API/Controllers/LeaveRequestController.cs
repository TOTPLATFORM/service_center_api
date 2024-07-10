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
    /// retrieves all leave request in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of leave request to retrieve</param>
    ///<param name="index">index of leave request to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all leave request.</returns> [HttpGet]

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<LeaveRequestResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequests(int itemCount, int index)
    {
        return await _leaveRequestService.GetAllLeaveRequestsAsync(itemCount, index);
    }
    /// <summary>
    /// retrieves a leave request  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the leave request .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the leave request category details.</returns>[HttpGet("{id}")]

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
    /// deletes a leave request from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the leave request to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteLeaveRequest(int id)
    {
        return await _leaveRequestService.DeleteLeaveRequestAsync(id);
    }

    /// <summary>
    /// retrieves facilities by their property unique identifier.
    /// </summary>
    ///<param name="employeeId">the unique identifier of the property</param>  
    /// <param name = "itemCount" > item count of leave request to retrieve</param>
    ///<param name="index">index of leave request to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the property's facilities.</returns>

    [HttpGet("employeeId/{employeeId}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result<List<LeaveRequestResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestsForSpecificEmployee(string employeeId, int itemCount, int index)
    {
        return await _leaveRequestService.GetAllLeaveRequestForSpecificEmployee(employeeId, itemCount, index);
    }
}