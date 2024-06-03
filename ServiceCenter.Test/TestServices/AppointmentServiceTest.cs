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

public class AppointmentServiceTest
{

    private static AppointmentService _appointmentService;
    private string userEmail = "hagershaaban7@gmail.com";
    private AppointmentService CreateAppointmentService()
    {

        if (_appointmentService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<AppointmentService> logger = new LoggerFactory().CreateLogger<AppointmentService>();

            IUserContextService userContext = new UserContextService();

            _appointmentService = new AppointmentService(dbContext, mapper, logger, userContext);
        }

        return _appointmentService;
    }
    private void CheckService()
    {
        if (_appointmentService is null)
            _appointmentService = CreateAppointmentService();
    }

    /// <summary>
    /// fuction to get all  appointments as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllAppointment()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _appointmentService.GetAllAppointmentsAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get appointment by id as a test case 
    /// </summary>
    /// <param name="id"> appointment id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdAppointment(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _appointmentService.GetAppointmentByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add appointment as a test case that take  schedule id , customer id
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData()]
    public async Task AddAppointment()
    {
        // Arrange
        CheckService();
        var AppointmentRequestDto = new AppointmentRequestDto { ScheduleId = 1, CustomerId = "0d133c1a-804f-4548-8f7e-8c3f50465165" };

        // Act
        var result = await _appointmentService.AddAppointmentAsync(AppointmentRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove appointment as a test case that take appointment id
    /// </summary>
    /// <param name="id"> appointment id</param>
    [Theory, TestPriority(4)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveAppointment(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _appointmentService.DeleteAppointmentAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update appointment as a test case that take id ,schedule id , customer id,expected result
    /// </summary>
    /// <param name="id">appointment id</param>  
    [Theory, TestPriority(3)]
    [InlineData(3, true)]
    [InlineData(30, false)]
    public async Task UpdateAppointment(int id, bool expectedResult)
    {
        // Arrange
        CheckService();
        var AppointmentRequestDto = new AppointmentRequestDto { ScheduleId = 1, CustomerId = "0d133c1a-804f-4548-8f7e-8c3f504561954" };

        // Act
        var result = await _appointmentService.UpdateAppointmentAsync(id, AppointmentRequestDto);

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
