//using AutoMapper;
//using HMSWithLayers.Application.Services;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Application.Services;
//using ServiceCenter.Domain.Enums;
//using ServiceCenter.Test.TestPriority;
//using ServiceCenter.Test.TestSetup;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Test.TestServices;

//[TestCaseOrderer(
//ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
//ordererAssemblyName: "ServiceCenter.Test")]

//public  class CampaginServiceTest
//{
//    private static CampaginService _campaginService;
//    private string userEmail = "mariamAbdeen@gmail.com";
//    private CampaginService CreateCampaginService()
//    {

//        if (_campaginService is null)
//        {
//            var dbContext = ContextGenerator.Generator();

//            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
//            ILogger<CampaginService> campaginLogger = new LoggerFactory().CreateLogger<CampaginService>();

//            IUserContextService userContext = new UserContextService();
//            _campaginService = new CampaginService(dbContext, mapper, campaginLogger, userContext);
//        }

//        return _campaginService;
//    }

//    private void CheckService()
//    {
//        if (_campaginService is null)
//            _campaginService = CreateCampaginService();
//    }
//    /// <summary>
//    /// fuction to add Campagin as a test case that take  
//    /// </summary>
//    /// <param name="goals">goals of campagin</param>
//    /// <param name="endDate">End Date</param>
//    /// <param name="startDate">start Date</param>
//    /// <param name="CampaginName">campagin name</param>
//    /// <param name="CampaginDescription">campagin description</param>
//    /// <param name="budget">budget</param>
//    [Theory, TestPriority(0)]
//    [InlineData(20, "2000/12/30", "2000/12/30", "campagin1", "campagin is done", "goal1")]
//    public async Task AddCampagin(int budget, string endDate, string startDate, string CampaginName, string CampaginDescription,string goals)
//    {
//        // Arrange
//        CheckService();
//        var CampaginRequestDto = new CampaginRequestDto
//        {
//            Goals=goals,
//            EndDate = DateOnly.Parse(endDate),
//            StartDate = DateOnly.Parse(startDate),
//            CampaginName = CampaginName,
//            CampaginDescription = CampaginDescription,
//            Budget = budget
//        };
//        // Act
//        var result = await _campaginService.AddCampaginAsync(CampaginRequestDto);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);
//    }
//    /// <summary>
//    /// fuction to get all  Campagines as a test case 
//    /// </summary>
//    [Fact, TestPriority(1)]
//    public async Task GetAllCampagin()
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _campaginService.GetAllCampaginsAsync();

//        // Assert
//        Assert.True(result.IsSuccess);

//    }

//    /// <summary>
//    /// fuction to get campagin by id as a test case 
//    /// </summary>
//    /// <param name="id">campagin id </param>
//    [Theory, TestPriority(2)]
//    [InlineData(1)]
//    [InlineData(10)]
//    public async Task GetByIdCampagin(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _campaginService.GetCampaginByIdAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }
//    /// <summary>
//    /// fuction to update Campagin as a test case that take  
//    /// </summary>
//    /// <param name="goals">goals of campagin</param>
//    /// <param name="endDate">End Date</param>
//    /// <param name="startDate">start Date</param>
//    /// <param name="CampaginName">campagin name</param>
//    /// <param name="CampaginDescription">campagin description</param>
//    /// <param name="budget">budget</param>
//    [Theory, TestPriority(0)]
//    [InlineData(3,2, "2000/12/30", "2000/12/30", "A", "B", "Goal1",CampaginStatus.Active,true)]
//    [InlineData(9, 2, "2000/12/30", "2000/12/30", "j", "t", "Goal3", CampaginStatus.Cancelled, false)]
//    public async Task UpdateCampagin(int id,int budget, string endDate, string startDate, string CampaginName, string CampaginDescription, string goals,CampaginStatus status, bool expectedResult)
//    {
//        // Arrange
//        CheckService();
//        var CampaginRequestDto = new CampaginRequestDto
//        {
//            Goals = goals,
//            EndDate = DateOnly.Parse(endDate),
//            StartDate = DateOnly.Parse(startDate),
//            CampaginName = CampaginName,
//            CampaginDescription = CampaginDescription,
//            Budget = budget,
//            Status=status,
//        };
//        // Act
//        var result = await _campaginService.UpdateCampaginAsync(id,CampaginRequestDto);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);
//    }
//    /// <summary>
//    /// fuction to update campagin as a test case that take  campagin id , campagin name , campagin descreiption , campagin dosage and campagin id and expected result
//    /// </summary>
//    /// <param name="from">campagin from</param>
//    /// <param name="status">campagin status</param>
//    [Theory, TestPriority(3)]
//    [InlineData(3, CampaginStatus.Active, true)]
//    [InlineData(10, CampaginStatus.Completed, false)]
//    public async Task UpdateCampaginStatus(int id, CampaginStatus status, bool expectedResult)
//    {
//        //Arrange
//        CheckService();
//        // Act
//        var result = await _campaginService.UpdateCampaginStatusAsync(id, status);
//        // Assert
//        if (expectedResult)
//        {
//            Assert.True(result.IsSuccess); // Expecting successful update
//        }
//        else
//        {
//            Assert.False(result.IsSuccess); // Expecting unsuccessful update
//        }
//    }
//    /// <summary>
//    /// fuction to remove campagin as a test case that take campagin id
//    /// </summary>
//    /// <param name="id">campagin id </param>
//    [Theory, TestPriority(4)]
//    [InlineData(2)]
//    [InlineData(30)]
//    public async Task RemoveCampagin(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _campaginService.DeleteCampaginAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }
//}
