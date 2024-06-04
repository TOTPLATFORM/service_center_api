//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Application.Services;
//using ServiceCenter.Test.TestPriority;
//using ServiceCenter.Test.TestSetup;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Test.TestServices;
//[TestCaseOrderer(
//ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
//ordererAssemblyName: "ServiceCenter.Test")]
//public class InventoryServiceTest
//{
//    private static InventoryService _inventoryService;

//    private InventoryService CreateInventoryService()
//    {

//        if (_inventoryService is null)
//        {
//            var dbContext = ContextGenerator.Generator();

//            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
//            ILogger<InventoryService> InventoryLogger = new LoggerFactory().CreateLogger<InventoryService>();
//            IUserContextService userContext = new UserContextService();

//            _inventoryService = new InventoryService(dbContext, mapper, InventoryLogger,  userContext);
//        }

//        return _inventoryService;
//    }

//    private void CheckService()
//    {
//        if (_inventoryService is null)
//            _inventoryService = CreateInventoryService();
//    }

//    /// <summary>
//    /// fuction to add inventory as a test case that take  inventory name  , location and capacity. 
//    /// </summary>
//    /// <param name="inventoryName">inventory name</param>
//    /// <param name="inventoryLocation">inventory location</param>
//    [Theory, TestPriority(0)]
//    [InlineData("inv1", "B", 10)]

//    public async Task AddInventory(string inventoryName, string inventoryLocation, int capacity)
//    {
//        // Arrange
//        CheckService();
//        var InventoryRequestDto = new InventoryRequestDto { InventoryName = inventoryName, InventoryCapacity = capacity, InventoryLocation = inventoryLocation };
//        // Act
//        var result = await _inventoryService.AddInventoryAsync(InventoryRequestDto);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//    }

//    /// <summary>
//    /// fuction to update inventory as a test case .
//    /// </summary>
//    /// <param name="inventoryName">inventory name</param>
//    /// <param name="id">inventory id</param>
//    /// <param name="expectedResult">expected result</param>
//    [Theory, TestPriority(3)]
//    [InlineData(1, "F5", true)]
//    [InlineData(20, "F6", false)]
//    public async Task UpdateInventoryAsync(int id, string inventoryName, bool expectedResult)
//    {
//        //Arrange
//        CheckService();
//        var inventoryRequestDto = new InventoryRequestDto
//        {
//            InventoryName = inventoryName
//        };
//        // Act
//        var result = await _inventoryService.UpdateInventoryAsync(id, inventoryRequestDto);
//        // Assert
//        if (expectedResult)
//        {
//            Assert.True(result.IsSuccess); // Expecting successful update
//        }
//        else
//        {
//            Assert.False(result.IsSuccess); // Expecting unsuccessful update
//        }
//    }
//    /// <summary>
//    /// fuction to get all  Inventory as a test case 
//    /// </summary>
//    [Fact, TestPriority(1)]
//    public async Task GetAllInventory()
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _inventoryService.GetAllInventoriesAsync();

//        // Assert
//        Assert.True(result.IsSuccess);

//    }
//    /// <summary>
//    /// fuction to get inventory by id as a test case that take inventory id
//    /// </summary>
//    /// <param name="id"> inventory id</param>
//    [Theory, TestPriority(2)]
//    [InlineData(1)]
//    [InlineData(20)]
//    public async Task GetByIdInventory(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _inventoryService.GetInventoryByIdAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }
//    /// <summary>
//    /// fuction to remove inventory as a test case that take inventory id
//    /// </summary>
//    /// <param name="id">inventory id</param>
//    [Theory, TestPriority(4)]
//    [InlineData(2)]
//    [InlineData(30)]
//    public async Task RemoveInventory_ReturnResult(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _inventoryService.DeleteInventoryAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }
//}
