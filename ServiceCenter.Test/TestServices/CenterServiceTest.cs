//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.API.Mapping;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Application.Services;
//using ServiceCenter.Domain.Entities;
//using ServiceCenter.Test.TestPriority;
//using ServiceCenter.Test.TestSetup;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Intrinsics.X86;
//using System.Text;
//using System.Threading.Tasks;
//using CenterService = ServiceCenter.Application.Services.CenterService;

//namespace ServiceCenter.Test.TestServices;
//[TestCaseOrderer(
//ordererTypeName: "ServiceCenter.Test.TestPriority.PriorityOrderer",
//ordererAssemblyName: "ServiceCenter.Test")]

//public class CenterServiceTest
//{
//    private static CenterService _CenterService;
//    private string userEmail = "mariamabdeeen@gmail.com";
//    private CenterService CreateCenterService()
//    {

//        if (_CenterService is null)
//        {
//            var dbContext = ContextGenerator.Generator();

//            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

//            ILogger<CenterService> logger = new LoggerFactory().CreateLogger<CenterService>();

//            IUserContextService userContext = new UserContextService();

//            _CenterService = new CenterService(dbContext, mapper, logger, userContext);
//        }

//        return _CenterService;
//    }
//    private void CheckService()
//    {
//        if (_CenterService is null)
//            _CenterService = CreateCenterService();
//    }

//    /// <summary>
//    /// fuction to add Center as a test case that take   Center id , Center number , Center avaliability , center id  
//    /// </summary>
//    /// <param name="CenterNumber">Center number</param>
//    /// <param name="CenterAvaliabitlity">Center availability</param>
//    /// <param name="centerId">Center Type id</param>
//    [Theory, TestPriority(0)]
//    [InlineData("center1", 2, "spec1")]
//    public async Task AddCenter(string centerName, int hours, string specialty)
//    {
//        // Arrange
//        CheckService();
//        var CenterRequestDto = new CenterRequestDto { CenterName = centerName, OpeningHours = hours, Specialty = specialty };
//        // Act
//        var result = await _CenterService.AddCenterAsync(CenterRequestDto);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);
//    }

//    /// <summary>
//    /// fuction to get all  Centers as a test case 
//    /// </summary>
//    [Fact, TestPriority(1)]
//    public async Task GetAllCenter()
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _CenterService.GetAllCentersAsync();

//        // Assert
//        Assert.True(result.IsSuccess);

//    }

//    /// <summary>
//    /// fuction to get Center by id as a test case 
//    /// </summary>
//    /// <param name="id">Center id </param>
//    [Theory, TestPriority(2)]
//    [InlineData(1)]
//    [InlineData(6)]
//    public async Task GetByIdCenter(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _CenterService.GetCenterByIdAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }

//    /// <summary>
//    /// fuction to update Center as a test case that take   Center id , Center number , Center avaliability , center id  
//    /// </summary>
//    /// <param name="CenterNumber">Center number</param>
//    /// <param name="CenterAvaliabitlity">Center availability</param>
//    /// <param name="centerId">Center Type id</param>
//    /// <param name="expectedResult">expected result</param>
//    [Theory, TestPriority(3)]
//    [InlineData(1, "center1", 2, "spec1", true)]
//    [InlineData(1, "center1", 2, "spec1", true)]
//    public async Task UpdateCenter(int id, string centerName, int hours, string specialty, bool expectedResult)
//    {
//        //Arrange
//        CheckService();
//        var CenterRequestDto = new CenterRequestDto { CenterName = centerName, OpeningHours = hours, Specialty = specialty };

//        // Act
//        var result = await _CenterService.UpdateCenterAsync(id, CenterRequestDto);
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
//    /// fuction to remove Center as a test case that take Center id
//    /// </summary>
//    /// <param name="id">Center id </param>
//    [Theory, TestPriority(4)]
//    [InlineData(2)]
//    [InlineData(50)]
//    public async Task RemoveCenter(int id)
//    {
//        // Arrange
//        CheckService();

//        // Act
//        var result = await _CenterService.DeleteCenterAsync(id);

//        // Assert
//        if (result.IsSuccess)
//            Assert.True(result.IsSuccess);
//        else
//            Assert.False(result.IsSuccess);

//    }
//}
