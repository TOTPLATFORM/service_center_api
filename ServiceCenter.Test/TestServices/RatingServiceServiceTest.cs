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
public class RatingServiceServiceTest
{
    private static RatingServiceService _ratingServiceService;
    private string userEmail = "hagershaaban7@gmail.com";
    private RatingServiceService CreateRatingServiceService()
    {

        if (_ratingServiceService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<RatingServiceService> logger = new LoggerFactory().CreateLogger<RatingServiceService>();

            IUserContextService userContext = new UserContextService();

            _ratingServiceService = new RatingServiceService(dbContext, mapper, logger, userContext);
        }

        return _ratingServiceService;
    }
    private void CheckService()
    {
        if (_ratingServiceService is null)
            _ratingServiceService = CreateRatingServiceService();
    }

    /// <summary>
    /// fuction to get all  rating services as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllRatingService()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _ratingServiceService.GetAllRatingServicesAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get rating service by id as a test case 
    /// </summary>
    /// <param name="id"> RatingService id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdRatingService(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _ratingServiceService.GetRatingServiceByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add rating service as a test case that take  
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData(5,"dnsx64984795-5148947-5489")]
    public async Task AddRatingService(int ratingValue , string customerId)
    {
        // Arrange
        CheckService();
        var ratingServiceRequestDto = new RatingServiceRequestDto { RatingValue = ratingValue ,CustomerId= customerId };

        // Act
        var result = await _ratingServiceService.AddRatingServiceAsync(ratingServiceRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove rating service as a test case that take RatingService id
    /// </summary>
    /// <param name="id"> rating service id</param>
    [Theory, TestPriority(4)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveRatingService(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _ratingServiceService.DeleteRatingServiceAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update rating service as a test case that take ,expected result
    /// </summary>
    /// <param name="id">rating service id</param>  
    [Theory, TestPriority(3)]
    [InlineData(2, 5,1, "0d133c1a-804f-4548-8f7e-8c3f504561954", true)]
    [InlineData(30, 5,1, "dnsx64984795-5148947-5489", false)]
    public async Task UpdateRatingService(int id, int ratingValue,int serviceId, string customerId, bool expectedResult)
    {
        // Arrange
        CheckService();
        var RatingServiceRequestDto = new RatingServiceRequestDto {RatingValue= ratingValue, ServiceId= serviceId, CustomerId = customerId };

        // Act
        var result = await _ratingServiceService.UpdateRatingServiceAsync(id, RatingServiceRequestDto);

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
