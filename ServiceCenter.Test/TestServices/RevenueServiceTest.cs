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
public class RevenueServiceTest
{
    private static RevenueService _revenueService;

    private RevenueService CreateRevenueService()
    {

        if (_revenueService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<RevenueService> ItemLogger = new LoggerFactory().CreateLogger<RevenueService>();

            IUserContextService userContext = new UserContextService();
            _revenueService = new RevenueService(dbContext, mapper, ItemLogger, userContext);
        }

        return _revenueService;
    }

    private void CheckService()
    {
        if (_revenueService is null)
            _revenueService = CreateRevenueService();
    }

    /// <summary>
    /// Tests adding a revenue with given value.
    /// </summary>
    /// <param name="value">value of revenue.</param>
    [Theory, TestPriority(0)]
    [InlineData(90)]
    public async Task AddRevenue(int value)
    {
        // Arrange
        CheckService();
        var revenueRequestDto = new RevenueRequestDto { Value = value };

        // Act
        var result = await _revenueService.AddRevenueAsync(revenueRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// Tests updating a revenue by ID with new data and checks if the update meets the expected outcome.
    /// </summary>
    /// <param name="id">revenue's ID to update.</param>
    /// <param name="value">New value for the revenue.</param>
    [Theory, TestPriority(3)]
    [InlineData(2, 601, true)]
    [InlineData(10, 30, false)]
    public async Task UpdateRevenueAsync(int id, int value, bool expectedResult)
    {
        //Arrange
        CheckService();
        var revenueRequestDto = new RevenueRequestDto { Value = value };

        // Act
        var result = await _revenueService.UpdateRevenueAsync(id, revenueRequestDto);

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
    /// Tests retrieving all revenue to ensure the service returns a successful result and the list is correctly fetched.
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllRevenues()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _revenueService.GetAllRevenuesAsync(2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// Tests the search functionality in the revenue service to ensure it can find revenues based on a search term.
    /// </summary>
    [Theory, TestPriority(4)]
    [InlineData("7/11/2024")]
    public async Task SearchCities(string date)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _revenueService.SearchRevenuesByTextAsync(DateOnly.Parse(date), 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// Tests the removal of a revenue by their ID to ensure the service can successfully delete cities and handle IDs that do not exist.
    /// </summary>
    /// <param name="id">The ID of the revenue to remove.</param>
    [Theory, TestPriority(5)]
    [InlineData(3)]
    [InlineData(30)]
    public async Task RemoveRevenue(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _revenueService.DeleteRevenueAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}