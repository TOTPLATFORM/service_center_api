using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class LeaveTypeController(ILeaveTypeService leaveTypeService) : BaseController
{
    private readonly ILeaveTypeService _leaveTypeService = leaveTypeService;

    /// <summary>
    /// Adds a new leave type asynchronously.
    /// </summary>
    /// <param name="leaveTypeDto">The DTO representing the leave type to create.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddLeaveType(LeaveTypeRequestDto leaveTypeDto)
    {
        return await _leaveTypeService.AddLeaveTypeAsync(leaveTypeDto);
    }
    /// <summary>
    /// retrieves all leave type in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of leave type to retrieve</param>
    ///<param name="index">index of leave type to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all leave type.</returns> [HttpGet]

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<LeaveTypeResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveTypeResponseDto>>> GetAllLeaveTypes(int itemCount, int index)
    {
        return await _leaveTypeService.GetAllLeaveTypesAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves a leave type  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the leave type .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the leave type category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<LeaveTypeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LeaveTypeResponseDto>> GetLeaveTypeById(int id)
    {
        return await _leaveTypeService.GetLeaveTypeByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing leave type by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the leave type to update.</param>
    /// <param name="leaveTypeDto">The DTO representing the updated leave type.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<LeaveTypeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LeaveTypeResponseDto>> UpdateLeaveType(int id, LeaveTypeRequestDto leaveTypeDto)
    {
        return await _leaveTypeService.UpdateLeaveTypeAsync(id, leaveTypeDto);
    }


    /// <summary>
    /// deletes a leave type from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the leave type to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteLeaveType(int id)
    {
        return await _leaveTypeService.DeleteLeaveTypeAsync(id);
    }

    /// <summary>
    /// searches leave type  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of leave type to retrieve</param>
    ///<param name="index">index of leave type to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of offer  that match the search criteria.</returns>

    [HttpGet("search/{typeName}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<LeaveTypeResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveTypeResponseDto>>> SearchLeaveTypeByText(string text, int itemCount, int index)
    {
        return await _leaveTypeService.SearchLeaveTypeByTextAsync(text, itemCount, index);
    }
}