using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ServiceCenter.API.Controllers;

public class OrderController(IOrderService orderService) : BaseController
{
    private readonly IOrderService _orderService= orderService;

    /// <summary>
    /// action for Get all  orders based on the status that take status.
    /// </summary>
    /// <param name="status">The status of orders to retrieve</param>
    /// <returns>result of list from order response dto.</returns>

    [HttpGet("{status}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<OrderResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(Status status)
    {
        return await _orderService.GetAllOrdersAsync(status);
    }

    /// <summary>
    /// action for Get an order by id that take order id.
    /// </summary>
    /// <param name="id">order id</param>
    /// <returns>result of order response dto </returns>

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id)
    {
        return await _orderService.GetOrderByIdAsync(id);
    }

    /// <summary>
    /// action for Add new an order that take order request dto.
    /// </summary>
    /// <param name="orderDto">order request dto.</param>
    /// <returns>result of the order added successfully</returns>

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddOrderAsync(OrderRequestDto orderDto)
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(Status status, int id)
    {
        return await _orderService.UpdateOrderStatusAsync(id,status);
    }
}
