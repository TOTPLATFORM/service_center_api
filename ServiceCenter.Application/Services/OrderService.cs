
//using ServiceCenter.Application.Services;
//using ServiceCenter.Infrastructure.BaseContext;
//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Core.Result;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Domain.Enums;
//using AutoMapper.QueryableExtensions;

//namespace hmswithlayers.application.services;

//public class orderservice(ItemService itemservice, ServiceCenterBaseDbContext dbcontext, IMapper mapper, ILogger<orderservice> logger, IUserContextService usercontext) : IOrderService
//{
//    private readonly ItemService _itemservice = itemservice;
//    private readonly ServiceCenterBaseDbContext _dbcontext = dbcontext;
//    private readonly IMapper _mapper = mapper;
//    private readonly ILogger<orderservice> _logger = logger;
//    private readonly IUserContextService _usercontext = usercontext;

//    //<inheritdoc/>
//    public async Task<Result<List<OrderResponseDto>>> GetAllOrderAsync(Status status)
//    {
//        var ordersresponsedto = await _dbcontext.Orders
//            .Where(order => order.OrderStatus == status)
//            .ProjectTo<OrderResponseDto>(_mapper.ConfigurationProvider)
//            .tolistasync();

//        _logger.loginformation("fetching all orders. total count: {ordersresponsedto}.", ordersresponsedto.count);
//        return result.success(ordersresponsedto);
//    }

//    //<inheritdoc/>
//    public async task<result<orderresponsedto>> getorderbyidasync(int id)
//    {
//        var orderresponsedto = await _dbcontext.orders
//            .projectto<orderresponsedto>(_mapper.configurationprovider)
//            .firstordefaultasync(item => item.id == id);

//        if (orderresponsedto is null)
//        {
//            _logger.logwarning($"order with id {id} was not found while attempting to fetch by id");
//            return result.notfound(["the order is not found"]);
//        }

//        _logger.loginformation("fetched one order");
//        return result.success(orderresponsedto);
//    }
//    /<inheritdoc/>
//    public async task<result> addorderasync(orderrequestdto orderdto)
//    {
//        var order = _mapper.map<order>(orderdto);

//        var result = await _itemservice.decreaseitemsquantity(orderdto.itemorders);

//        if (!result.issuccess)
//        {
//            return result;
//        }

//        order.createdby = _usercontext.email;
//        _dbcontext.orders.add(order);
//        await _dbcontext.savechangesasync();

//        _logger.loginformation($"successfully placed an order : {order}");
//        return result.successwithmessage("successfully placed order");
//    }

//<inheritdoc/>
//public async task<result<orderresponsedto>> updateorderstatusasync(int id, status status)
//    {
//        var order = await _dbcontext.orders.findasync(id);

//        if (order is null)
//        {
//            _logger.logwarning($"order with id {id} was not found while attempting to update order status by id");
//            return result.notfound(["the order is not found"]);
//        }



//        order.orderstatus = status;
//        order.modifiedby = _usercontext.email;
//        await _dbcontext.savechangesasync();
//        var orderresponsedto = _mapper.map<orderresponsedto>(order);

//        _logger.loginformation($"successfully update order status to: {order.orderstatus} ");
//        return result.success(orderresponsedto, "successfully updated order");
//    }


//    public async task<result<list<orderresponsedto>>> searchorderbytextasync(status text)
//    {
//        var orders = await _dbcontext.orders
//            .projectto<orderresponsedto>(_mapper.configurationprovider)
//            .where(n => n.orderstatus.equals(text))
//            .tolistasync();
//        _logger.loginformation("fetching search order by name . total count: {order}.", orders.count);
//        return result.success(orders);
//    }
//}
