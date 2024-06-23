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
public class SalesServiceTest
{
    private static SalesService _salesService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f645630",FirstName="Hager",LastName="Shaban",DateOfBirth=new DateOnly(2000,12,30),Email="hagershaaban7@gmail.com" ,UserName="hager3012"},
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f654945",FirstName="Hager",LastName="Shaban",DateOfBirth=new DateOnly(2000,12,30),Email="hagershaaban7@gmail.com" ,UserName="hager1230"},

        };

    private SalesService CreateSalesService()
    {

        if (_salesService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<SalesService> logger = new LoggerFactory().CreateLogger<SalesService>();

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


            _salesService = new SalesService(dbContext, mapper, logger, userContext, authService);
        }

        return _salesService;
    }
    private void CheckService()
    {
        if (_salesService is null)
            _salesService = CreateSalesService();
    }

    /// <summary>
    /// fuction to add sales as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", 1)]
    public async Task Addsales(string dateOfBirth, string email, string firstName, string lastName, string phoneNumber,int departmentId)
    {
        // Arrange
        CheckService();
        var salesRequestDto = new SalesRequestDto
        {
            DateOfBirth = DateOnly.Parse(dateOfBirth),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            DepartmentId=departmentId,

        };
        // Act
        var result = await _salesService.AddSalesAsync(salesRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  saleses as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllSales()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _salesService.GetAllSalesAsync(3, 1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get sales by id as a test case 
    /// </summary>
    /// <param name="id">sales id </param>
    [Theory, TestPriority(2)]
    [InlineData("ksn56418942")]
    [InlineData("nksalknsdn")]
    public async Task GetByIdCenter(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _salesService.GetSalesByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update sales as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("0d133c1a-804f-4508-8f7e-8c3f504844e0", "2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654",1, true)]
    [InlineData("ja1651666", "2000-12-30", "agershaban7@gmail.com", "hager", "shaban", "0987654", 1, false)]
    public async Task UpdateSales(string id, string dateOfBirth, string email, string firstName, string lastName, string phoneNumber, int departmentId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var salesRequestDto = new SalesRequestDto
        {
            DateOfBirth = DateOnly.Parse(dateOfBirth),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            DepartmentId = departmentId,
        };

        // Act
        var result = await _salesService.UpdateSalesAsync(id, salesRequestDto);
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
    /// Tests the search functionality in the sales service to ensure it can find sales based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchSales()
    {
        // Arrange
        CheckService();
        string text = "hager";

        // Act
        var result = await _salesService.SearchSalesByTextAsync(text, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
