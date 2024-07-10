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
public class ContactServiceTest
{
    private static ContactService _contactService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private List<ApplicationUser> _users = new List<ApplicationUser>
        {
            new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f645630",Email="hagershaaban7@gmail.com" ,UserName="hager3012"},
            new ApplicationUser() {Id = "53ae72a7-589e-4f0b-81ed-40389f654945", Email = "hagershaaban7@gmail.com", UserName = "hager1230"},

        };
    private ContactService CreateContactService()
    {

        if (_contactService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ContactService> logger = new LoggerFactory().CreateLogger<ContactService>();

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


            _contactService = new ContactService(dbContext, mapper, logger,  authService, userContext);
        }

       

        return _contactService;
    }
    private void CheckService()
    {
        if (_contactService is null)
            _contactService = CreateContactService();
    }

    /// <summary>
    /// fuction to add contact as a test case that take   contact name , contact phone number , Center avaliability , center id  ,email address
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("hagershabaan7@gmail.com")]
    public async Task AddContact(string name)
    {
        // Arrange
        CheckService();
        var contactRequestDto = new ContactRequestDto {FirstName=name };
        // Act
        var result = await _contactService.AddContactAsync(contactRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  contactes as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllContact()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contactService.GetAllContactsAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update contact as a test case that take   status
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("123e4567-e89b-12d3-a456-426614174000", ContactStatus.Customer, true)]
    [InlineData("123e4567-e89b-12d3-a456-426614174659", ContactStatus.Lead, false)]
    public async Task UpdateContact(string id, ContactStatus status, bool expectedResult)
    {
        //Arrange
        CheckService();
        Guid guid = Guid.Parse(id);
        // Act
        var result = await _contactService.UpdateContactStatusAsync(guid, status);
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
    /// fuction to get contact by id as a test case 
    /// </summary>
    /// <param name="id">contact id </param>
    [Theory, TestPriority(2)]
    [InlineData("0d133c1a-804f-4548-8f7e-8c3f504844u0")]
    [InlineData("nksalknsdn")]
    public async Task GetByIdContact(string guidString)
    {
        // Arrange
        Guid guid = Guid.Parse(guidString);
        CheckService();

        // Act
        var result = await _contactService.GetContacttByIdAsync(guid);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
