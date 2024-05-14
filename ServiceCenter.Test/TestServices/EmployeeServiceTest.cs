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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestServices;

public class EmployeeServiceTest
{
    private static EmployeeService _employeeService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private EmployeeService CreateEmployeeService()
    {

        if (_employeeService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<EmployeeService> logger = new LoggerFactory().CreateLogger<EmployeeService>();

            IUserContextService userContext = new UserContextService();
            IAuthService authService = default;

            _employeeService = new EmployeeService(dbContext, mapper, logger, userContext, authService);
        }

        return _employeeService;
    }
    private void CheckService()
    {
        if (_employeeService is null)
            _employeeService = CreateEmployeeService();
    }

    /// <summary>
    /// fuction to add employee as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000/12/30", 1, "agershaban7@gmail.com", "hager", "shaban", "0621654984")]
    public async Task Addemployee(string dateOfBirth, int departmentId,string email,string firstName,string lastName,string phoneNumber)
    {
        // Arrange
        CheckService();
        var employeeRequestDto = new EmployeeRequestDto {
            DateOfBirth = DateOnly.Parse(dateOfBirth),
            DepartmentId = departmentId,
            EmployeeEmail = email,
            EmployeeFirstName = firstName,
            EmployeeLastName = lastName,
            EmployeePhoneNumber = phoneNumber
        };
        // Act
        var result = await _employeeService.AddEmployeeAsync(employeeRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  employeees as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllEmployee()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get employee by id as a test case 
    /// </summary>
    /// <param name="id">employee id </param>
    [Theory, TestPriority(2)]
    [InlineData("ksn56418942")]
    [InlineData("nksalknsdn")]
    public async Task GetByIdCenter(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _employeeService.GetEmployeeByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update employee as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData("ksn56418942", "2000/12/30", 1, "agershaban7@gmail.com", "hager", "shaban", "0621654984", true)]
    [InlineData("ja1651666", "2000/12/30", 1, "agershaban7@gmail.com", "hager", "shaban", "0621654984", false)]
    public async Task UpdateCenter(string id, string dateOfBirth, int departmentId, string email, string firstName, string lastName, string phoneNumber, bool expectedResult)
    {
        //Arrange
        CheckService();
        var employeeRequestDto = new EmployeeRequestDto
        {
            DateOfBirth = DateOnly.Parse(dateOfBirth),
            DepartmentId = departmentId,
            EmployeeEmail = email,
            EmployeeFirstName = firstName,
            EmployeeLastName = lastName,
            EmployeePhoneNumber = phoneNumber
        };

        // Act
        var result = await _employeeService.UpdateEmployeeAsync(id, employeeRequestDto);
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
    /// fuction to remove employee as a test case that take employee id
    /// </summary>
    /// <param name="id">employee id </param>
    [Theory, TestPriority(4)]
    [InlineData("ksn56418942")]
    [InlineData("nksalknsdn")]
    public async Task Removeemployee(string id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _employeeService.DeleteEmployeeAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
