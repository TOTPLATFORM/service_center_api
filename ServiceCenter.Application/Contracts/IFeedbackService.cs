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
/// provides an interface for feedback-related services that manages feedback data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IFeedbackService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new feedback to the database.
    /// </summary>
    /// <param name="feedbackRequestDto">the feedback data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the feedback addition.</returns>
    public Task<Result> AddFeedbackAsync(FeedbackRequestDto FeedbackRequestDto);
    /// <summary>
    /// asynchronously retrieves all feedbacks in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of feedback response DTOs.</returns>
    public Task<Result<PaginationResult<FeedbackResponseDto>>> GetAllFeedbackAsync(int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves a feedback by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the feedback to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the feedback response DTO.</returns>
    public Task<Result<FeedbackResponseDto>> GetFeedbackByIdAsync(int id);
    /// <summary>
    /// asynchronously updates the data of an existing feedback.
    /// </summary>
    /// <param name="id">the unique identifier of the feedback to update.</param>
    /// <param name="feedbackRequestDto">the feedback data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<FeedbackResponseDto>> UpdateFeedbackAsync(int id, FeedbackRequestDto FeedbackRequestDto);
    /// <summary>
    /// asynchronously deletes a feedback from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the feedback to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteFeedbackAsync(int id);
    /// <summary>
    /// asynchronously retrieves feedbacks by customer unique identifier.
    /// </summary>
    /// <param name="customerId">the unique identifier of the customer to retrieve its feedbacks.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all feedbacks by customer id operation.</returns>
    public Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificCustomerAsync(string customerId, int itemCount, int index);
    public Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificProductAsync(int ProductId, int itemCount, int index);
    public Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificServiceAsync(int ProductId, int itemCount, int index);
}
