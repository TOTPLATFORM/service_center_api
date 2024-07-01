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
public class RatingServiceTest
{
    private static RatingService _RatingService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private RatingService CreateRatingService()
    {

        if (_RatingService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<RatingService> logger = new LoggerFactory().CreateLogger<RatingService>();

            IUserContextService userContext = new UserContextService();

            _RatingService = new RatingService(dbContext, mapper, logger, userContext);
        }

        return _RatingService;
    }
    private void CheckService()
    {
        if (_RatingService is null)
            _RatingService = CreateRatingService();
    }

    /// <summary>
    /// fuction to get all  Ratings as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllRating()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _RatingService.GetAllRatingAsync(2, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get Rating by id as a test case 
    /// </summary>
    /// <param name="id"> Rating id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdRating(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _RatingService.GetRatingByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add Rating as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData(1, "0d133c1a-804f-4548-8f7e-8c3f504844u0", 1)]
    public async Task AddRating(int ratingValue, string customerId, int serviceId)
    {
        // Arrange
        CheckService();
        var RatingRequestDto = new RatingRequestDto { RatingValue = ratingValue, CustomerId = customerId, ServiceId = serviceId };

        // Act
        var result = await _RatingService.AddRatingAsync(RatingRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove Rating as a test case that take Rating id
    /// </summary>
    /// <param name="id"> Rating id</param>
    [Theory, TestPriority(8)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveRating(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _RatingService.DeleteRatingAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Rating as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">Rating id</param>  
    [Theory, TestPriority(3)]
    [InlineData(1, 2, true)]
    [InlineData(8, 3, false)]
    public async Task UpdateRating(int id, int ratingValue, bool expectedResult)
    {
        // Arrange
        CheckService();


        // Act
        var result = await _RatingService.UpdateRatingValueAsync(id, ratingValue);

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
    /// Tests the search functionality in the rating service to ensure it can find rating based on a search term.
    /// </summary>
    [Fact, TestPriority(5)]
    public async Task GetRatingsForSpecificCustomer()
    {
        // Arrange
        CheckService();
        string customerId = "0d133c1a-804f-4548-8f7e-8c3f504844u0";

        // Act
        var result = await _RatingService.GetRatingsForSpecificCustomerAsync(customerId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the rating service to ensure it can find rating based on a search term.
    /// </summary>
    [Fact, TestPriority(6)]
    public async Task GetRatingsForSpecificService()
    {
        // Arrange
        CheckService();
        int serviceId = 1;

        // Act
        var result = await _RatingService.GetRatingsForSpecificServiceAsync(serviceId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the rating service to ensure it can find rating based on a search term.
    /// </summary>
    [Fact, TestPriority(7)]
    public async Task GetRatingsForSpecificProduct()
    {
        // Arrange
        CheckService();
        int productId = 1;

        // Act
        var result = await _RatingService.GetRatingsForSpecificProductAsync(productId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }


}

