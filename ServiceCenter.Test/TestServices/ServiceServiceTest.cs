using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Domain.Enums;
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
public class ServiceServiceTest
{
    private static ServiceService _serviceService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ServiceService CreateServiceService()
    {

        if (_serviceService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ServiceService> logger = new LoggerFactory().CreateLogger<ServiceService>();

            IUserContextService userContext = new UserContextService();

            _serviceService = new ServiceService(dbContext, mapper, logger, userContext);
        }

        return _serviceService;
    }
    private void CheckService()
    {
        if (_serviceService is null)
            _serviceService = CreateServiceService();
    }
    /// <summary>
    /// fuction to add service  as a test case that take  number ,service  description
    /// </summary>
    /// <param name="Name"> number</param>
    /// <param name="serviceDescription"> service  description</param>
    /// <param name="price">service price</param>
    /// <param name="status">status</param>
    [Theory, TestPriority(0)]
    [InlineData("Service1", "Desc1",120,Status.Pending)]
    public async Task AddService(string Name, string serviceDescription,int price,Status status)
    {
        // Arrange
        CheckService();
        var ServiceRequestDto = new ServiceRequestDto { ServiceName = Name, ServiceDescription = serviceDescription,ServicePrice=price, Avaliable=status};
        // Act
        var result = await _serviceService.AddServiceAsync(ServiceRequestDto);

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
    public async Task GetAllService()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceService.GetAllServiceAsync(2, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get service  by id as a test case 
    /// </summary>
    /// <param name="id">service  id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdService(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceService.GetServiceByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to update service  as a test case that take    number,service  description
    /// </summary>
    /// <param name="categoryName"> number</param>
    /// <param name="serviceCategoryDescription"> service  description </param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "Service1", "Desc1", 120, Status.Pending, true)]
    [InlineData(10, "Service1", "Desc1", 120, Status.Pending, false)]
    public async Task UpdateService(int id, string Name, string serviceDescription, int price, Status status, bool expectedResult)
    {
        //Arrange
        CheckService();
        var ServiceRequestDto = new ServiceRequestDto { ServiceName = Name, ServiceDescription = serviceDescription, ServicePrice = price, Avaliable = status };

        // Act
        var result = await _serviceService.UpdateServiceAsync(id, ServiceRequestDto);
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
    /// fuction to remove service  as a test case that take service  id
    /// </summary>
    /// <param name="id">service  id </param>
    [Theory, TestPriority(7)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveService(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceService.DeleteServiceAsync(id);

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
    public async Task SearchService()
    {
        // Arrange
        CheckService();
        string text = "Service1";

        // Act
        var result = await _serviceService.SearchServiceByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the Service service to ensure it can find Service based on a search term.
    /// </summary>
    [Fact, TestPriority(6)]
    public async Task GetServicesForSpecificServiceCategory()
    {
        // Arrange
        int ServiceCategoryId = 1;
        CheckService();
      

        // Act
        var result = await _serviceService.GetServicesByCategoryAsync(ServiceCategoryId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
   
}

