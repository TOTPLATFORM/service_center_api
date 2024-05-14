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

public class OfferServiceTest
{
    private static OfferService _offerService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private OfferService CreateOfferService()
    {

        if (_offerService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<OfferService> logger = new LoggerFactory().CreateLogger<OfferService>();

            IUserContextService userContext = new UserContextService();

            _offerService = new OfferService(dbContext, mapper, logger, userContext);
        }

        return _offerService;
    }
    private void CheckService()
    {
        if (_offerService is null)
            _offerService = CreateOfferService();
    }

    /// <summary>
    /// fuction to add Offer as a test case that take  
    /// </summary>
    /// <param name="discount">discount number</param>
    /// <param name="endDate">End Date</param>
    /// <param name="startDate">start Date</param>
    /// <param name="OfferName">offer name</param>
    /// <param name="OfferDescription">offer description</param>
    /// <param name="ProductId">product id</param>
    [Theory, TestPriority(0)]
    [InlineData(20, "2000/12/30", "2000/12/30","offer1","offer is done",1)]
    public async Task AddOffer(int discount, string endDate, string startDate, string OfferName, string OfferDescription, int ProductId)
    {
        // Arrange
        CheckService();
        var OfferRequestDto = new OfferRequestDto
        {
            Discount = discount,
            EndDate = DateOnly.Parse(endDate),
            StartDate= DateOnly.Parse(startDate),
            OfferName = OfferName,
            OfferDescription= OfferDescription,
            ProductId= ProductId
        };
        // Act
        var result = await _offerService.AddOfferAsync(OfferRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  Offeres as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllOffer()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _offerService.GetAllOfferAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get Offer by id as a test case 
    /// </summary>
    /// <param name="id">Offer id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(10)]
    public async Task GetByIdCenter(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _offerService.GetOfferByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Offer as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="discount">discount number</param>
    /// <param name="endDate">End Date</param>
    /// <param name="startDate">start Date</param>
    /// <param name="OfferName">offer name</param>
    /// <param name="OfferDescription">offer description</param>
    /// <param name="ProductId">product id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, 20, "2000/12/30", "2000/12/30", "offer1", "offer is done", 1, true)]
    [InlineData(20, 20, "2000/12/30", "2000/12/30", "offer1", "offer is done", 1, false)]
    public async Task UpdateCenter(int id, int discount, string endDate, string startDate, string OfferName, string OfferDescription, int ProductId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var OfferRequestDto = new OfferRequestDto
        {
            Discount = discount,
            EndDate = DateOnly.Parse(endDate),
            StartDate = DateOnly.Parse(startDate),
            OfferName = OfferName,
            OfferDescription = OfferDescription,
            ProductId = ProductId
        };

        // Act
        var result = await _offerService.UpdateOfferAsync(id, OfferRequestDto);
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
    /// fuction to remove Offer as a test case that take Offer id
    /// </summary>
    /// <param name="id">Offer id </param>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(30)]
    public async Task RemoveOffer(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _offerService.DeleteOfferAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
