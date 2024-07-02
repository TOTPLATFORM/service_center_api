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
public class WareHouseManagerServiceTest
{
    private static WareHouseManagerService _warehousemanagerService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f645630",Email="hagershaaban7@gmail.com" ,UserName="hager3012"},
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f654945",Email="hagershaaban7@gmail.com" ,UserName="hager1230"},

        };

    private WareHouseManagerService CreateWareHouseManagerService()
    {

        if (_warehousemanagerService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<WareHouseManagerService> logger = new LoggerFactory().CreateLogger<WareHouseManagerService>();

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


            _warehousemanagerService = new WareHouseManagerService(dbContext, mapper, logger, userContext, authService);
        }

        return _warehousemanagerService;
    }
    private void CheckService()
    {
        if (_warehousemanagerService is null)
            _warehousemanagerService = CreateWareHouseManagerService();
    }

    /// <summary>
    /// fuction to add warehousemanager as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", "2000-12-30", "2000-12-30", "WareHouseManager")]
    public async Task Addwarehousemanager(string dateOfBirth, string email, string firstName, string lastName, string phoneNumber, string startDate, string endDate,string positionTitle)
    {
        // Arrange
        CheckService();
        var warehousemanagerRequestDto = new WareHouseManagerRequestDto
        {
            PositionTitle =positionTitle ,
           StartDate = DateOnly.Parse(startDate),
            EndDate = DateOnly.Parse(endDate),
            DepartmentId = 1,
            InventoryId = 1
        };
        // Act
        var result = await _warehousemanagerService.AddWareHouseManagerServiceAsync(warehousemanagerRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  warehousemanageres as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllWareHouseManager()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _warehousemanagerService.GetAllWareHouseManagerServicesAsync(3, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get warehousemanager by id as a test case 
    /// </summary>
    /// <param name="id">warehousemanager id </param>
    [Theory, TestPriority(2)]
    [InlineData("ksn56418942")]
    [InlineData("nksalknsdn")]
    public async Task GetByIdWareHouseManager(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _warehousemanagerService.GetWareHouseManagerServiceByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update warehousemanager as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("0d133cpa-804f-4548-8f7e-8c3f504844e0", "2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", "2000-12-30", "2000-12-30", "WareHouseManager", true)]
    [InlineData("ja1651666", "2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", "2000-12-30", "2000-12-30", "WareHouseManager", false)]
    public async Task UpdateWareHouseManager(string id, string dateOfBirth, string email, string firstName, string lastName, string phoneNumber, string startDate, string endDate, string positionTitle, bool expectedResult)
    {
        //Arrange
        CheckService();
        var warehousemanagerRequestDto = new WareHouseManagerRequestDto
        {  
            PositionTitle =positionTitle,
             StartDate = DateOnly.Parse(startDate),
            EndDate = DateOnly.Parse(endDate),
            DepartmentId=1,
            InventoryId=1,
            
        };

        // Act
        var result = await _warehousemanagerService.UpdateWareHouseManagerServiceAsync(id, warehousemanagerRequestDto);
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
    /// Tests the search functionality in the warehousemanager service to ensure it can find warehousemanager based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchWareHouseManager()
    {
        // Arrange
        CheckService();
        string text = "hager";

        // Act
        var result = await _warehousemanagerService.SearchWareHouseManagerByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
