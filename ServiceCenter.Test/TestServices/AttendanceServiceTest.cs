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
public class AttendanceServiceTest
{
    private static AttendanceService _attendanceService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private AttendanceService CreateAttendanceService()
    {

        if (_attendanceService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<AttendanceService> logger = new LoggerFactory().CreateLogger<AttendanceService>();

            IUserContextService userContext = new UserContextService();

            _attendanceService = new AttendanceService(dbContext, mapper, logger, userContext);
        }

        return _attendanceService;
    }
    private void CheckService()
    {
        if (_attendanceService is null)
            _attendanceService = CreateAttendanceService();
    }

        /// <summary>
        /// fuction to add attendance as a test case. 
        /// </summary>
        /// <param name="date">attendance date</param>
        /// <param name="inTime">attendance in time</param>
        /// <param name="outTime">attendance out time</param>
        [Theory, TestPriority(0)]
    [InlineData("3/11/2024", "02:00:00", "03:00:00")]
    public async Task AddAttendance(string date, string inTime, string outTime)
    {
        // Arrange
        CheckService();
        var attendanceRequestDto = new AttendanceRequestDto { AttendanceDate = DateOnly.Parse(date), ClockInTime = TimeOnly.Parse(inTime), ClockOutTime = TimeOnly.Parse(outTime) };
        // Act
        var result = await _attendanceService.AddAttendanceAsync(attendanceRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to get attendance by id as a test case 
    /// </summary>
    /// <param name="id">attendanceid </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdAttendance(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _attendanceService.GetAttendanceByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to update attendance as a test case 
    /// </summary>
    /// <param name="id">attendance id</param>
    /// <param name="date">attendance date</param>
    /// <param name="startTime">attendance start time</param>
    /// <param name="endTime">attendance end time</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "3/10/2024", "5:30:00", "12:30:00", true)]
    [InlineData(10, "3/11/2024", "5:30:00", "5:30:00", false)]
    public async Task UpdateAttendance(int id, string date, string startTime, string endTime,  bool expectedResult)
    {
        //Arrange
        CheckService();
        var attendanceRequestDto = new AttendanceRequestDto { AttendanceDate = DateOnly.Parse(date), ClockInTime = TimeOnly.Parse(startTime), ClockOutTime = TimeOnly.Parse(endTime) ,EmployeeId= "123e4567-e89b-12d3-a456-426614174000" };
        // Act
        var result = await _attendanceService.UpdateAttendanceAsync(id, attendanceRequestDto);
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
    /// fuction to remove attendance as a test case that take attendance id
    /// </summary>
    /// <param name="id"> attendance id </param>>
    [Theory, TestPriority(6)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveAttendance(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _attendanceService.DeleteAttendanceAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

}