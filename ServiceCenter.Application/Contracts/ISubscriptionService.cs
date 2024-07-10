using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Subscriptions;

/// <summary>
/// provides an interface for subscription-related services that manages subscription data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface ISubscriptionService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new subscription to the database.
    /// </summary>
    /// <param name="subscriptionRequestDto">the subscription data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the subscription addition.</returns>
    public Task<Result> AddSubscriptionAsync(SubscriptionRequestDto subscriptionRequestDto);

	/// <summary>
	/// asynchronously retrieves all subscriptions in the system.
	/// </summary>
	/// <param name = "subscriptionitemCount" > subscription count of subscriptions to retrieve</param>
	///<param name="index">index of subscriptions to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of subscription response DTOs.</returns>
	public Task<Result<PaginationResult<SubscriptionResponseDto>>> GetAllSubscriptionAsync(int subscriptionitemCount, int index);
    /// <summary>
    /// asynchronously retrieves a subscription by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the subscription to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the subscription response DTO.</returns>
    public Task<Result<SubscriptionResponseDto>> GetSubscriptionByIdAsync(int id);

    /// <summary>
    /// asynchronously updates the data of an existing subscription.
    /// </summary>
    /// <param name="id">the unique identifier of the subscription to update.</param>
    /// <param name="subscriptionRequestDto">the subscription data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<SubscriptionResponseDto>> UpdateSubscriptionAsync(int id, SubscriptionRequestDto subscriptionRequestDto);
	/// <summary>
	/// asynchronously deletes a subscription from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the subscription to delete.</param>
	/// <param name = "subscriptionitemCount" > subscription count of subscriptions to retrieve</param>
	///<param name="index">index of subscriptions to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteSubscriptionAsync(int id);
    /// <summary>
    /// function to search by Product   that take  Product category name
    /// </summary>
    /// <param name="text">Product  name</param>
    /// <returns>Product response dto </returns>
    public Task<Result<PaginationResult<SubscriptionResponseDto>>> GetSubscriptionsForSpecificCustomerAsync(string customerId, int subscriptionitemCount, int index);
}
