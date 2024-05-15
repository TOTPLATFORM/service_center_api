using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Test.TestPriority;
using ServiceCenter.Test.TestSetup;
using ServiceCenter.Test.TestSetup.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestServices;
[TestCaseOrderer(
ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
ordererAssemblyName: "ServiceCenter.Test")]
public class FeedbackServiceTest
{
    private static FeedbackService _feedbackService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private FeedbackService CreatefeedbackService()
    {

        if (_feedbackService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<FeedbackService> logger = new LoggerFactory().CreateLogger<FeedbackService>();

            IUserContextService userContext = new UserContextService();

            _feedbackService = new FeedbackService(dbContext, mapper, logger, userContext);
        }

        return _feedbackService;
    }
    private void CheckService()
    {
        if (_feedbackService is null)
            _feedbackService = CreatefeedbackService();
    }

    /// <summary>
    /// fuction to get all  feedbacks as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllfeedback()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _feedbackService.GetAllFeedbackAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get feedback by id as a test case 
    /// </summary>
    /// <param name="id"> feedback id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdfeedback(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _feedbackService.GetFeedbackByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add feedback as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData("Desc1", "Product", "7/11/2024", "53ae72a7-589e-4f0b-81ed-4038169498")]
    public async Task Addfeedback(string feedbackDesc, string feedbackCategory,string feedbackDate,string customerId)
    {
        // Arrange
        CheckService();
        var feedbackRequestDto = new FeedbackRequestDto { FeedbackDescription= feedbackDesc, FeedbackCategory= feedbackCategory, FeedbackDate= DateOnly.Parse(feedbackDate) };

        // Act
        var result = await _feedbackService.AddFeedbackAsync(feedbackRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove feedback as a test case that take feedback id
    /// </summary>
    /// <param name="id"> feedback id</param>
    [Theory, TestPriority(4)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task Removefeedback(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _feedbackService.DeleteFeedbackAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update feedback as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">feedback id</param>  
    [Theory, TestPriority(3)]
    [InlineData(1, true)]
    [InlineData(30, false)]
    public async Task Updatefeedback(int id, bool expectedResult)
    {
        // Arrange
        CheckService();
        var feedbackRequestDto = new FeedbackRequestDto { FeedbackDescription = "feedbackDesc", FeedbackCategory = "feedbackCategory", FeedbackDate = DateOnly.Parse("7/11/2024") };

        // Act
        var result = await _feedbackService.UpdateFeedbackAsync(id, feedbackRequestDto);

        if (expectedResult)
        {
            Assert.True(result.IsSuccess); // Expecting successful update
        }
        else
        {
            Assert.False(result.IsSuccess); // Expecting unsuccessful update
        }
    }
}
