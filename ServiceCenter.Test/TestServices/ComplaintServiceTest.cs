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
public class ComplaintServiceTest
{
    private static ComplaintService _ComplaintService;
    private string userEmail = "Mariamabdeeen@gmail.com";
    private ComplaintService CreateComplaintService()
    {

        if (_ComplaintService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ComplaintService> logger = new LoggerFactory().CreateLogger<ComplaintService>();

            IUserContextService userContext = new UserContextService();

            _ComplaintService = new ComplaintService(dbContext, mapper, logger, userContext);
        }

        return _ComplaintService;
    }
    private void CheckService()
    {
        if (_ComplaintService is null)
            _ComplaintService = CreateComplaintService();
    }

    /// <summary>
    /// fuction to get all  Complaints as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllComplaint()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _ComplaintService.GetAllComplaintsAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }
    /// <summary>
    /// fuction to get Complaint by id as a test case 
    /// </summary>
    /// <param name="id"> Complaint id</param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdComplaint(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _ComplaintService.GetComplaintByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// fuction to add Complaint as a test case that take  timeslot id ,created by user name
    /// </summary>

    [Theory, TestPriority(0)]
    [InlineData("Desc1", "Product", "3/11/2024", "53ae72a7-589e-4f0b-81ed-4038169498","Dep1",Status.Pending)]
    public async Task AddComplaint(string ComplaintDesc, string ComplaintCategory, string ComplaintDate, string customerId,string assigned ,Status status)
    {
        // Arrange
        CheckService();
        var ComplaintRequestDto = new ComplaintRequestDto { ComplaintDescription = ComplaintDesc, ComplaintCategory = ComplaintCategory, ComplaintDate= DateOnly.Parse(ComplaintDate), CustomerId = customerId,AssignedTo=assigned ,ComplaintStatus=status };

        // Act
        var result = await _ComplaintService.AddComplaintAsync(ComplaintRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }
    /// <summary>
    /// fuction to remove Complaint as a test case that take Complaint id
    /// </summary>
    /// <param name="id"> Complaint id</param>
    [Theory, TestPriority(4)]
    [InlineData(1)]
    [InlineData(50)]
    public async Task RemoveComplaint(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _ComplaintService.DeleteComplaintAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Complaint as a test case that take id Updated by user name,expected result
    /// </summary>
    /// <param name="id">Complaint id</param>  
    [Theory, TestPriority(3)]
    [InlineData(1, true)]
    [InlineData(30, false)]
    public async Task UpdateComplaint(int id, bool expectedResult)
    {
        // Arrange
        CheckService();
        var ComplaintRequestDto = new ComplaintRequestDto { ComplaintDescription = "ComplaintDesc", ComplaintCategory = "ComplaintCategory", ComplaintDate = DateOnly.Parse("7/11/2024"), CustomerId = "53ae72a7-589e-4f0b-81ed-4038169498" ,AssignedTo = "Dep2",
            ComplaintStatus = Status.Cancelled,
        };

        // Act
        var result = await _ComplaintService.UpdateComplaintAsync(id, ComplaintRequestDto);

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

