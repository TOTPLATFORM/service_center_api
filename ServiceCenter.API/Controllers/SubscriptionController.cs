using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Subscriptions;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;
using ServiceCenter.Core.Entities;
using ServiceCenter.Application.Contracts;

namespace ServiceCenter.API.Controllers;


public class SubscriptionController(ISubscriptionService SubscriptionService) : BaseController
{
    private readonly ISubscriptionService _SubscriptionService = SubscriptionService;

    /// <summary>
    /// action for add Subscription action that take Subscription dto   
    /// </summary>
    /// <param name="SubscriptionDto">Subscription dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager,ServiceProvider")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSubscription(SubscriptionRequestDto SubscriptionDto)
    {
        return await _SubscriptionService.AddSubscriptionAsync(SubscriptionDto);
    }

    /// <summary>
    /// retrieves all subscription in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of subscription to retrieve</param>
    ///<param name="index">index of subscription to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all subscription.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin,Manager,ServiceProvider")]
    [ProducesResponseType(typeof(Result<PaginationResult<SubscriptionResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<SubscriptionResponseDto>>> GetAllSubscription(int itemCount, int index)
    {
        return await _SubscriptionService.GetAllSubscriptionAsync(itemCount, index);
    }
    /// <summary>
    /// retrieves a subscription  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the subscription .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Customer,Manager,ServiceProvider" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the subscription category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Admin,Customer,ServiceProvider")]
    [ProducesResponseType(typeof(Result<SubscriptionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<SubscriptionResponseDto>> GetSubscriptionById(int id)
    {
        return await _SubscriptionService.GetSubscriptionByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing subscription  by its ID.
    /// </summary>
    ///<param name="id">id of Subscription.</param>
    ///<param name="SubscriptionRequestDto">Subscription dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Manager,Customer,ServiceProvider" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Customer,ServiceProvider")]
    [ProducesResponseType(typeof(Result<SubscriptionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SubscriptionResponseDto>> UpdateSubscription(int id, SubscriptionRequestDto SubscriptionRequestDto)
    {
        return await _SubscriptionService.UpdateSubscriptionAsync(id, SubscriptionRequestDto);
    }

    /// <summary>
    /// deletes a subscription from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Manager,Customer" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the subscription to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteSubscriptionAsycn(int id)
    {
        return await _SubscriptionService.DeleteSubscriptionAsync(id);
    }
    /// <summary>
    /// retrieves subscription by their customer unique identifier.
    /// </summary>
    ///<param name="customerId">the unique identifier of the customer</param>  
    /// <param name = "itemCount" > item count of subscription to retrieve</param>
    ///<param name="index">index of subscription to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Customer,ServiceProvider" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the customer's subscription.</returns>

    [HttpGet("searchByCustomer/{customerId}")]
    [Authorize(Roles = "Manager,Customer,ServiceProvider")]
    [ProducesResponseType(typeof(Result<PaginationResult<SubscriptionResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<SubscriptionResponseDto>>> SearchSubscriptionByrelation(string customerId, int itemCount, int index)
    {
        return await _SubscriptionService.GetSubscriptionsForSpecificCustomerAsync(customerId, itemCount, index);
    }
}
