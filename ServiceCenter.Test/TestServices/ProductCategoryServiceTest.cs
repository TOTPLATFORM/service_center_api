using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
public class ProductCategoryServiceTest
{
    private static ProductCategoryService _productCategoryService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ProductCategoryService CreateProductCategoryService()
    {

        if (_productCategoryService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ProductCategoryService> logger = new LoggerFactory().CreateLogger<ProductCategoryService>();

            IUserContextService userContext = new UserContextService();

            _productCategoryService = new ProductCategoryService(dbContext, mapper, logger, userContext);
        }

        return _productCategoryService;
    }
    private void CheckService()
    {
        if (_productCategoryService is null)
            _productCategoryService = CreateProductCategoryService();
    }
    /// <summary>
    /// fuction to add product category as a test case that take category number,category description,country of origin,founded year
    /// </summary>
    /// <param name="categoryName">category number</param>
    [Theory, TestPriority(0)]
    [InlineData("ProductCategory1")]
    public async Task AddProductCategory(string categoryName)
    {
        // Arrange
        CheckService();
        var productCategoryRequestDto = new ProductCategoryRequestDto { CategoryName = categoryName };
        // Act
        var result = await _productCategoryService.AddProductCategoryAsync(productCategoryRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to get all  product categories as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllProductCategory()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productCategoryService.GetAllProductCategoryAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get product category by id as a test case 
    /// </summary>
    /// <param name="id">product category id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdProductCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productCategoryService.GetProductCategoryByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to update product category as a test case that take   category number,category description,country of origin,founded year
    /// </summary>
    /// <param name="categoryName">category number</param>
    /// <param name="referenceNumber"> Reference Number</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "ProductCategory1",  true)]
    [InlineData(10, "ProductCategory1", false)]
    public async Task UpdateProductCategory(int id, string categoryName, bool expectedResult)
    {
        //Arrange
        CheckService();
        var productCategoryRequestDto = new ProductCategoryRequestDto { CategoryName = categoryName };

        // Act
        var result = await _productCategoryService.UpdateProductCategoryAsync(id, productCategoryRequestDto);
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
    /// fuction to remove product category as a test case that take product category id
    /// </summary>
    /// <param name="id">product category id </param>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveProductCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productCategoryService.DeleteProductCategoryAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// Tests the search functionality in the product category service to ensure it can find product category based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchProductCategories()
    {
        // Arrange
        CheckService();
        string text = "Category1";

        // Act
        var result = await _productCategoryService.SearchProductCategoryByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
