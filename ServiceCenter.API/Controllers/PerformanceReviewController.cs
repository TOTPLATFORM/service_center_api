using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class PerformanceReviewController(IPerformanceReviewService performanceReviewService) : BaseController
{
    private readonly IPerformanceReviewService _performanceReviewService = performanceReviewService;

    /// <summary>
    /// retrieves all performance review in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of performance review to retrieve</param>
    ///<param name="index">index of performance review to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all performance review.</returns> [HttpGet]

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<PerformanceReviewResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<PerformanceReviewResponseDto>>> GetAllPerformanceReviews(int itemCount,int index)
    {
        return await _performanceReviewService.GetAllPerformanceReviewsAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a performance review  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the performance review .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the performance review category details.</returns>[HttpGet("{id}")]
     [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PerformanceReviewResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PerformanceReviewResponseDto>> GetPerformanceReviewById(int id)
    {
        return await _performanceReviewService.GetPerformanceReviewByIdAsync(id);
    }

    /// <summary>
    /// Adds a new performance review asynchronously.
    /// </summary>
    /// <param name="performance reviewDto">The DTO representing the performance review to create.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns> a task that represents the asynchronous operation, which encapsulates the result of the addition process .</returns>
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
    /// deletes a performance review from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the performance review to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeletePerformanceReview(int id)
    {
        return await _performanceReviewService.DeletePerformanceReviewAsync(id);
    }
}