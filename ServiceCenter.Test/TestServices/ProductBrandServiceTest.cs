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
//public class ProductBrandServiceTest
//{
//    private static ProductBrandService _productBrandService;
//    private string userEmail = "mariamabdeeen@gmail.com";
//    private ProductBrandService CreateProductBrandService()
//    {

//        if (_productBrandService is null)
//        {
//            var dbContext = ContextGenerator.Generator();

//            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

//            ILogger<ProductBrandService> logger = new LoggerFactory().CreateLogger<ProductBrandService>();

//            IUserContextService userContext = new UserContextService();

//            _productBrandService = new ProductBrandService(dbContext, mapper, logger, userContext);
//        }

//        return _productBrandService;
//    }
//    private void CheckService()
//    {
//        if (_productBrandService is null)
//            _productBrandService = CreateProductBrandService();
//    }

//    /// <summary>
//    /// fuction to add product brand as a test case that take brand number,brand description,country of origin,founded year
//    /// </summary>
//    /// <param name="brandName">brand number</param>
//    /// <param name="brandDescription">brand description</param>
//    /// <param name="countryOfOrigin">country of origin</param>
//    /// <param name="foundedYear">founded year</param>
//    [Theory, TestPriority(0)]
//    [InlineData("ProductBrand1", "ProductBrand1 is good", "egypt", "2000/12/30")]
//    public async Task AddCenter(string brandName, string brandDescription, string countryOfOrigin,string foundedYear)
//    {
//        // Arrange
//        CheckService();
//        var productBrandRequestDto = new ProductBrandRequestDto {BrandName = brandName,BrandDescription = brandDescription,CountryOfOrigin = countryOfOrigin ,FoundedYear = DateOnly.Parse(foundedYear) };
//        // Act
//        var result = await _productBrandService.AddProductBrandAsync(productBrandRequestDto);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);
//    }

//    /// <summary>
//    /// fuction to get all  product brandes as a test case 
//    /// </summary>
//    [Fact, TestPriority(1)]
//    public async Task GetAllProductBrand()
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _productBrandService.GetAllProductBrandAsync();

//        // Assert
//        Assert.True(result.IsSuccess);

//    }

//    /// <summary>
//    /// fuction to get product brand by id as a test case 
//    /// </summary>
//    /// <param name="id">product brand id </param>
//    [Theory, TestPriority(2)]
//    [InlineData(1)]
//    [InlineData(6)]
//    public async Task GetByIdCenter(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _productBrandService.GetProductBrandByIdAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }

//    /// <summary>
//    /// fuction to update product brand as a test case that take   brand number,brand description,country of origin,founded year
//    /// </summary>
//    /// <param name="brandName">brand number</param>
//    /// <param name="brandDescription">brand description</param>
//    /// <param name="countryOfOrigin">country of origin</param>
//    /// <param name="foundedYear">founded year</param>
//    /// <param name="expectedResult">expected result</param>
//    [Theory, TestPriority(3)]
//    [InlineData(1, "ProductBrand1", "ProductBrand1 is good", "egypt", "2000/12/30", true)]
//    [InlineData(10, "ProductBrand1", "ProductBrand1 is good", "egypt", "2000/12/30", false)]
//    public async Task UpdateCenter(int id, string brandName, string brandDescription, string countryOfOrigin, string foundedYear, bool expectedResult)
//    {
//        //Arrange
//        CheckService();
//        var productBrandRequestDto = new ProductBrandRequestDto { BrandName = brandName, BrandDescription = brandDescription, CountryOfOrigin = countryOfOrigin, FoundedYear = DateOnly.Parse(foundedYear) };

//        // Act
//        var result = await _productBrandService.UpdateProductBrandAsync(id, productBrandRequestDto);
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
//    /// fuction to remove product brand as a test case that take product brand id
//    /// </summary>
//    /// <param name="id">product brand id </param>
//    [Theory, TestPriority(4)]
//    [InlineData(2)]
//    [InlineData(50)]
//    public async Task RemoveProductBrand(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _productBrandService.DeleteProductBrandAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }
//}
