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
    /// <param name="refereneNumber">reference number</param>
    [Theory, TestPriority(0)]
    [InlineData("ProductCategory1", 2)]
    public async Task AddProductCategory(string categoryName,int refereneNumber)
    {
        // Arrange
        CheckService();
        var productCategoryRequestDto = new ProductCategoryRequestDto { CategoryName = categoryName, ReferenceNumber = refereneNumber };
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
        var result = await _productCategoryService.GetAllProductCategoryAsync();

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
}
