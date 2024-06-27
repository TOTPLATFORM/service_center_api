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
    /// <returns>result for Subscription  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSubscription(SubscriptionRequestDto SubscriptionDto)
    {
        return await _SubscriptionService.AddSubscriptionAsync(SubscriptionDto);
    }


    /// <summary>
    /// get all Subscription in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<SubscriptionResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<SubscriptionResponseDto>>> GetAllSubscription(int itemCount, int index)
    {
        return await _SubscriptionService.GetAllSubscriptionAsync(itemCount, index);
    }
    /// <summary>
    /// get Subscription by id in the system.
    /// </summary>
    ///<param name="id">id of Subscription.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Employee,Manager,Admin")]
    [ProducesResponseType(typeof(Result<SubscriptionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<SubscriptionResponseDto>> GetSubscriptionById(int id)
    {
        return await _SubscriptionService.GetSubscriptionByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Subscription.</param>
    ///<param name="SubscriptionRequestDto">Subscription dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Employee,Manager,Admin")]
    [ProducesResponseType(typeof(Result<SubscriptionResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SubscriptionResponseDto>> UpdateSubscription(int id, SubscriptionRequestDto SubscriptionRequestDto)
    {
        return await _SubscriptionService.UpdateSubscriptionAsync(id, SubscriptionRequestDto);
    }
    /// <summary>
    /// delete  Subscription  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteSubscriptionAsycn(int id)
    {
        return await _SubscriptionService.DeleteSubscriptionAsync(id);
    }
    [HttpGet("searchByCustomer/{subscriptionId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<SubscriptionResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<SubscriptionResponseDto>>> SearchSubscriptionByrelation(string customerId, int itemCount, int index)
    {
        return await _SubscriptionService.GetSubscriptionsForSpecificCustomerAsync(customerId, itemCount, index);
    }
}
