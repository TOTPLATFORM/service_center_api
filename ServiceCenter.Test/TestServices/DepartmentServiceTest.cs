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


public class DepartmentServiceTest
{
    private static DepartmentService _departmentService;
    private string userEmail = "mariamAbdeen@gmail.com";
    private DepartmentService CreateDepartmentService()
    {

        if (_departmentService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<DepartmentService> departmentLogger = new LoggerFactory().CreateLogger<DepartmentService>();

            IUserContextService userContext = new UserContextService();
            _departmentService = new DepartmentService(dbContext, mapper, departmentLogger, userContext);
        }

        return _departmentService;
    }

    private void CheckService()
    {
        if (_departmentService is null)
            _departmentService = CreateDepartmentService();
    }

    /// <summary>
    /// fuction to add department as a test case . 
    /// </summary>
    /// <param name="DepartName">item name</param>
    [Theory, TestPriority(0)]
    [InlineData("Department Test",2)]
    [InlineData("Department Test adfa2",2)]
    public async Task AddDepartment(string DepartName,int centerId)
    {
        // Arrange
        CheckService();
        var departmentRequestDto = new DepartmentRequestDto { DepartmentName = DepartName,CenterId=2 };
        // Act
        var result = await _departmentService.AddDepartmentAsync(departmentRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to update department as a test case .
    /// </summary>
    /// <param name="departName">department name</param>
    [Theory, TestPriority(3)]
    [InlineData(3, "Department Test Updated", true)]
    [InlineData(100, "Department Test 2 update", false)]
    public async Task UpdateDepartmentAsync(int id, string departName, bool expectedResult)
    {
        //Arrange
        CheckService();
        var departmentRequestDto = new DepartmentRequestDto { DepartmentName = departName };
        // Act
        var result = await _departmentService.UpdateDepartmentAsync(id, departmentRequestDto);
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
    /// fuction to get all  departments as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllDepartments()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _departmentService.GetAllDepartmentsAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get department by id as a test case that take department id
    /// </summary>
    /// <param name="id"> department id</param>
    [Theory, TestPriority(2)]
    [InlineData(3)]
    [InlineData(20)]
    public async Task GetByIdDepartment(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _departmentService.GetDepartmentByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to remove department as a test case.
    /// </summary>
    /// <param name="id">department id</param>
    [Theory, TestPriority(4)]
    [InlineData(3)]
    [InlineData(30)]
    public async Task RemoveDepartment(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _departmentService.DeleteDepartmentAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
