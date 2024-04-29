using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IOrderService : IApplicationService, IScopedService
{
    Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(Status status);
    Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id);
    Task<Result> AddOrderAsync(OrderRequestDto orderRequestDto);
    Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(int id , Status status);
}
