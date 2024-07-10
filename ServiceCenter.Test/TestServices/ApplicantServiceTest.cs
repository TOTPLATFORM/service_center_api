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
    /// fuction to update applicant as a test case .
    /// </summary>
    /// <param name="id">applicant id</param>
    /// <param name="departmenId">department id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("78ty72a7-589e-4f0b-81ed-40389f683610",9 , true)]
    [InlineData("45yi72a7-589e-4f0b-81ed-40389f683090", 5, false)]
    public async Task UpdateApplicantAsync(string id,int departmenId , bool expectedResult)
    {
        //Arrange
        CheckService();
        var ApplicantRequestDto = new ApplicantRequestDto
        {
            DepartmentId = departmenId,
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
    /// fuction to get all  applicant as a test case 
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
    /// fuction to get applicant by id as a test case that take applicant id
    /// </summary>
    /// <param name="id"> Applicant id</param>
    [Theory, TestPriority(2)]
    [InlineData("53ae72a7-589e-4f0b-81ed-4038169498")]
    [InlineData("53ae72a7-589e-4f0b-81ed-40389f68302765165")]
    public async Task GetByIdApplicant(string id)
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
