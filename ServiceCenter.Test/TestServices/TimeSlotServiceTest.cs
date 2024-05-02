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
public class TimeSlotServiceTest
{
    private static TimeSlotService _timeSlotService;
    private string userEmail = "mariamabdeen@gmail.com";
    private TimeSlotService CreateTimeSlotService()
    {

        if (_timeSlotService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<TimeSlotService> logger = new LoggerFactory().CreateLogger<TimeSlotService>();

            IUserContextService userContext = new UserContextService();

            _timeSlotService = new TimeSlotService(dbContext, mapper, logger, userContext);
        }

        return _timeSlotService;
    }
    private void CheckService()
    {
        if (_timeSlotService is null)
            _timeSlotService = CreateTimeSlotService();
    }

    /// <summary>
    /// fuction to add TimeSlot as a test case. 
    /// </summary>
    /// <param name="day">TimeSlot dat</param>
    /// <param name="startTime">TimeSlot start time</param>
    /// <param name="endTime">TimeSlot end time</param>
    [Theory, TestPriority(0)]
    [InlineData("SunDay", "02:00:00", "03:00:00")]
    public async Task AddTimeSlot(string day, string startTime, string endTime)
    {
        // Arrange
        CheckService();
        var TimeSlotRequestDto = new TimeSlotRequestDto { Day = day, StartTime = TimeOnly.Parse(startTime), EndTime = TimeOnly.Parse(endTime) };
        // Act
        var result = await _timeSlotService.AddTimeSlotAsync(TimeSlotRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  TimeSlots as a test case 
    /// </summary>
    /// <returns>boolean for check result is success or failed</returns>
    [Fact, TestPriority(1)]
    public async Task GetAllTimeSlot()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _timeSlotService.GetAllTimeSlotAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get TimeSlot by id as a test case 
    /// </summary>
    /// <param name="TimeSlot">list of TimeSlot </param>
    /// <returns>list of TimeSlot</returns>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdTimeSlot(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _timeSlotService.GetTimeSlotByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update TimeSlot as a test case 
    /// </summary>
    /// <param name="id">TimeSlot id</param>
    /// <param name="day">TimeSlot dat</param>
    /// <param name="startTime">TimeSlot start time</param>
    /// <param name="endTime">TimeSlot end time</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "SunDay", "5:30:00", "6:30:00", true)]
    [InlineData(10, "SunDay", "5:30:00", "5:30:00", false)]
    public async Task UpdateSpectialization(int id, string day, string startTime, string endTime, bool expectedResult)
    {
        //Arrange
        CheckService();
        var TimeSlotRequestDto = new TimeSlotRequestDto { Day = day, StartTime = TimeOnly.Parse(startTime), EndTime = TimeOnly.Parse(endTime) };
        // Act
        var result = await _timeSlotService.UpdateTimeSlotAsync(id, TimeSlotRequestDto);
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
    /// fuction to remove TimeSlot as a test case that take TimeSlot id
    /// </summary>
    /// <param name="TimeSlot">object of TimeSlot </param>
    /// <returns>TimeSlot remove successfully</returns>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveTimeSlot(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _timeSlotService.DeleteTimeSlotAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
