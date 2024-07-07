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
public class ServiceProviderServiceTest
{
    private static ServiceProviderService _serviceproviderService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f645630",Email="hagershaaban7@gmail.com" ,UserName="hager3012"},
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f654945",Email="hagershaaban7@gmail.com" ,UserName="hager1230"},

        };

    private ServiceProviderService CreateServiceProviderService()
    {

        if (_serviceproviderService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ServiceProviderService> logger = new LoggerFactory().CreateLogger<ServiceProviderService>();

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


            _serviceproviderService = new ServiceProviderService(dbContext, mapper, logger, userContext, authService);
        }

        return _serviceproviderService;
    }
    private void CheckService()
    {
        if (_serviceproviderService is null)
            _serviceproviderService = CreateServiceProviderService();
    }

    /// <summary>
    /// fuction to add serviceprovider as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000-12-30", 1, "agershaban7@gmail.com", "hager", "shaban", "0621654984")]
    public async Task Addserviceprovider(string dateOfBirth, int departmentId, string email, string firstName, string lastName, string phoneNumber)
    {
        // Arrange
        CheckService();
        var serviceproviderRequestDto = new ServiceProviderRequestDto
        {
           
            DepartmentId = departmentId,
            //Email = email,
             ServiceIds = [1]
        };
        // Act
        var result = await _serviceproviderService.AddServiceProviderAsync(serviceproviderRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  serviceprovideres as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllServiceProvider()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceproviderService.GetAllServiceProviderAsync(3, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get serviceprovider by id as a test case 
    /// </summary>
    /// <param name="id">serviceprovider id </param>
    [Theory, TestPriority(2)]
    [InlineData("ksn56418942")]
    [InlineData("nksalknsdn")]
    public async Task GetByIdCenter(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _serviceproviderService.GetServiceProviderByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update serviceprovider as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("0d133c1a-804f-4548-8f7e-8c3f004844e0", "2000-12-30", 1, "agershaban7@gmail.com", "hager", "shaban", "0621654984", "[1, 2, 3]", true)]
    [InlineData("ja1651666", "2000-12-30", 1, "agershaban7@gmail.com", "hager", "shaban", "0621654984", "[1, 2, 3]",  false)]
    public async Task UpdateServiceProvider(string id, string dateOfBirth, int departmentId, string email, string firstName, string lastName, string phoneNumber, string serviceIdsJson, bool expectedResult)
    {
        List<int> serviceIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(serviceIdsJson);
        //Arrange
        CheckService();
        var serviceproviderRequestDto = new ServiceProviderRequestDto
        {
           
            DepartmentId = departmentId, 
           // Email = email,
            ServiceIds = serviceIds
        };

        // Act
        var result = await _serviceproviderService.UpdateServiceProviderAsync(id, serviceproviderRequestDto);
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
    /// Tests the search functionality in the serviceProvider service to ensure it can find serviceProvider based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchServiceProvider()
    {
        // Arrange
        CheckService();
        string text = "hager";

        // Act
        var result = await _serviceproviderService.SearchServiceProviderByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
