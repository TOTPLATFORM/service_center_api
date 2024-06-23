using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
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
public class ReportServiceTest
{
    private static ReportService _reportService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private ReportService CreatereportService()
    {

        if (_reportService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ReportService> logger = new LoggerFactory().CreateLogger<ReportService>();

            IUserContextService userContext = new UserContextService();

            _reportService = new ReportService(dbContext, mapper, logger, userContext);
        }

        return _reportService;
    }
    private void CheckService()
    {
        if (_reportService is null)
            _reportService = CreatereportService();
    }

    /// <summary>
    /// fuction to get all  reports as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllreport()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _reportService.GetAllReportAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get report by id as a test case 
    /// </summary>
    /// <param name="id"> report id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdreport(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _reportService.GetReportByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add report as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData("Desc1", "Product", "2000-12-30", "53ae72a7-589e-4f0b-81ed-40381")]
    public async Task AddReport(string task, string priority, string dueDate, string salesId)
    {
        // Arrange
        CheckService();
        var reportRequestDto = new ReportRequestDto { Task = task, Priority = priority, DueDate = DateTime.Parse(dueDate), SalesId = salesId };

        // Act
        var result = await _reportService.AddReportAsync(reportRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove report as a test case that take report id
    /// </summary>
    /// <param name="id"> report id</param>
    [Theory, TestPriority(6)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveReport(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _reportService.DeleteReportAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to update report as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">report id</param>  
    [Theory, TestPriority(3)]
    [InlineData(1, true)]
    [InlineData(30, false)]
    public async Task UpdateReport(int id, bool expectedResult)
    {
        // Arrange
        CheckService();
        var reportRequestDto = new ReportRequestDto { Task = "task", Priority = "priority", DueDate = DateTime.Parse("2000-12-30"), SalesId = "53ae72a7-589e-4f0b-81ed-40381" };

        // Act
        var result = await _reportService.UpdateReportAsync(id, reportRequestDto);

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
    /// fuction to update Report as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">Report id</param>  
    [Theory, TestPriority(4)]
    [InlineData(9, ReportStatus.Good, true)]
    [InlineData(78, ReportStatus.Bad, false)]
    public async Task UpdateReportStatus(int id, ReportStatus reportStatus, bool expectedResult)
    {
        // Arrange
        CheckService(); 


        // Act
        var result = await _reportService.UpdateReportStatusAsync(id, reportStatus);

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
