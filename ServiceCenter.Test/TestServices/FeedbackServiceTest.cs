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
public class FeedbackServiceTest
{
    private static FeedbackService _FeedbackService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private FeedbackService CreateFeedbackService()
    {

        if (_FeedbackService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<FeedbackService> logger = new LoggerFactory().CreateLogger<FeedbackService>();

            IUserContextService userContext = new UserContextService();

            _FeedbackService = new FeedbackService(dbContext, mapper, logger, userContext);
        }

        return _FeedbackService;
    }
    private void CheckService()
    {
        if (_FeedbackService is null)
            _FeedbackService = CreateFeedbackService();
    }

    /// <summary>
    /// fuction to get all  Feedbacks as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllFeedback()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _FeedbackService.GetAllFeedbackAsync(2, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get Feedback by id as a test case 
    /// </summary>
    /// <param name="id"> Feedback id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdFeedback(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _FeedbackService.GetFeedbackByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add Feedback as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData("Desc1", "0d133c1a-804f-4548-8f7e-8c3f504844u0", 1)]
    public async Task AddFeedback(string FeedbackDesc, string customerId, int serviceId)
    {
        // Arrange
        CheckService();
        var FeedbackRequestDto = new FeedbackRequestDto { FeedbackDescription = FeedbackDesc, ContactId = customerId, ServiceId = serviceId };

        // Act
        var result = await _FeedbackService.AddFeedbackAsync(FeedbackRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove Feedback as a test case that take Feedback id
    /// </summary>
    /// <param name="id"> Feedback id</param>
    [Theory, TestPriority(8)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveFeedback(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _FeedbackService.DeleteFeedbackAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Feedback as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">Feedback id</param>  
    [Theory, TestPriority(3)]
    [InlineData(1, "Desc", true)]
    [InlineData(8, "Desc1", false)]
    public async Task UpdateFeedback(int id, string feedbackDesc, bool expectedResult)
    {
        // Arrange
        CheckService();


        // Act
        var result = await _FeedbackService.UpdateFeedbackDescAsync(id, feedbackDesc);

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
    /// Tests the search functionality in the feedback service to ensure it can find feedback based on a search term.
    /// </summary>
    [Fact, TestPriority(5)]
    public async Task GetFeedbacksForSpecificCustomer()
    {
        // Arrange
        CheckService();
        string customerId = "0d133c1a-804f-4548-8f7e-8c3f504844u0";

        // Act
        var result = await _FeedbackService.GetFeedbacksForSpecificCustomerAsync(customerId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the feedback service to ensure it can find feedback based on a search term.
    /// </summary>
    [Fact, TestPriority(6)]
    public async Task GetFeedbacksForSpecificService()
    {
        // Arrange
        CheckService();
        int serviceId = 1;

        // Act
        var result = await _FeedbackService.GetFeedbacksForSpecificServiceAsync(serviceId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the feedback service to ensure it can find feedback based on a search term.
    /// </summary>
    [Fact, TestPriority(7)]
    public async Task GetFeedbacksForSpecificProduct()
    {
        // Arrange
        CheckService();
        int productId = 1;

        // Act
        var result = await _FeedbackService.GetFeedbacksForSpecificProductAsync(productId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }


}

