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
public class ProductServiceTest
{
    private static ProductService _productService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ProductService CreateProductService()
    {

        if (_productService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ProductService> logger = new LoggerFactory().CreateLogger<ProductService>();

            IUserContextService userContext = new UserContextService();

            _productService = new ProductService(dbContext, mapper, logger, userContext);
        }

        return _productService;
    }
    private void CheckService()
    {
        if (_productService is null)
            _productService = CreateProductService();
    }

    /// <summary>
    /// fuction to add product  as a test case that take product name,product description,product price,product brand id,product category id,sales id
    /// </summary>
    /// <param name="productName">product name</param>
    /// <param name="productDescription">product description</param>
    /// <param name="productPrice">product price</param>
    /// <param name="productBrandId">product brand id</param>
    /// <param name="productCategoryId">product category id</param>
    /// <param name="salesId">sales id</param>
    [Theory, TestPriority(0)]
    [InlineData("Product1", "Product1 is good", 20, 1,  "js6549874965-54463651654")]
    public async Task AddProduct(string productName, string productDescription, int productPrice, int productBrandId,  string salesId)
    {
        // Arrange
        CheckService();
        var ProductRequestDto = new ProductRequestDto { ProductName = productName, ProductDescription = productDescription, ProductPrice = productPrice };
        // Act
        var result = await _productService.AddProductAsync(ProductRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  product as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllProduct()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productService.GetAllProductAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get product  by id as a test case 
    /// </summary>
    /// <param name="id">product  id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdProduct(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productService.GetProductByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update product as a test case that take product name,product description,product price,product brand id,product category id,sales id
    /// </summary>
    /// <param name="productName">product name</param>
    /// <param name="productDescription">product description</param>
    /// <param name="productPrice">product price</param>
    /// <param name="productBrandId">product brand id</param>
    /// <param name="productCategoryId">product category id</param>
    /// <param name="salesId">sales id</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "Product1", "Product1 is good", 20,  true)]
    [InlineData(10, "Product1", "Product1 is good", 20,  false)]
    public async Task UpdateProduct(int id, string productName, string productDescription, int productPrice,  bool expectedResult)
    {
        //Arrange
        CheckService();
        var productRequestDto = new ProductRequestDto { ProductName = productName, ProductDescription = productDescription, ProductPrice = productPrice};

        // Act
        var result = await _productService.UpdateProductAsync(id, productRequestDto);
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
    /// fuction to remove product brand as a test case that take product brand id
    /// </summary>
    /// <param name="id">product brand id </param>
    [Theory, TestPriority(6)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveProduct(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productService.DeleteProductAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// Tests the search functionality in the product service to ensure it can find product based on a search term.
    /// </summary>
    [Fact, TestPriority(5)]
    public async Task GetProductsForSpecificProductCategory()
    {
        // Arrange
        CheckService();
        int productId = 1;

        // Act
        var result = await _productService.GetProductsForProductCategoryAsync(productId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the product service to ensure it can find product based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchProducts()
    {
        // Arrange
        CheckService();
        string text = "product1";

        // Act
        var result = await _productService.SearchProductByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
