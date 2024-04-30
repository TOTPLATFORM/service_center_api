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

    ///<inheritdoc/>
    public async Task<Result<List<OrderResponseDto>>> GetAllOrderAsync(Status status)
    {
        var ordersResponseDto = await _dbContext.Orders
            .Where(order => order.OrderStatus == status)
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        _logger.LogInformation("Fetching all orders. Total count: {ordersResponseDto}.", ordersResponseDto.Count);
        return Result.Success(ordersResponseDto);
    }

    ///<inheritdoc/>
    public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id)
    {
        var orderResponseDto = await _dbContext.Orders
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
    ///<inheritdoc/>
    public async Task<Result> AddOrderAsync(OrderRequestDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);

        var result = await _itemService.DecreaseItemsQuantity(orderDto.ItemOrders);

        if (!result.IsSuccess)
        {
            return result;
        }

        order.CreatedBy = _userContext.Email;
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully placed an order : {order}");
        return Result.SuccessWithMessage("Successfully placed order");
    }

    ///<inheritdoc/>
    public async Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(int id,Status status)
    {
        var order = await _dbContext.Orders.FindAsync(id);

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

  
    public async Task<Result<List<OrderResponseDto>>> SearchOrderByTextAsync(Status text)
    {
        var orders = await _dbContext.Users.OfType<Order>()
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.OrderStatus.Equals(text))
            .ToListAsync();
        _logger.LogInformation("Fetching search Order by name . Total count: {order}.", orders.Count);
        return Result.Success(orders);
    }
}
