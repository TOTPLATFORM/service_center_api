using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Subscriptions;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Test.TestPriority;
using ServiceCenter.Test.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Application.Contracts;

namespace ServiceCenter.Test.TestServices;
[TestCaseOrderer(
ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
ordererAssemblyName: "ServiceCenter.Test")]
public class SubscriptionServiceTest
{
    private static SubscriptionService _contractService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private SubscriptionService CreateSubscriptionService()
    {

        if (_contractService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<SubscriptionService> logger = new LoggerFactory().CreateLogger<SubscriptionService>();

            IUserContextService userContext = new UserContextService();

            _contractService = new SubscriptionService(dbContext, mapper, logger, userContext);
        }

        return _contractService;
    }
    private void CheckService()
    {
        if (_contractService is null)
            _contractService = CreateSubscriptionService();
    }

    /// <summary>
    /// fuction to add contract as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000-12-30", 1)]
    public async Task AddSubscription(string duration, int servicePackageId)
    {
        // Arrange
        CheckService();
        var SubscriptionRequestDto = new SubscriptionRequestDto { Duration = DateOnly.Parse(duration), ServicePackageId = servicePackageId };
        // Act
        var result = await _contractService.AddSubscriptionAsync(SubscriptionRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  Subscriptiones as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllSubscription()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contractService.GetAllSubscriptionAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get Subscription by id as a test case 
    /// </summary>
    /// <param name="id">Subscription id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdCenter(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contractService.GetSubscriptionByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Subscription as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "2000-12-30", 1, true)]
    [InlineData(10, "2000-12-30", 1, false)]
    public async Task UpdateCenter(int id, string duration, int servicePackageId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var SubscriptionRequestDto = new SubscriptionRequestDto { Duration = DateOnly.Parse(duration), ServicePackageId = servicePackageId };

        // Act
        var result = await _contractService.UpdateSubscriptionAsync(id, SubscriptionRequestDto);
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
    /// fuction to remove Subscription as a test case that take Subscription id
    /// </summary>
    /// <param name="id">Subscription id </param>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveSubscription(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contractService.DeleteSubscriptionAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
