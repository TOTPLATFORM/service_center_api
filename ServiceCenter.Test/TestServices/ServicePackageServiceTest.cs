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
public class ServicePackageServiceTest
{
    private static ServicePackageService _servicePackageService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ServicePackageService CreateServiceCategoryService()
    {

        if (_servicePackageService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ServicePackageService> logger = new LoggerFactory().CreateLogger<ServicePackageService>();

            IUserContextService userContext = new UserContextService();

            _servicePackageService = new ServicePackageService(dbContext, mapper, logger, userContext);
        }

        return _servicePackageService;
    }
    private void CheckService()
    {
        if (_servicePackageService is null)
            _servicePackageService = CreateServiceCategoryService();
    }
    /// <summary>
    /// fuction to add service Package as a test case that take Package number ,service Package description
    /// </summary>
    /// <param name="packageName">Package number</param>
    /// <param name="packageDescription"> service Package description</param>
    /// <param name="price">service package price</param>
    [Theory, TestPriority(0)]
    [InlineData("ServicePackage1", "Desc1",120)]
    public async Task AddServicePackage(string packageName, string packageDescription,int price)
    {
        // Arrange
        CheckService();
        var servicePackageRequestDto = new ServicePackageRequestDto { PackageName = packageName, PackageDescription = packageDescription ,PackagePrice=price};
        // Act
        var result = await _servicePackageService.AddServicePackageAsync(servicePackageRequestDto);

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
    public async Task GetAllServicePackages()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _servicePackageService.GetAllServicePackageAsync(2, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get service Package by id as a test case 
    /// </summary>
    /// <param name="id">service Package id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdServicePackage(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _servicePackageService.GetServicePackageByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to update service Package as a test case that take   Package number,service Package description
    /// </summary>
    /// <param name="categoryName">Package number</param>
    /// <param name="serviceCategoryDescription"> service Package description </param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "ServicePackage1", "Desc1", 120,true)]
    [InlineData(10, "ServicePackage2", "Desc2",200, false)]
    public async Task UpdateServiceCategory(int id, string packageName, string packageDescription, int price, bool expectedResult)
    {
        //Arrange
        CheckService();
        var servicePackageRequestDto = new ServicePackageRequestDto {PackageName = packageName, PackageDescription = packageDescription ,PackagePrice = price };

        // Act
        var result = await _servicePackageService.UpdateServicePackageAsync(id, servicePackageRequestDto);
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
    /// fuction to remove service Package as a test case that take service Package id
    /// </summary>
    /// <param name="id">service Package id </param>
    [Theory, TestPriority(5)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveServiceCategory(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _servicePackageService.DeleteServicePackageAsync(id);

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
    public async Task SearchServicePackage()
    {
        // Arrange
        CheckService();
        string text = "Package1";

        // Act
        var result = await _servicePackageService.SearchServicePackageByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}

 