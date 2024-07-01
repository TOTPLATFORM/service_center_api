using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceCenter.Application.Services;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.JWT;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Test.TestPriority;
using ServiceCenter.Test.TestSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.API.Mapping;

namespace ServiceCenter.Test.TestServices;
[TestCaseOrderer(
ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
ordererAssemblyName: "ServiceCenter.Test")]
public class ApplicantServiceTest
{
    private static ApplicantService _applicantService;

    private List<ApplicationUser> _users = new List<ApplicationUser>
    {
        new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f683040" },
        new ApplicationUser() { Id = "53ae72a7-589e-4f0b-81ed-40389f689027565" }
    };

    private ApplicantService CreateApplicantService()
    {

        if (_applicantService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ApplicationUser> userLogger = new LoggerFactory().CreateLogger<ApplicationUser>();
            var userStore = new UserStore<ApplicationUser>(dbContext);
            UserManager<ApplicationUser> userManager = InMemoryUserStore.MockUserManager(_users).Object;

            var jwtOptions = Options.Create(new JWT
            {
                Issuer = "TOTPlatform",
                Audience = "PlatformUsers",
                Key = "QqEz6jAMz8LIsXLcm4GtSOp24cQ50LxPlY/cgZ4NCZQ=",
                DurationInDays = 1
            });
            ILogger<ApplicantService> ApplicantLogger = new LoggerFactory().CreateLogger<ApplicantService>();
            var authService = new AuthService(userManager, userLogger, mapper, jwtOptions);
            _applicantService = new ApplicantService(dbContext, mapper, ApplicantLogger, authService);
        }

        return _applicantService;
    }

    private void CheckService()
    {
        if (_applicantService is null)
            _applicantService = CreateApplicantService();
    }
    /// <summary>
    /// fuction to update Applicant as a test case .
    /// </summary>
    /// <param name="ApplicantEmail">Applicant email</param>
    /// <param name="ApplicantFirstName">Applicant first name</param>
    /// <param name="ApplicantLastName">Applicant last name</param> 
    /// <param name="ApplicantPhoneNumber">Applicant phone number</param>
    /// <param name="Gender">Applicant gender</param>
    /// <param name="id">Applicant id</param>
    /// <param name="Password">Applicant password</param>
    /// <param name="SpecializationId">specialization id</param>
    /// <param name="UserName">Applicant user name</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("78ty72a7-589e-4f0b-81ed-40389f683610", "nejosif605@hisotyr.com", "Hager", "shaaban", "01065695783", true)]
    [InlineData("45yi72a7-589e-4f0b-81ed-40389f683020", "nejosif605@hisotyr.com", "Hager", "shaaban", "01065695783", false)]
    public async Task UpdateApplicantAsync(string id, string ApplicantEmail, string ApplicantFirstName, string ApplicantLastName, string ApplicantPhoneNumber, bool expectedResult)
    {
        //Arrange
        CheckService();
        var ApplicantRequestDto = new ApplicantRequestDto
        {
            //Email = ApplicantEmail,
            //FirstName = ApplicantFirstName,
            //LastName = ApplicantLastName,
            //PhoneNumber = ApplicantPhoneNumber
        };
        // Act
        var result = await _applicantService.UpdateApplicantAsync(id, ApplicantRequestDto);
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
    /// fuction to get all  Applicant as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllApplicant()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _applicantService.GetAllApplicantsAsync(0, 0);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get Applicant by id as a test case that take Applicant id
    /// </summary>
    /// <param name="id"> Applicant id</param>
    [Theory, TestPriority(2)]
    [InlineData("53ae72a7-589e-4f0b-81ed-4038169498")]
    [InlineData("53ae72a7-589e-4f0b-81ed-40389f68302765165")]
    public async Task GetByIdApplicant_ReturnResult(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _applicantService.GetApplicantByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to search for applicant by applicant name as a test case that take text
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchApplicants()
    {
        // Arrange
        CheckService();
        string text = "name 1";

        // Act
        var result = await _applicantService.SearchApplicantByTextAsync(text, 0, 0);

        // Assert
        Assert.True(result.IsSuccess);
    }
}
