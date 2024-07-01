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
public class LeaveTypeServiceTest
{
    private static LeaveTypeService _leaveTypeService;
    private string userEmail = "hagershaaban7@gmail.com";
    private LeaveTypeService CreateLeaveTypeService()
    {

        if (_leaveTypeService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<LeaveTypeService> LeaveTypeLogger = new LoggerFactory().CreateLogger<LeaveTypeService>();

            IUserContextService userContext = new UserContextService();
            _leaveTypeService = new LeaveTypeService(dbContext, mapper, LeaveTypeLogger, userContext);
        }

        return _leaveTypeService;
    }

    private void CheckService()
    {
        if (_leaveTypeService is null)
            _leaveTypeService = CreateLeaveTypeService();
    }

    /// <summary>
    /// fuction to add leaveType test  as a test case . 
    /// </summary>
    /// <param name="id">leaveType test order id</param>
    /// <param name="typeName">typeName for leaveType test</param>
    /// <param name="description">pateient id</param>
    [Theory, TestPriority(0)]
    [InlineData("Sick", "sickkk")]
    public async Task AddLeaveType(string typeName, string description)
    {
        // Arrange
        CheckService();

        var LeaveTypeTypeDto = new LeaveTypeRequestDto
        {

            TypeName = typeName


        };
        // Act
        var result = await _leaveTypeService.AddLeaveTypeAsync(LeaveTypeTypeDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// fuction to update leaveType test  as a test case .
    /// </summary>
    /// <param name="id">leaveType test order id</param>
    /// <param name="typeName">typeName for leaveType test</param>
    /// <param name="description">pateient id</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "Sick", "sickkk", true)]
    [InlineData(10, "Sick", "sickkk", false)]

    public async Task UpdateLeaveTypeAsync(int id, string typeName, string description, bool expectedResult)
    {
        //Arrange
        CheckService();
        var leaveTypeDto = new LeaveTypeRequestDto
        {

            TypeName = typeName
        };
        // Act
        var result = await _leaveTypeService.UpdateLeaveTypeAsycn(id, leaveTypeDto);
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
    /// fuction to get leaveType test  by id as a test case that take leaveType test order id
    /// </summary>
    /// <param name="id"> leaveType test  id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(20)]
    public async Task GetByIdLeaveType(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _leaveTypeService.GetLeaveTypeByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to remove leaveType test  as a test case.
    /// </summary>
    /// <param name="id">leaveType test  id</param>
    [Theory, TestPriority(4)]
    [InlineData(3)]
    [InlineData(30)]
    public async Task RemoveLeaveType(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _leaveTypeService.DeleteLeaveTypeAsync(id);

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
    public async Task GetAllLeaveType()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _leaveTypeService.GetAllLeaveTypesAsync(0, 0);

        // Assert
        Assert.True(result.IsSuccess);

    }
}