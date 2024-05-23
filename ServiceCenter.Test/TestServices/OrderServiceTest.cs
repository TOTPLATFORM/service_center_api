using AutoMapper;
using HMSWithLayers.Application.Services;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Test.TestPriority;
using ServiceCenter.Test.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestServices;
[TestCaseOrderer(
ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
ordererAssemblyName: "ServiceCenter.Test")]
public class OrderServiceTest
{
    private static OrderService _orderService;
    private string userEmail = "mariamabdeen@gmail.com";
    private List<ItemOrderRequestDto> orders = new List<ItemOrderRequestDto>
    {
             new ItemOrderRequestDto
             {
                 ItemId = 1
             },
             new ItemOrderRequestDto
             {
                 ItemId = 2
             },
    };
    private OrderService CreateOrderService()
    {

        if (_orderService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<OrderService> logger = new LoggerFactory().CreateLogger<OrderService>();
            var mapperItem = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<ItemService> ItemLogger = new LoggerFactory().CreateLogger<ItemService>();

            IUserContextService userContext = new UserContextService();
            var itemService = new ItemService(dbContext, mapperItem, ItemLogger, userContext);
            _orderService = new OrderService(itemService, dbContext, mapper, logger, userContext);
        }

        return _orderService;
    }
    private void CheckService()
    {
        if (_orderService is null)
            _orderService = CreateOrderService();
    }

    /// <summary>
    /// fuction to add Order as a test case .   
    /// </summary>
    /// <param name="from">Order from</param>
    /// <param name="status">Order status</param>
    [Theory, TestPriority(0)]
    [InlineData("Supplier C",  Status.Pending)]
    public async Task AddOrder(string from, Status status)
    {
        // Arrange
        CheckService();
        var OrderRequestDto = new OrderRequestDto { From = from, OrderStatus = status, ItemOrders = orders };
        // Act
        var result = await _orderService.AddOrderAsync(OrderRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  Orders as a test case 
    /// </summary>
    /// <returns>boolean for check result is success or failed</returns>
    [Theory, TestPriority(1)]
    [InlineData(Status.Pending)]
    public async Task GetAllOrder(Status status)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _orderService.GetAllOrderAsync(status);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get Order by id as a test case 
    /// </summary>
    /// <param name="Order">list of Order </param>
    /// <returns>list of Order</returns>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdOrder(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _orderService.GetOrderByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Order as a test case that take  Order id , Order name , Order descreiption , Order dosage and Order id and expected result
    /// </summary>
    /// <param name="from">Order from</param>
    /// <param name="status">Order status</param>
    /// <param name="date">Order date</param>
    [Theory, TestPriority(3)]
    [InlineData(1, Status.Pending, true)]
    [InlineData(10, Status.Pending, false)]
    public async Task UpdateSpectialization(int id, Status status, bool expectedResult)
    {
        //Arrange
        CheckService();
        // Act
        var result = await _orderService.UpdateOrderStatusAsync(id, status);
        // Assert
        if (expectedResult)
        {
            Assert.True(result.IsSuccess); // Expecting successful update
        }
        else
        {
            Assert.False(result.IsSuccess); // Expecting unsuccessful update
        }
    }
}
