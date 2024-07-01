using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

/// <summary>
/// Controller responsible for handling leave type-related HTTP requests.
/// </summary>
/// <param name="leaveTypeService">The service for performing leave type-related operations.</param>
/// <seealso cref="BaseController"/>
public class LeaveTypeController(ILeaveTypeService leaveTypeService) : BaseController
{
    private readonly ILeaveTypeService _leaveTypeService = leaveTypeService;

    /// <summary>
    /// Adds a new leave type asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="leaveTypeDto">The DTO representing the leave type to create.</param>
    /// <returns>A result indicating the outcome of the add operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddLeaveType(LeaveTypeRequestDto leaveTypeDto)
    {
        return await _leaveTypeService.AddLeaveTypeAsync(leaveTypeDto);
    }

    /// <summary>
    /// Retrieves all leave types asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <returns>A result containing a list of leave type response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<LeaveTypeResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveTypeResponseDto>>> GetAllLeaveTypes(int itemCount, int index)
    {
        return await _leaveTypeService.GetAllLeaveTypesAsync(itemCount, index);
    }

    /// <summary>
    /// Retrieves a leave type by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the leave type to retrieve.</param>
    /// <returns>A result containing the leave type response DTO.</returns>
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
    /// <remarks>Available to users with the roles: Admin, LeaveType.</remarks>
    /// <param name="id">The ID of the leave type to update.</param>
    /// <param name="leaveTypeDto">The DTO representing the updated leave type.</param>
    /// <returns>A result containing the updated leave type response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,LeaveType")]
    [ProducesResponseType(typeof(Result<LeaveTypeResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<LeaveTypeResponseDto>> UpdateLeaveType(int id, LeaveTypeRequestDto leaveTypeDto)
    {
        return await _leaveTypeService.UpdateLeaveTypeAsycn(id, leaveTypeDto);
    }

    /// <summary>
    /// Deletes a leave type by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the leave type to delete.</param>
    /// <returns>A result indicating the outcome of the deletion operation.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteLeaveType(int id)
    {
        return await _leaveTypeService.DeleteLeaveTypeAsync(id);
    }

    /// <summary>
    /// Searches for leave types by text asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="typeName">The text to search for in leave types.</param>
    /// <returns>A result containing a list of leave type response DTOs matching the search text.</returns>
    [HttpGet("search/{typeName}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<LeaveTypeResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<LeaveTypeResponseDto>>> SearchLeaveTypeByText(string typeName, int itemCount, int index)
    {
        return await _leaveTypeService.SearchLeaveTypeByTextAsync(typeName, itemCount, index);
    }
}