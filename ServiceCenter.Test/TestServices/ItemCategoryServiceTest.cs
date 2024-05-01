using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.Services;
using ServiceCenter.Test.TestPriority;
using ServiceCenter.Test.TestSetup;
using ServiceCenter.Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestServices;

public class ItemCategoryServiceTest
{
    private static ItemCategoryService _itemCategoryService;
    private string userEmail = "mariamAbdeen@gmail.com";
    private ItemCategoryService CreateItemCategoryService()
    {

        if (_itemCategoryService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<ItemCategoryService> ItemCategoryLogger = new LoggerFactory().CreateLogger<ItemCategoryService>();

            IUserContextService userContext = new UserContextService();
            _itemCategoryService = new ItemCategoryService(dbContext, mapper, ItemCategoryLogger, userContext);
        }

        return _itemCategoryService;
    }

    private void CheckService()
    {
        if (_itemCategoryService is null)
            _itemCategoryService = CreateItemCategoryService();
    }

    /// <summary>
    /// fuction to add item category as a test case . 
    /// </summary>
    /// <param name="ItemCategoryName">item category name</param>
    [Theory, TestPriority(0)]
    [InlineData("Medical Equipment")]
    public async Task AddItemCategory(string ItemCategoryName)
    {
        // Arrange
        CheckService();
        var ItemCategoryRequestDto = new  ItemCategoryRequestDto {  CategoryName = ItemCategoryName };
        // Act
        var result = await _itemCategoryService.AddItemCategoryAsync(ItemCategoryRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// fuction to update itemCategory as a test case .
    /// </summary>
    /// <param name="ItemCategoryName">item category name</param>
    /// <param name="id">ItemCategory id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "Medical Equipment", true)]
    [InlineData(200, "Medical Equipment", false)]
    public async Task UpdateItemCategoryAsync(int id, string ItemCategoryName, bool expectedResult)
    {
        //Arrange
        CheckService();
        var ItemCategoryRequestDto = new ItemCategoryRequestDto { CategoryName = ItemCategoryName };
        // Act
        var result = await _itemCategoryService.UpdateItemCategoryAsync(id, ItemCategoryRequestDto);
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
    /// fuction to get all  item category as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllItemCategory()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _itemCategoryService.GetAllItemCategoryAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get item category by id as a test case that take item category id
    /// </summary>
    /// <param name="id"> item category id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(20)]
    public async Task GetByIdItemCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _itemCategoryService.GetItemCategoryByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to remove item category as a test case.
    /// </summary>
    /// <param name="id">item category id</param>
    [Theory, TestPriority(4)]
    [InlineData(3)]
    [InlineData(30)]
    public async Task RemoveItemCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _itemCategoryService.DeleteItemCategoryAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
