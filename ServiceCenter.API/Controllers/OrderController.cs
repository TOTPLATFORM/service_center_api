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
    /// <returns>result of list from order response dto.</returns>

    [HttpGet()]
    [Authorize(Roles = "Admin,Employee")]
    [ProducesResponseType(typeof(Result<PaginationResult<OrderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<OrderResponseDto>>> GetAllOrders(Status status,int ItemCount,int Index)
    {
        return await _orderService.GetAllOrderAsync(status,ItemCount,Index);
    }

    /// <summary>
    /// action for Get an order by id that take order id.
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>result of order response dto </returns>

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Employee")]
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
    [Authorize(Roles = "Admin,Employee,Customer")]
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
    /// <returns>result of the order response dto after updated successfully</returns>

    [HttpPut("orderId/{id}/status/{status}")]
    [Authorize(Roles = "Admin,Employee")]
    [ProducesResponseType(typeof(Result<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OrderResponseDto>> UpdateOrderStatus(Status status, int id)
    {
        return await _orderService.UpdateOrderStatusAsync(id, status);
    }

    /// <summary>
    /// action for get orders by customer id.
    /// </summary>
    /// <returns>result of the order response dto after updated successfully</returns>
    [HttpPut("GetOrdersByCustomerId")]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result<PaginationResult<OrderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<OrderResponseDto>>> GetOrdersByCustomerId(int ItemCount, int Index)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return await _orderService.GetOrdersByCustomerId(customerId, ItemCount, Index);
    }
}
