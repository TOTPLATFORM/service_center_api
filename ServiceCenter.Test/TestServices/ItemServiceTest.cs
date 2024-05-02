using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
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

    public  class ItemServiceTest
    {
        private static ItemService _itemService;
        private string userEmail = "mariamAbdeen@gmail.com";
        private ItemService CreateItemService()
        {

            if (_itemService is null)
            {
                var dbContext = ContextGenerator.Generator();

                var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
                ILogger<ItemService> ItemLogger = new LoggerFactory().CreateLogger<ItemService>();

                IUserContextService userContext = new UserContextService();
                _itemService = new ItemService(dbContext, mapper, ItemLogger, userContext);
            }

            return _itemService;
        }

        private void CheckService()
        {
            if (_itemService is null)
                _itemService = CreateItemService();
        }

        /// <summary>
        /// fuction to add item as a test case . 
        /// </summary>
        /// <param name="ItemName">item name</param>
        [Theory, TestPriority(0)]
        [InlineData("First Aid Kit", "Basic first aid supplies for emergencies", 100, 1)]
        [InlineData("First Aid Kit", "Basic first aid supplies for emergencies", 100, 50)]
        public async Task AddItem(string ItemName, string ItemDescription, int ItemPrice, int ItemCategoryId)
        {
            // Arrange
            CheckService();
            var itemRequestDto = new ItemRequestDto { ItemName = ItemName, ItemDescription = ItemDescription, ItemPrice = ItemPrice, CategoryId = ItemCategoryId };
            // Act
            var result = await _itemService.AddItemAsync(itemRequestDto);

            // Assert
            if (result.IsSuccess)
                Assert.True(result.IsSuccess);
            else
                Assert.False(result.IsSuccess);
        }

        /// <summary>
        /// fuction to update Item as a test case .
        /// </summary>
        /// <param name="ItemName">item name</param>
        /// <param name="ItemDescription">item description</param>
        /// <param name="ItemPrice">item price</param>
        /// <param name="ItemCategoryId">item category id</param>
        /// <param name="id">Item id</param>
        /// <param name="expectedResult">expected result</param>
        [Theory, TestPriority(3)]
        [InlineData(1, "First Aid Kit", "Basic first aid supplies for emergencies", 100, 1, true)]
        [InlineData(100, "First Aid Kit", "Basic first aid supplies for emergencies", 100, 50, false)]
        public async Task UpdateItemAsync(int id, string ItemName, string ItemDescription, int ItemPrice, int ItemCategoryId, bool expectedResult)
        {
            //Arrange
            CheckService();
            var itemRequestDto = new ItemRequestDto { ItemName = ItemName, ItemDescription = ItemDescription, ItemPrice = ItemPrice, CategoryId = ItemCategoryId };
            // Act
            var result = await _itemService.UpdateItemAsync(id, itemRequestDto);
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
        /// <summary>
        /// fuction to get all  Item as a test case 
        /// </summary>
        [Fact, TestPriority(1)]
        public async Task GetAllItem()
        {
            // Arrange
            CheckService();

            // Act
            var result = await _itemService.GetAllItemAsync();

            // Assert
            Assert.True(result.IsSuccess);

        }
        /// <summary>
        /// fuction to get Item by id as a test case that take Item id
        /// </summary>
        /// <param name="id"> Item id</param>
        [Theory, TestPriority(2)]
        [InlineData(1)]
        [InlineData(20)]
        public async Task GetByIdItem(int id)
        {
            // Arrange
            CheckService();

            // Act
            var result = await _itemService.GetItemByIdAsync(id);

            // Assert
            if (result.IsSuccess)
                Assert.True(result.IsSuccess);
            else
                Assert.False(result.IsSuccess);

        }
        /// <summary>
        /// fuction to remove Item as a test case.
        /// </summary>
        /// <param name="id">Item id</param>
        [Theory, TestPriority(4)]
        [InlineData(3)]
        [InlineData(30)]
        public async Task RemoveItem(int id)
        {
            // Arrange
            CheckService();

            // Act
            var result = await _itemService.DeleteItemAsync(id);

            // Assert
            if (result.IsSuccess)
                Assert.True(result.IsSuccess);
            else
                Assert.False(result.IsSuccess);

        }
    }

