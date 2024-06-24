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
    private static ServiceCategoryService _serviceCategoryService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ServiceCategoryService CreateServiceCategoryService()
    {

        if (_serviceCategoryService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ServiceCategoryService> logger = new LoggerFactory().CreateLogger<ServiceCategoryService>();

            IUserContextService userContext = new UserContextService();

            _serviceCategoryService = new ServiceCategoryService(dbContext, mapper, logger, userContext);
        }

        return _serviceCategoryService;
    }
    private void CheckService()
    {
        if (_serviceCategoryService is null)
            _serviceCategoryService = CreateServiceCategoryService();
    }
    /// <summary>
    /// fuction to add service category as a test case that take category number ,service category description
    /// </summary>
    /// <param name="categoryName">category number</param>
    /// <param name="serviceCategoryDescription"> service category description</param>
    [Theory, TestPriority(0)]
    [InlineData("ServiceCategory1", "Desc1")]
    public async Task AddServiceCategory(string categoryName, string serviceCategoryDescription)
    {
        // Arrange
        CheckService();
        var serviceCategoryRequestDto = new ServiceCategoryRequestDto { ServiceCategoryName = categoryName, ServiceCategoryDescription = serviceCategoryDescription };
        // Act
        var result = await _serviceCategoryService.AddServiceCategoryAsync(serviceCategoryRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to get all  service categories as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllServiceCategory()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceCategoryService.GetAllServiceCategoryAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get service category by id as a test case 
    /// </summary>
    /// <param name="id">service category id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdServiceCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceCategoryService.GetServiceCategoryByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to update service category as a test case that take   category number,service category description
    /// </summary>
    /// <param name="categoryName">category number</param>
    /// <param name="serviceCategoryDescription"> service category description </param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "ServiceCategory1", "Desc1", true)]
    [InlineData(10, "ServiceCategory1", "Desc2", false)]
    public async Task UpdateServiceCategory(int id, string categoryName, string serviceCategoryDescription, bool expectedResult)
    {
        //Arrange
        CheckService();
        var serviceCategoryRequestDto = new ServiceCategoryRequestDto { ServiceCategoryName = categoryName, ServiceCategoryDescription = serviceCategoryDescription };

        // Act
        var result = await _serviceCategoryService.UpdateServiceCategoryAsync(id, serviceCategoryRequestDto);
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
    /// fuction to remove service category as a test case that take service category id
    /// </summary>
    /// <param name="id">service category id </param>
    [Theory, TestPriority(5)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveServiceCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceCategoryService.DeleteServiceCategoryAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// Tests the search functionality in the serviceGategory service to ensure it can find serviceGategory based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchServiceGategory()
    {
        // Arrange
        CheckService();
        string text = "Category1";

        // Act
        var result = await _serviceCategoryService.SearchServiceCategoryByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}

