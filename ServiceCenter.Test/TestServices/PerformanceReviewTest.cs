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
public class PerformanceReviewTest
{
    private static PerformanceReviewService _performanceReviewService;
    private string userEmail = "passant7@gmail.com";
    private PerformanceReviewService CreatePerformanceReviewService()
    {

        if (_performanceReviewService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<PerformanceReviewService> logger = new LoggerFactory().CreateLogger<PerformanceReviewService>();

            IUserContextService userContext = new UserContextService();

            _performanceReviewService = new PerformanceReviewService(dbContext, mapper, logger, userContext);
        }

        return _performanceReviewService;
    }
    private void CheckService()
    {
        if (_performanceReviewService is null)
            _performanceReviewService = CreatePerformanceReviewService();
    }

    /// <summary>
    /// fuction to add performanceReview as a test case that take   performanceReview id , performanceReview number , performanceReview avaliability , performanceReviewtype id  
    /// </summary>
    /// <param name="performanceReviewNumber">performanceReview number</param>
    /// <param name="performanceReviewAvaliabitlity">performanceReview availability</param>
    /// <param name="performanceReviewTypeId">performanceReview Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("any", "any", 3, "3/3/2014", "45yi72a7-589e-4f0b-81ed-40389f683027")]
    public async Task AddperformanceReview(string per, string per1, decimal rate, string date, string empId)
    {
        // Arrange
        CheckService();
        var performanceReviewRequestDto = new PerformanceReviewRequestDto { Comments = per, PerformanceDetails = per1, ReviewDate = DateOnly.Parse(date), PerformanceRating = rate, EmployeeId = empId };
        // Act
        var result = await _performanceReviewService.AddPerformanceReviewAsync(performanceReviewRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  performanceReviews as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllPerformanceReview()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _performanceReviewService.GetAllPerformanceReviewsAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get performanceReview by id as a test case 
    /// </summary>
    /// <param name="id">performanceReview id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdPerformanceReview(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _performanceReviewService.GetPerformanceReviewByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update performanceReview as a test case that take   performanceReview id , performanceReview number , performanceReview avaliability , performanceReviewtype id  
    /// </summary>
    /// <param name="performanceReviewNumber">performanceReview number</param>
    /// <param name="performanceReviewAvaliabitlity">performanceReview availability</param>
    /// <param name="performanceReviewTypeId">performanceReview Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "any", "any", 3, "3/3/2014", "45yi72a7-589e-4f0b-81ed-40389f683027", true)]
    [InlineData(10, "any", "any", 3, "3/3/2014", "45yi72a7-589e-4f0b-81ed-40389f683027", false)]
    public async Task UpdatePerformanceReview(int id, string per, string per1, decimal rate, string date, string empId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var performanceReviewRequestDto = new PerformanceReviewRequestDto { Comments = per, PerformanceDetails = per1, ReviewDate = DateOnly.Parse(date), PerformanceRating = rate, EmployeeId = empId };

        // Act
        var result = await _performanceReviewService.UpdatePerformanceReviewAsync(id, performanceReviewRequestDto);
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
    /// fuction to remove performanceReview as a test case that take performanceReview id
    /// </summary>
    /// <param name="id">performanceReview id </param>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveperformanceReview(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _performanceReviewService.DeletePerformanceReviewAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
}
