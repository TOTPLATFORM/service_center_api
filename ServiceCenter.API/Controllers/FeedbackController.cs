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
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Feedback  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddFeedback(FeedbackRequestDto FeedbackDto)
    {
        return await _FeedbackService.AddFeedbackAsync(FeedbackDto);
    }
    /// <summary>
    /// get all Feedback categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetAllFeedback(int itemCount, int index)
    {
        return await _FeedbackService.GetAllFeedbackAsync( itemCount,  index);
    }
    /// <summary>
    /// get Feedback by id in the system.
    /// </summary>
    ///<param name="id">id of Feedback.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<FeedbackResponseDto>> GetFeedbackById(int id)
    {
        return await _FeedbackService.GetFeedbackByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Feedback.</param>
    ///<param name="FeedbackRequestDto">Feedback dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<FeedbackResponseDto>> UpdateFeedback(int id, FeedbackRequestDto FeedbackRequestDto)
    {
        return await _FeedbackService.UpdateFeedbackAsync(id, FeedbackRequestDto);
    }
    /// <summary>
    /// delete  Feedback  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteFeedbackAsycn(int id)
    {
        return await _FeedbackService.DeleteFeedbackAsync(id);
    }
    /// </summary>
    ///<param name="text">customer id </param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("searchByFeedbacks/{customerId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificCustomer(string customerId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificCustomerAsync(customerId, itemCount,  index);
    }
    /// </summary>
    ///<param name="text">product id  </param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("searchByFeedbacks/{productId}")]
    [Authorize(Roles = "Admin,Customer,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificProduct(int productId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificProductAsync(productId, itemCount, index);
    }
    /// </summary>
    ///<param name="text">service id </param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("searchByFeedbacks/{serviceId}")]
    [Authorize(Roles = "Admin,Customer,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificService(int serviceId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificServiceAsync(serviceId, itemCount, index);
    }

}
