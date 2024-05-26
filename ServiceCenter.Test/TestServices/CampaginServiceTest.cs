using AutoMapper;
using HMSWithLayers.Application.Services;
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

public  class CampaginServiceTest
{
    private static CampaginService _campaginService;
    private string userEmail = "mariamAbdeen@gmail.com";
    private CampaginService CreateCampaginService()
    {

        if (_campaginService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<CampaginService> campaginLogger = new LoggerFactory().CreateLogger<CampaginService>();

            IUserContextService userContext = new UserContextService();
            _campaginService = new CampaginService(dbContext, mapper, campaginLogger, userContext);
        }

        return _campaginService;
    }

    private void CheckService()
    {
        if (_campaginService is null)
            _campaginService = CreateCampaginService();
    }
    /// <summary>
    /// fuction to add Campagin as a test case that take  
    /// </summary>
    /// <param name="goals">goals of campagin</param>
    /// <param name="endDate">End Date</param>
    /// <param name="startDate">start Date</param>
    /// <param name="CampaginName">campagin name</param>
    /// <param name="CampaginDescription">campagin description</param>
    /// <param name="budget">budget</param>
    [Theory, TestPriority(0)]
    [InlineData(20, "2000/12/30", "2000/12/30", "campagin1", "campagin is done", "goal1")]
    public async Task AddCampagin(int budget, string endDate, string startDate, string CampaginName, string CampaginDescription,string goals)
    {
        // Arrange
        CheckService();
        var CampaginRequestDto = new CampaginRequestDto
        {
            Goals=goals,
            EndDate = DateOnly.Parse(endDate),
            StartDate = DateOnly.Parse(startDate),
            CampaginName = CampaginName,
            CampaginDescription = CampaginDescription,
            Budget = budget
        };
        // Act
        var result = await _campaginService.AddCampaginAsync(CampaginRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to get all  Campagines as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllCampagin()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _campaginService.GetAllCampaginsAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }



}
