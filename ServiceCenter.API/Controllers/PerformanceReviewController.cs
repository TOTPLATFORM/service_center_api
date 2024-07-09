using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class PerformanceReviewController(IPerformanceReviewService performanceReviewService) : BaseController
{
    private readonly IPerformanceReviewService _performanceReviewService = performanceReviewService;

    /// <summary>
    /// Retrieves all performance reviews.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
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
    /// <param name="id">The ID of the performance review to retrieve.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
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
    /// <param name="performanceReviewDto">The DTO containing the performance review details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
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
    /// <param name="id">The ID of the performance review to update.</param>
    /// <param name="performanceReviewDto">The DTO containing the updated performance review details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
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
    /// <param name="id">The ID of the performance review to delete.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
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