using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class FeedbackController(IFeedbackService FeedbackService) : BaseController
{
    private readonly IFeedbackService _FeedbackService = FeedbackService;

    /// <summary>
    /// action for add Feedback  action that take  Feedback dto   
    /// </summary>
    /// <param name="FeedbackDto">Feedback  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Customer" role.
    /// </remarks>   
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddFeedback(FeedbackRequestDto FeedbackDto)
    {
        return await _FeedbackService.AddFeedbackAsync(FeedbackDto);
    }
    /// <summary>
    /// retrieves all feedback in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of feedback to retrieve</param>
    ///<param name="index">index of feedback to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all feedback.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetAllFeedback(int itemCount, int index)
    {
        return await _FeedbackService.GetAllFeedbackAsync( itemCount,  index);
    }
    /// <summary>
    /// retrieves a feedback  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the feedback .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the feedback category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<FeedbackResponseDto>> GetFeedbackById(int id)
    {
        return await _FeedbackService.GetFeedbackByIdAsync(id);
    }
    /// <summary>
    /// updates an existing expense's information.
    /// </summary>
    ///<param name="id">id of Feedback.</param>
    ///<param name="feedbackDesc">Feedback desc.</param>
    /// <remarks>
    /// Access is limited to users with the "Customer" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<FeedbackResponseDto>> UpdateFeedbackDesc(int id, string feedbackDesc)
    {
        return await _FeedbackService.UpdateFeedbackDescAsync(id, feedbackDesc);
    }

    /// <summary>
    /// deletes a feedback from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Customer" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the feedback to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteFeedbackAsycn(int id)
    {
        return await _FeedbackService.DeleteFeedbackAsync(id);
    }
    /// <summary>
    /// retrieves feedbacks by their customer unique identifier.
    /// </summary>
    ///<param name="customerId">the unique identifier of the customer</param>  
    /// <param name = "itemCount" > item count of feedback to retrieve</param>
    ///<param name="index">index of feedback to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin,Customer" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the customer's feedback</returns>
    [HttpGet("searchByCustomer/{customerId}")]
    [Authorize(Roles = "Admin,Manager,Customer")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificCustomer(string customerId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificCustomerAsync(customerId, itemCount,  index);
    }
    /// <summary> 
    /// retrieves feedbacks by their product unique identifier.
    /// </summary>
    ///<param name="productId">the unique identifier of the product</param>  
    /// <param name = "itemCount" > item count of feedback to retrieve</param>
    ///<param name="index">index of feedback to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product's feedback</returns>

    [HttpGet("searchByProduct/{productId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificProduct(int productId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificProductAsync(productId, itemCount, index);
    }
    /// <summary>
    /// retrieves feedbacks by their service unique identifier.
    /// </summary>
    ///<param name="serviceId">the unique identifier of the service</param>  
    /// <param name = "itemCount" > item count of feedback to retrieve</param>
    ///<param name="index">index of feedback to retrieve</param>
    /// <returns>>a task that represents the asynchronous operation, which encapsulates the result containing the service's feedback.</returns>
    [HttpGet("searchByService/{serviceId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificService(int serviceId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificServiceAsync(serviceId, itemCount, index);
    }

}
