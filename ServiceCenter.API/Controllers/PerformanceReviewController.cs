using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

/// <summary>
/// Controller responsible for handling performance review-related HTTP requests.
/// </summary>
/// <param name="performanceReviewService">The service for performing performance review-related operations.</param>
/// <seealso cref="BaseController"/>
public class PerformanceReviewController(IPerformanceReviewService performanceReviewService) : BaseController
{
    private readonly IPerformanceReviewService _performanceReviewService = performanceReviewService;

    /// <summary>
    /// Retrieves all performance reviews.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <returns>A result containing a list of performance review response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<PerformanceReviewResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<PerformanceReviewResponseDto>>> GetAllPerformanceReviews()
    {
        return await _performanceReviewService.GetAllPerformanceReviewsAsync();
    }

    /// <summary>
    /// Retrieves a performance review by its ID.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the performance review to retrieve.</param>
    /// <returns>A result containing the performance review response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PerformanceReviewResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PerformanceReviewResponseDto>> GetPerformanceReviewById(int id)
    {
        return await _performanceReviewService.GetPerformanceReviewByIdAsync(id);
    }

    /// <summary>
    /// Adds a new performance review.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="performanceReviewDto">The DTO containing the performance review details.</param>
    /// <returns>A result indicating the success of the operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddPerformanceReview(PerformanceReviewRequestDto performanceReviewDto)
    {
        return await _performanceReviewService.AddPerformanceReviewAsync(performanceReviewDto);
    }

    /// <summary>
    /// Updates an existing performance review by its ID.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the performance review to update.</param>
    /// <param name="performanceReviewDto">The DTO containing the updated performance review details.</param>
    /// <returns>A result containing the updated performance review response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PerformanceReviewResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PerformanceReviewResponseDto>> UpdatePerformanceReview(int id, PerformanceReviewRequestDto performanceReviewDto)
    {
        return await _performanceReviewService.UpdatePerformanceReviewAsync(id, performanceReviewDto);
    }

    /// <summary>
    /// Deletes a performance review by its ID.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the performance review to delete.</param>
    /// <returns>A result indicating the success of the deletion.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeletePerformanceReview(int id)
    {
        return await _performanceReviewService.DeletePerformanceReviewAsync(id);
    }
}