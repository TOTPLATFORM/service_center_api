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
public class OverviewServiceTest
{
    private static OverviewService _overviewService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private OverviewService CreateoverviewService()
    {

        if (_overviewService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<OverviewService> logger = new LoggerFactory().CreateLogger<OverviewService>();

            IUserContextService userContext = new UserContextService();

            _overviewService = new OverviewService(dbContext, mapper, logger, userContext);
        }

        return _overviewService;
    }
    private void CheckService()
    {
        if (_overviewService is null)
            _overviewService = CreateoverviewService();
    }

    /// <summary>
    /// fuction to get all  overviews as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAlloverview()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _overviewService.GetAllOverviewAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get overview by id as a test case 
    /// </summary>
    /// <param name="id"> overview id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdoverview(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _overviewService.GetOverviewByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add overview as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData("Desc1", "Product", "7/11/2024")]
    public async Task AddOverview(string task, string priority, string dueDate)
    {
        // Arrange
        CheckService();
        var overviewRequestDto = new OverviewRequestDto { Task = task, Priority = priority,  DueDate = DateTime.Parse(dueDate) };

        // Act
        var result = await _overviewService.AddOverviewAsync(overviewRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
}
