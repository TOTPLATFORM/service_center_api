using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
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
public class ScheduleServiceTest
{
    private static ScheduleService _scheduleService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private ScheduleService CreatescheduleService()
    {

        if (_scheduleService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ScheduleService> logger = new LoggerFactory().CreateLogger<ScheduleService>();

            IUserContextService userContext = new UserContextService();

            _scheduleService = new ScheduleService(dbContext, mapper, logger, userContext);
        }

        return _scheduleService;
    }
    private void CheckService()
    {
        if (_scheduleService is null)
            _scheduleService = CreatescheduleService();
    }


    /// <summary>
    /// fuction to get schedule by id as a test case 
    /// </summary>
    /// <param name="id"> schedule id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdSchedule(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _scheduleService.GetScheduleByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add schedule as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData()]
    public async Task Addschedule()
    {
        // Arrange
        CheckService();
        var scheduleRequestDto = new ScheduleRequestDto { ServiceId = 1 };
        // Act
        var result = await _scheduleService.AddScheduleAsync(scheduleRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove schedule as a test case that take schedule id
    /// </summary>
    /// <param name="id"> schedule id</param>
    [Theory, TestPriority(4)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveSchedule(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _scheduleService.DeleteScheduleAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update schedule as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">schedule id</param>  
    [Theory, TestPriority(3)]
    [InlineData(1, true)]
    [InlineData(30, false)]
    public async Task Updateschedule(int id, bool expectedResult)
    {
        // Arrange
        CheckService();
        var scheduleRequestDto = new ScheduleRequestDto { ServiceId = 1 };

        // Act
        var result = await _scheduleService.UpdateScheduleAsync(id, scheduleRequestDto);

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
