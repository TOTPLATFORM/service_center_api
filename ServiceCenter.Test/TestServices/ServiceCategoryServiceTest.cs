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
public class ServiceCategoryServiceTest
{
    private static ServiceCategoryService _productCategoryService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ServiceCategoryService CreateServiceCategoryService()
    {

        if (_productCategoryService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ServiceCategoryService> logger = new LoggerFactory().CreateLogger<ServiceCategoryService>();

            IUserContextService userContext = new UserContextService();

            _productCategoryService = new ServiceCategoryService(dbContext, mapper, logger, userContext);
        }

        return _productCategoryService;
    }
    private void CheckService()
    {
        if (_productCategoryService is null)
            _productCategoryService = CreateServiceCategoryService();
    }
    /// <summary>
    /// fuction to add product category as a test case that take category number ,service category description
    /// </summary>
    /// <param name="categoryName">category number</param>
    /// <param name="serviceCategoryDescription"> service category description</param>
    [Theory, TestPriority(0)]
    [InlineData("ServiceCategory1", "Desc1")]
    public async Task AddServiceCategory(string categoryName, string serviceCategoryDescription)
    {
        // Arrange
        CheckService();
        var productCategoryRequestDto = new ServiceCategoryRequestDto { ServiceCategoryName = categoryName, ServiceCategoryDescription = serviceCategoryDescription };
        // Act
        var result = await _productCategoryService.AddServiceCategoryAsync(productCategoryRequestDto);

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
    public async Task GetAllServiceCategory()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _productCategoryService.GetAllServiceCategoryAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
}

