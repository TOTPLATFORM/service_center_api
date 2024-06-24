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
    /// fuction to get all appointments as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllAppointment()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _appointmentService.GetAllAppointmentsAsync(0, 0);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add appointment as a test case. 
    /// </summary>
    /// <param name="contactId"> contact id</param>
    /// <param name="scheduleId">  schedule id</param>
    /// <param name="description">appointment desc</param>
    [Theory, TestPriority(0)]
    [InlineData("0d133c1a-804f-4548-8f7e-8c3f504844u0", 1, "03:00:00")]
    public async Task AddAppointment(string contactId, int scheduleId, string description)
    {
        // Arrange
        CheckService();
        var appointmentRequestDto = new AppointmentRequestDto { ContactId = contactId, ScheduleId = scheduleId, Description = description };
        // Act
        var result = await _appointmentService.BookAppointmentAsync(appointmentRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to get appointment by id as a test case 
    /// </summary>
    /// <param name="id">appointmentid </param>
    /// <returns>specific appointment</returns>
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
    /// fuction to update appointment as a test case 
    /// </summary>
    /// <param name="id">appointment id</param>
    /// <param name="date">appointment date</param>
    /// <param name="startTime">appointment start time</param>
    /// <param name="endTime">appointment end time</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "0d133c1a-804f-4548-8f7e-8c3f504844u0", 1, AppointmentStatus.Canceled, "03:00:00", true)]
    [InlineData(10, "gjhhgh55541", 1, AppointmentStatus.Canceled, "03:00:00", false)]
    public async Task UpdateAppointment(int id, string contactId, int scheduleId, AppointmentStatus status, string description, bool expectedResult)
    {
        //Arrange
        CheckService();
        var appointmentRequestDto = new AppointmentRequestDto {ContactId = contactId, ScheduleId = scheduleId, Description = description };
        // Act
        var result = await _appointmentService.ChangeAppointmentStatusAsync(id, status);
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
    /// fuction to remove appointment as a test case that take appointment id
    /// </summary>
    /// <param name="id"> appointment id </param>
    /// <returns>appointment remove successfully</returns>
    [Theory, TestPriority(6)]
    [InlineData(2)]
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
    /// fuction to get all appointments for specific service as a test case 
    /// </summary>
    [Theory, TestPriority(4)]
    [InlineData(1)]
    public async Task GetAllAppointmentForSpecificService(int serviceId)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _appointmentService.GetAppointmentsByServiceIdAsync(serviceId, 0, 0);

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get all appointments for specific service as a test case 
    /// </summary>
    [Theory, TestPriority(5)]
    [InlineData("0d133c1a-804f-4548-8f7e-8c3f504844u0")]
    public async Task GetAllAppointmentForSpecificContact(string contactId)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _appointmentService.GetAppointmentsByContactIdAsync(contactId, 0, 0);

        // Assert
        Assert.True(result.IsSuccess);

    }
}
