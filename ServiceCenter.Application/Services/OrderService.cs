using ServiceCenter.Core.Result;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ServiceCenter.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;

namespace HMSWithLayers.Application.Services;

public class OrderService(IItemService itemService, ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<OrderService> logger, IUserContextService userContext) : IOrderService
{
    private readonly IItemService _itemService = itemService;
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<OrderService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <summary>
    /// Gets all orders asynchronously.
    /// </summary>
    /// <returns>A Result containing order response DTOs.</returns>
    public async Task<Result<List<OrderResponseDto>>> GetAllOrdersAsync(Status status)
    {
        var ordersResponseDto = await _dbContext.Order
            .Where(order => order.OrderStatus == status)
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        _logger.LogInformation("Fetching all orders. Total count: {ordersResponseDto}.", ordersResponseDto.Count);
        return Result.Success(ordersResponseDto);
    }

    /// <summary>
    /// Gets an Order by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>A Result containing order response DTO or NotFound Result if order is not found.</returns>
    public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id)
    {
        var orderResponseDto = await _dbContext.Order
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (orderResponseDto is null)
        {
            _logger.LogWarning($"Order with id {id} was not found while attempting to fetch by id");
            return Result.NotFound(["The order is not found"]);
        }

        _logger.LogInformation("Fetched one order");
        return Result.Success(orderResponseDto);
    }

    /// <summary>
    /// Creates order asynchronously.
    /// </summary>
    /// <param name="orderDto">The Order to create.</param>
    /// <returns>A Result containing order response DTO or Result returned from item service.</returns>
    public async Task<Result> AddOrderAsync(OrderRequestDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);

        var result = await _itemService.DecreaseItemsQuantity(orderDto.ItemOrders);

        if (!result.IsSuccess)
        {
            return result;
        }

        order.CreatedBy = _userContext.Email;
        _dbContext.Order.Add(order);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully placed an order : {order}");
        return Result.SuccessWithMessage("Successfully placed order");
    }

    /// <summary>
    /// Updates the status of an order asynchronously.
    /// </summary>
    /// <param name="status">The new status of the order.</param>
    /// <param name="id">The ID of the order to update.</param>
    /// <returns>A Result containing the updated order response DTO.</returns>
    public async Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(int id,Status status)
    {
        var order = await _dbContext.Order.FindAsync(id);

        if (order is null)
        {
            _logger.LogWarning($"Order with id {id} was not found while attempting to update order status by id");
            return Result.NotFound(["The order is not found"]);
        }

        if (status == Status.Cancelled)
        {
            var result = await _itemService.IncreaseItemsQuantity(_mapper.Map<List<ItemOrderRequestDto>>(order.ItemOrders));

            if (!result.IsSuccess)
            {
                return result;
            }
        }

        var previousOrderStatus = order.OrderStatus;
        order.OrderStatus = status;
        order.ModifiedBy = _userContext.Email;
        await _dbContext.SaveChangesAsync();
        var orderResponseDto = _mapper.Map<OrderResponseDto>(order);

        _logger.LogInformation($"Successfully update order status to: {order.OrderStatus} from: {previousOrderStatus}");
        return Result.Success(orderResponseDto, "Successfully updated order");
    }
}
