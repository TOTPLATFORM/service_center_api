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
/// Service interface for handling performance review-related operations.
/// </summary>
public interface IPerformanceReviewService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new performanceReview.
    /// </summary>
    /// <param name="performanceReviewRequestDto">The data transfer object containing performanceReview information.</param>
    /// <returns>The result indicating the success of adding the performanceReview.</returns>
    public Task<Result> AddPerformanceReviewAsync(PerformanceReviewRequestDto performanceReviewRequestDto);

    /// <summary>
    /// Retrieves all performanceReviews.
    /// </summary>
    /// <returns>The result containing a list of performanceReview response data transfer objects.</returns>
    public Task<Result<PaginationResult<PerformanceReviewResponseDto>>> GetAllPerformanceReviewsAsync(int itemCount ,int index);

    /// <summary>
    /// Retrieves a performanceReview by its ID.
    /// </summary>
    /// <param name="id">The ID of the performanceReview to retrieve.</param>
    /// <returns>The result containing the performanceReview response data transfer object.</returns>
    public Task<Result<PerformanceReviewResponseDto>> GetPerformanceReviewByIdAsync(int id);

    /// <summary>
    /// Updates a performanceReview by its ID.
    /// </summary>
    /// <param name="id">The ID of the performanceReview to update.</param>
    /// <param name="performanceReviewRequestDto">The data transfer object containing updated performanceReview information.</param>
    /// <returns>The result containing the updated performanceReview response data transfer object.</returns>
    public Task<Result<PerformanceReviewResponseDto>> UpdatePerformanceReviewAsync(int id, PerformanceReviewRequestDto performanceReviewRequestDto);

    /// <summary>
    /// Removes a performanceReview by its ID.
    /// </summary>
    /// <param name="id">The ID of the performanceReview to remove.</param>
    /// <returns>The result indicating the success of removing the performanceReview.</returns>
    public Task<Result> DeletePerformanceReviewAsync(int id);
}