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
public class RecruitmentRecordTest
{
    private static RecruitmentRecordService _recruitmentRecordService;
    private string userEmail = "passant7@gmail.com";
    private RecruitmentRecordService CreateRecruitmentRecordService()
    {

        if (_recruitmentRecordService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<RecruitmentRecordService> logger = new LoggerFactory().CreateLogger<RecruitmentRecordService>();

            IUserContextService userContext = new UserContextService();

            _recruitmentRecordService = new RecruitmentRecordService(dbContext, mapper, logger, userContext);
        }

        return _recruitmentRecordService;
    }
    private void CheckService()
    {
        if (_recruitmentRecordService is null)
            _recruitmentRecordService = CreateRecruitmentRecordService();
    }

    /// <summary>
    /// fuction to add recruitmentRecord as a test case that take   recruitmentRecord id , recruitmentRecord number , recruitmentRecord avaliability , recruitmentRecordtype id  
    /// </summary>
    /// <param name="recruitmentRecordNumber">recruitmentRecord number</param>
    /// <param name="recruitmentRecordAvaliabitlity">recruitmentRecord availability</param>
    /// <param name="recruitmentRecordTypeId">recruitmentRecord Type id</param>
    [Theory, TestPriority(0)]
    [InlineData(1, Status.Pending, "45yi72a7-589e-4f0b-81ed-40389f683027")]
    public async Task AddrecruitmentRecord(int per, Status per1, string empId)
    {
        // Arrange
        CheckService();
        var recruitmentRecordRequestDto = new RecruitmentRecordRequestDto { DepartmentId = per, Status = per1, ApplicantId = empId };
        // Act
        var result = await _recruitmentRecordService.AddRecruitmentRecordAsync(recruitmentRecordRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  recruitmentRecords as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllRecruitmentRecord()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _recruitmentRecordService.GetAllRecruitmentRecordsAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get recruitmentRecord by id as a test case 
    /// </summary>
    /// <param name="id">recruitmentRecord id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdRecruitmentRecord(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _recruitmentRecordService.GetRecruitmentRecordByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update recruitmentRecord as a test case that take   recruitmentRecord id , recruitmentRecord number , recruitmentRecord avaliability , recruitmentRecordtype id  
    /// </summary>
    /// <param name="recruitmentRecordNumber">recruitmentRecord number</param>
    /// <param name="recruitmentRecordAvaliabitlity">recruitmentRecord availability</param>
    /// <param name="recruitmentRecordTypeId">recruitmentRecord Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, 1, Status.Pending, "78ty72a7-589e-4f0b-81ed-40389f683610", true)]
    [InlineData(10, 1, Status.Pending, "78ty72a7-589e-4f0b-81ed-40389f683610", false)]
    public async Task UpdateRecruitmentRecord(int id, int per, Status per1, string empId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var recruitmentRecordRequestDto = new RecruitmentRecordRequestDto { DepartmentId = per, Status = per1, ApplicantId = empId };

        // Act
        var result = await _recruitmentRecordService.UpdateRecruitmentRecordAsync(id, recruitmentRecordRequestDto);
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
    /// fuction to remove recruitmentRecord as a test case that take recruitmentRecord id
    /// </summary>
    /// <param name="id">recruitmentRecord id </param>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoverecruitmentRecord(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _recruitmentRecordService.DeleteRecruitmentRecordAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
}