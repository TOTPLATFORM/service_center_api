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
{/// <summary>
 /// function to add  order that take  orderDto   
 /// </summary>
 /// <param name="orderRequestDto">order request dto</param>
 /// <returns> order added successfully </returns>
    public Task<Result> AddOrderAsync(OrderRequestDto orderRequestDto);
    /// <summary>
    /// function to get all order 
    /// </summary>
    ///   <param name="status">order status</param>
    /// <returns>list all order response dto </returns>
    public Task<Result<PaginationResult<OrderResponseDto>>> GetAllOrderAsync(Status status,int ItemCount,int Index);
    /// <summary>
    /// function to get  order by id that take   order id
    /// </summary>
    /// <param name="id"> order id</param>
    /// <returns> order response dto</returns>
    public Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id);
    /// <summary>
    /// function to update order that take orderRequestDto   
    /// </summary>
    /// <param name="id">order id</param>
    ///   <param name="status">order status</param>
    /// <returns>Updated order </returns>
    public Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(int id, Status status);

    /// <summary>
    /// function to search by order status  that take  order status
    /// </summary>
    /// <param name="text">order status</param>
    /// <returns>order response dto </returns>
    public Task<Result<PaginationResult<OrderResponseDto>>> SearchOrderByTextAsync(Status text,int ItemCount,int Index);

}
