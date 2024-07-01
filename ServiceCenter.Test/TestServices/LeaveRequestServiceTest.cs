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
public class LeaveRequestServiceTest
{
    private static LeaveRequestService _leaveRequestService;
    private string userEmail = "hagershaaban7@gmail.com";
    private LeaveRequestService CreateLeaveRequestService()
    {

        if (_leaveRequestService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<LeaveRequestService> LeaveRequestLogger = new LoggerFactory().CreateLogger<LeaveRequestService>();

            IUserContextService userContext = new UserContextService();
            _leaveRequestService = new LeaveRequestService(dbContext, mapper, LeaveRequestLogger, userContext);
        }

        return _leaveRequestService;
    }

    private void CheckService()
    {
        if (_leaveRequestService is null)
            _leaveRequestService = CreateLeaveRequestService();
    }

    /// <summary>
    /// fuction to add leaveRequest test order as a test case . 
    /// </summary>
    /// <param name="id">leaveRequest test order id</param>
    /// <param name="status">status for leaveRequest test</param>
    /// <param name="startDate">startDate</param>
    /// <param name="endDate">endDate</param>
    /// <param name="leaveTypeId">laboratorist id</param>
    [Theory, TestPriority(0)]
    [InlineData(Status.Pending, "3/11/2024", "3/11/2024", 1)]
    public async Task AddLeaveRequest(Status status, string startDate, string endDate, int leaveTypeId)
    {
        // Arrange
        CheckService();

        var LeaveRequestRequestDto = new LeaveRequestRequestDto
        {
            StartDate = DateOnly.Parse(startDate),
            EndDate = DateOnly.Parse(endDate),
            LeaveTypeId = leaveTypeId,
            //Status =   status

        };
        // Act
        var result = await _leaveRequestService.AddLeaveRequestAsync(LeaveRequestRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// fuction to update leaveRequest test order as a test case .
    /// </summary>
    /// <param name="id">leaveRequest id</param>
    /// <param name="status">status for leaveRequest </param>
    /// <param name="startDate">startDate</param>
    /// <param name="endDate">endDate</param>
    /// <param name="leaveTypeId">leaveType id</param>
    [Theory, TestPriority(3)]
    [InlineData(1, Status.Pending, "3/10/2024", "3/11/2024", 1, true)]
    [InlineData(10, Status.Pending, "3/10/2024", "3/11/2024", 1, false)]

    public async Task UpdateLeaveRequestAsync(int id, Status status, string startDate, string endDate, int leaveTypeId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var leaveRequestDto = new LeaveRequestRequestDto
        {
            StartDate = DateOnly.Parse(startDate),
            EndDate = DateOnly.Parse(endDate),
            LeaveTypeId = leaveTypeId,
            //Status =   status
        };
        // Act
        var result = await _leaveRequestService.UpdateLeaveRequestAsycn(id, leaveRequestDto);
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
    /// fuction to get leaveRequest test order by id as a test case that take leaveRequest test order id
    /// </summary>
    /// <param name="id"> leaveRequest test order id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(20)]
    public async Task GetByIdLeaveRequest(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _leaveRequestService.GetLeaveRequestByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to remove leaveRequest test order as a test case.
    /// </summary>
    /// <param name="id">leaveRequest test order id</param>
    [Theory, TestPriority(4)]
    [InlineData(3)]
    [InlineData(30)]
    public async Task RemoveLeaveRequest(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _leaveRequestService.DeleteLeaveRequestAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get all  Leave Type as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllLeaveRequest()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _leaveRequestService.GetAllLeaveRequestsAsync(0, 0);

        // Assert
        Assert.True(result.IsSuccess);

    }
}