﻿using Microsoft.AspNetCore.Authorization;
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
    /// get all Feedback categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
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
    /// Access is limited to users with the "Admin,Manager" role.
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
    /// delete  Feedback  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Customer" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteFeedbackAsycn(int id)
    {
        return await _FeedbackService.DeleteFeedbackAsync(id);
    }
     /// <summary>
     /// search  feedback by customer in the system.
     /// </summary>
     ///<param name="customerId">customer id </param>
     /// <remarks>
     /// Access is limited to users with the "Admin,Customer,Manager" role.
     /// </remarks>
     /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("searchByCustomer/{customerId}")]
    [Authorize(Roles = "Admin,Manager,Customer")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificCustomer(string customerId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificCustomerAsync(customerId, itemCount,  index);
    }
    /// <summary>
     /// search  feedback by product in the system.
     /// </summary>
     ///<param name="productId">product id  </param>
     /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("searchByProduct/{productId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificProduct(int productId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificProductAsync(productId, itemCount, index);
    }
    /// <summary>
     /// search  feedback by service in the system.
     /// </summary>
     ///<param name="serviceId">service id </param>
     /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("searchByService/{serviceId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<FeedbackResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificService(int serviceId, int itemCount, int index)
    {
        return await _FeedbackService.GetFeedbacksForSpecificServiceAsync(serviceId, itemCount, index);
    }

}
