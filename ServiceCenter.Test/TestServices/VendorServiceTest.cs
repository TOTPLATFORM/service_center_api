using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.JWT;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Test.TestPriority;
using ServiceCenter.Test.TestSetup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestServices;
[TestCaseOrderer(
ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
ordererAssemblyName: "ServiceCenter.Test")]
public class VendorServiceTest
{
    private static VendorService _vendorService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f645630",Email="hagershaaban7@gmail.com" ,UserName="hager3012"},
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f654945",Email="hagershaaban7@gmail.com" ,UserName="hager1230"},

        };

    private VendorService CreateVendorService()
    {

        if (_vendorService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<VendorService> logger = new LoggerFactory().CreateLogger<VendorService>();

            IUserContextService userContext = new UserContextService();
            var userStore = new UserStore<ApplicationUser>(dbContext);
            ILogger<ApplicationUser> userLogger = new LoggerFactory().CreateLogger<ApplicationUser>();

            UserManager<ApplicationUser> userManager = InMemoryUserStore.MockUserManager(_users).Object;

            var jwtOptions = Options.Create(new JWT

            {

                Issuer = "TOTPlatform",

                Audience = "PlatformUsers",

                Key = "QqEz6jAMz8LIsXLcm4GtSOp24cQ50LxPlY/cgZ4NCZQ=",

                DurationInDays = 1

            });

            var authService = new AuthService(userManager, userLogger, mapper, jwtOptions);


            _vendorService = new VendorService(dbContext, mapper, logger, userContext, authService);
        }

        return _vendorService;
    }
    private void CheckService()
    {
        if (_vendorService is null)
            _vendorService = CreateVendorService();
    }

    /// <summary>
    /// fuction to add vendor as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000-12-30",  "agershaban7@gmail.com", "hager", "shaban","0987654", "2000-12-30", "2000-12-30")]
    public async Task Addvendor(string dateOfBirth, string email, string firstName, string lastName, string phoneNumber,string startDate,string endDate)
    {
        // Arrange
        CheckService();
        var vendorRequestDto = new VendorRequestDto
        {
            ContractStartDate = DateOnly.Parse(startDate),
           ContractEndDate = DateOnly.Parse(endDate),
            Email = email,
           
          
        };
        // Act
        var result = await _vendorService.AddVendorAsync(vendorRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  vendores as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllVendor()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _vendorService.GetAllVendorsAsync(3, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get vendor by id as a test case 
    /// </summary>
    /// <param name="id">vendor id </param>
    [Theory, TestPriority(2)]
    [InlineData("ksn56418942")]
    [InlineData("nksalknsdn")]
    public async Task GetByIdCenter(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _vendorService.GetVendorByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update vendor as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("0d133c1a-809f-4548-8f7e-8c3f504844e0", "2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", "2000-12-30", "2000-12-30", true)]
    [InlineData("ja1651666", "2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", "2000-12-30", "2000-12-30", false)]
    public async Task UpdateVendor(string id, string dateOfBirth, string email, string firstName, string lastName, string phoneNumber, string startDate, string endDate, bool expectedResult)
    {
        //Arrange
        CheckService();
        var vendorRequestDto = new VendorRequestDto
        {
           ContractStartDate = DateOnly.Parse(startDate),
            ContractEndDate = DateOnly.Parse(endDate),
            Email = email,
           
        };

        // Act
        var result = await _vendorService.UpdateVendorAsync(id, vendorRequestDto);
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
    /// Tests the search functionality in the vendor service to ensure it can find vendor based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchVendor()
    {
        // Arrange
        CheckService();
        string text = "hager";

        // Act
        var result = await _vendorService.SearchVendorByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
