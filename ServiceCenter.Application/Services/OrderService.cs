//using servicecenter.core.result;
//using automapper;
//using automapper.queryableextensions;
//using servicecenter.application.contracts;
//using microsoft.entityframeworkcore;
//using microsoft.extensions.logging;
//using servicecenter.infrastructure.basecontext;
//using servicecenter.application.dtos;
//using servicecenter.domain.entities;
//using servicecenter.domain.enums;

//namespace hmswithlayers.application.services;

//public class orderservice(iitemservice itemservice, servicecenterbasedbcontext dbcontext, imapper mapper, ilogger<orderservice> logger, iusercontextservice usercontext) : iorderservice
//{
//    private readonly iitemservice _itemservice = itemservice;
//    private readonly servicecenterbasedbcontext _dbcontext = dbcontext;
//    private readonly imapper _mapper = mapper;
//    private readonly ilogger<orderservice> _logger = logger;
//    private readonly iusercontextservice _usercontext = usercontext;

//    /<inheritdoc/>
//    public async task<result<list<orderresponsedto>>> getallorderasync(status status)
//    {
//        var ordersresponsedto = await _dbcontext.orders
//            .where(order => order.orderstatus == status)
//            .projectto<orderresponsedto>(_mapper.configurationprovider)
//            .tolistasync();

//        _logger.loginformation("fetching all orders. total count: {ordersresponsedto}.", ordersresponsedto.count);
//        return result.success(ordersresponsedto);
//    }

//    /<inheritdoc/>
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
