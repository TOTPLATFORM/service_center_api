
using ServiceCenter.Application.Services;
using ServiceCenter.Infrastructure.BaseContext;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Core.Result;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Domain.Enums;
using AutoMapper.QueryableExtensions;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Domain.Entities;

namespace hmswithlayers.application.services;

public class OrderService(ServiceCenterBaseDbContext dbcontext, IMapper mapper, ILogger<OrderService> logger, IUserContextService usercontext) : IOrderService
{
    private readonly ServiceCenterBaseDbContext _dbcontext = dbcontext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<OrderService> _logger = logger;
    private readonly IUserContextService _usercontext = usercontext;

    //<inheritdoc/>
    public async Task<Result<PaginationResult<OrderResponseDto>>> GetAllOrderAsync(Status status,int ItemCount,int index)
    {
        var ordersresponsedto = await _dbcontext.Orders
            .Where(order => order.OrderStatus == status)
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(ItemCount,index);

        _logger.LogInformation("fetching all orders. total count: {ordersresponsedto}.", ordersresponsedto.Data.Count);
        return Result.Success(ordersresponsedto);
    }

    //<inheritdoc/>
    public async Task<Result<OrderResponseDto>> GetOrderByIdAsync(int id)
    {
        var orderResponseDto = await _dbcontext.Orders
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(O => O.Id == id);

        if (orderResponseDto is null)
        {
            _logger.LogWarning($"order with id {id} was not found while attempting to fetch by id");
            return Result.NotFound(["the order is not found"]);
        }

        _logger.LogInformation("fetched one order");
        return Result.Success(orderResponseDto);
    }
    //<inheritdoc/>
    public async Task<Result> AddOrderAsync(OrderRequestDto orderdto)
    {
        var order = _mapper.Map<Order>(orderdto);

        foreach (var item in orderdto.ProductOrders)
        {
            var product =await _dbcontext.Products.FirstOrDefaultAsync(P => P.Id == item.ProductId);
            product.ProductStock = product.ProductStock - item.Quantity;
        }

        order.CreatedBy = _usercontext.Email;
        _dbcontext.Orders.Add(order);
        await _dbcontext.SaveChangesAsync();

        _logger.LogInformation($"successfully placed an order : {order}");
        return Result.SuccessWithMessage("successfully placed order");
    }

   //<inheritdoc/>
    public async Task<Result<OrderResponseDto>> UpdateOrderStatusAsync(int id, Status status)
    {
        var order = await _dbcontext.Orders.FindAsync(id);

        if (order is null)
        {
            _logger.LogWarning($"order with id {id} was not found while attempting to update order status by id");
            return Result.NotFound(["the order is not found"]);
        }



        order.OrderStatus = status;
        order.ModifiedBy = _usercontext.Email;
        await _dbcontext.SaveChangesAsync();
        var orderresponsedto = _mapper.Map<OrderResponseDto>(order);

        _logger.LogInformation($"successfully update order status to: {order.OrderStatus} ");
        return Result.Success(orderresponsedto, "successfully updated order");
    }

    //<inheritdoc/>
    public async Task<Result<PaginationResult<OrderResponseDto>>> SearchOrderByTextAsync(Status text,int ItemCount,int Index)
    {
        var orders = await _dbcontext.Orders
            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
            .Where(n => n.OrderStatus.Equals(text))
            .GetAllWithPagination(ItemCount,Index);
        _logger.LogInformation("fetching search order by name . total count: {order}.", orders.Data.Count);
        return Result.Success(orders);
    }
}
