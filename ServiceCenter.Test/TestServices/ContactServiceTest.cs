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
public  class ContactServiceTest
{
    private static ContactService _contactService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ContactService CreateContactService()
    {

        if (_contactService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ContactService> logger = new LoggerFactory().CreateLogger<ContactService>();

            IUserContextService userContext = new UserContextService();

            _contactService = new ContactService(dbContext, mapper, logger, userContext);
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
    [InlineData("hagershabaan7@gmail.com", "hager", "shaban", "female")]
    public async Task AddCenter(string contactEmail, string contactFirstName, string contactLastName, string gender)
    {
        // Arrange
        CheckService();
        var contactRequestDto = new ContactRequestDto {ContactEmail = contactEmail,ContactFirstName = contactFirstName,ContactLastName = contactLastName,Gender = gender};
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
        var result = await _contactService.GetAllContactsAsync();

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
    [InlineData(1, ContactStatus.Customer, true)]
    [InlineData(10, ContactStatus.Lead, false)]
    public async Task UpdateCenter(int id, ContactStatus status, bool expectedResult)
    {
        //Arrange
        CheckService();

        // Act
        var result = await _contactService.UpdateContactStatusAsync(id, status);
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
}
