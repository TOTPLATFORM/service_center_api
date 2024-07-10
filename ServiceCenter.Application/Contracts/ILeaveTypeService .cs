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
/// provides an interface for leavetype-related services that manages item data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface ILeaveTypeService : IApplicationService, IScopedService
{
	/// <summary>
	/// Adds a new leave type asynchronously.
	/// </summary>
	/// <param name="leaveTypeRequestDto">The data transfer object containing leave type information.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the leave type addition.</returns>
	public Task<Result> AddLeaveTypeAsync(LeaveTypeRequestDto leaveTypeRequestDto);

	/// <summary>
	/// Retrieves all leave types asynchronously.
	/// </summary>
	/// <param name = "itemCount" > item count of leave typees to retrieve</param>
	///<param name="index">index of leave typees to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of leave type response DTOs.</returns>
	public Task<Result<PaginationResult<LeaveTypeResponseDto>>> GetAllLeaveTypesAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a leave type by their unique identifier.
	/// </summary>
	/// <param name="id">The ID of the leave type to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the leave type response DTO.</returns>
	public Task<Result<LeaveTypeResponseDto>> GetLeaveTypeByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing leave type.
	/// </summary>
	/// <param name="id">The ID of the leave type to update.</param>
	/// <param name="leaveTypeRequestDto">The data transfer object containing updated leave type information.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<LeaveTypeResponseDto>> UpdateLeaveTypeAsync(int id, LeaveTypeRequestDto leaveTypeRequestDto);

	/// <summary>
	/// asynchronously deletes a leave type from the system by their unique identifier.
	/// </summary>
	/// <param name="id">The ID of the leave type to remove.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteLeaveTypeAsync(int id);

	/// <summary>
	/// function to search by leave type name  that take  leave type name asynchronously.
	/// </summary>
	/// <param name="text">leave type name</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of leave type response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<LeaveTypeResponseDto>>> SearchLeaveTypeByTextAsync(string text, int itemCount, int index);

}