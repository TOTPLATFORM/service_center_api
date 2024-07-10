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
/// provides an interface for performance review-related services that manages performance review data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IPerformanceReviewService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new performance review to the database.
	/// </summary>
	/// <param name="performanceReviewRequestDto">the performance review data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the performance review addition.</returns>
	public Task<Result> AddPerformanceReviewAsync(PerformanceReviewRequestDto performanceReviewRequestDto);

	/// <summary>
	/// asynchronously retrieves all performance reviews in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of performance reviewes to retrieve</param>
	///<param name="index">index of performance reviewes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of performance review response DTOs.</returns>
	public Task<Result<List<PerformanceReviewResponseDto>>> GetAllPerformanceReviewsAsync();

	/// <summary>
	/// asynchronously retrieves a performance review by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the performance review to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the performance review response DTO.</returns>
	public Task<Result<PerformanceReviewResponseDto>> GetPerformanceReviewByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing performance review.
	/// </summary>
	/// <param name="id">the unique identifier of the performance review to update.</param>
	/// <param name="performanceReviewRequestDto">the performance review data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<PerformanceReviewResponseDto>> UpdatePerformanceReviewAsync(int id, PerformanceReviewRequestDto performanceReviewRequestDto);

   /// <summary>
    /// asynchronously deletes a performance review from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the performance review to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeletePerformanceReviewAsync(int id);
}