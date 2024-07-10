using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for order-related services that manages order data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IOrderService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new order to the database.
	/// </summary>
	/// <param name="orderRequestDto">the order data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the order addition.</returns>
	public Task<Result> AddOrderAsync(OrderRequestDto orderRequestDto);

	/// <summary>
	/// asynchronously retrieves all orders in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of orders to retrieve</param>
	///<param name="index">index of orders to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of order response DTOs.</returns>
	public Task<Result<PaginationResult<OrderResponseDto>>> GetAllOrderAsync(Status status,int ItemCount,int Index);

	/// <summary>
	/// asynchronously retrieves a order by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the order to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the order response DTO.</returns>
	public Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the status of order of an existing order.
	/// </summary>
	/// <param name="id">the unique identifier of the order to update.</param>
	/// <param name="orderRequestDto">the order data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(int id, Status status);

	/// <summary>
	/// asynchronously searches for orders based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within order data.</param>
	/// <param name = "itemCount" > item count of orders to retrieve</param>
	///<param name="index">index of orders to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of order response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<OrderResponseDto>>> SearchOrderByTextAsync(Status text,int ItemCount,int Index);

	/// <summary>
	/// asynchronously retrieves all orders in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of orderes to retrieve</param>
	///<param name="index">index of orderes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of order response DTOs.</returns>
	public Task<Result<PaginationResult<OrderResponseDto>>> GetOrdersByCustomerId(string customerId, int ItemCount, int Index);
}
