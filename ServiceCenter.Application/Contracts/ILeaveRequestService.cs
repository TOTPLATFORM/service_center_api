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
/// provides an interface for leaverequest-related services that manages leave request data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface ILeaveRequestService : IApplicationService, IScopedService
{
	/// <summary>
	/// Adds a new leave request asynchronously.
	/// </summary>
	/// <param name="leaveRequestDto">The data transfer object containing leave request information.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the leave request addition.</returns>
	public Task<Result> AddLeaveRequestAsync(LeaveRequestRequestDto leaveRequestDto);

	/// <summary>
	/// Retrieves all leave request asynchronously.
	/// </summary>
	/// <param name = "itemCount" > item count of leave request to retrieve</param>
	///<param name="index">index of leave request to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of leave request response DTOs.</returns>
	public Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestsAsync(int itemCount, int index);

	/// <summary>
	/// Retrieves a leave request  by its ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the leave request  to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the leave request response DTO.</returns>
	public Task<Result<LeaveRequestResponseDto>> GetLeaveRequestByIdAsync(int id);

	/// <summary>
	/// Updates a leave request  by its ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the leave request  to update.</param>
	/// <param name="leaveRequestDto">The data transfer object containing updated leave request  information.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<LeaveRequestResponseDto>> UpdateLeaveRequestAsync(int id, LeaveRequestRequestDto leaveRequestDto);

	/// <summary>
	/// Removes a leave request  by its ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the leave request  to remove.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteLeaveRequestAsync(int id);


	/// <summary>
	/// Gets all leave requests for a specific employee asynchronously.
	/// </summary>
	/// /// <param name="employeeId">leaveType id</param>
	/// <param name = "itemCount" > item count of leave request to retrieve</param>
	///<param name="index">index of leave request to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of leave request response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestForSpecificEmployee(string employeeId, int itemCount, int index);
}