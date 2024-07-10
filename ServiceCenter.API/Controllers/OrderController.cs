using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ServiceCenter.Core.Entities;

namespace ServiceCenter.API.Controllers;

public class OrderController(IOrderService orderService) : BaseController
{
    private readonly IOrderService _orderService = orderService;

    /// <summary>
    /// action for Get all  orders based on the status that take status.
    /// </summary>
    /// <param name="status">The status of orders to retrieve</param>
    /// <param name="ItemCount">item count of orders to retrieve</param>
    /// <param name="Index">index of orders to retrieve</param>
    /// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
   /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<OrderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<OrderResponseDto>>> GetAllOrders(Status status,int ItemCount,int Index)
    {
        return await _orderService.GetAllOrderAsync(status,ItemCount,Index);
    }
    /// <summary>
     /// retrieves a applicant  by their unique identifier.
     /// </summary>
     /// <param name="id">the unique identifier of the applicant .</param>
     /// <remarks>
     /// Access is limited to users with the "Admin" role.
     /// </remarks> 
     /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the applicant category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<OrderResponseDto>> GetOrderById(int id)
    {
        return await _orderService.GetOrderByIdAsync(id);
    }

    /// <summary>
    /// action for Add new an order that take order request dto.
    /// </summary>
    /// <param name="orderDto">order request dto.</param>
    /// <returns>result of the order added successfully</returns>

    [HttpPost]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddOrder(OrderRequestDto orderDto)
    {
        return await _orderService.AddOrderAsync(orderDto);
    }

    /// <summary>
    /// action for update an order status that take order status and order id.
    /// </summary>
    /// <param name="id">order id.</param>
    /// <param name="status">order status</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("orderId/{id}/status/{status}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OrderResponseDto>> UpdateOrderStatus(Status status, int id)
    {
        return await _orderService.UpdateOrderStatusAsync(id, status);
    }

    /// <summary>
    /// retrieves facilities by their property unique identifier.
    /// </summary>  
    /// <param name = "itemCount" > item count of applicant to retrieve</param>
    ///<param name="index">index of applicant to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Customer" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the property's facilities.</returns>

    [HttpGet("GetOrdersByCustomerId")]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result<PaginationResult<OrderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<OrderResponseDto>>> GetOrdersByCustomerId(int itemCount, int index)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return await _orderService.GetOrdersByCustomerId(customerId, itemCount, index);
    }
}
