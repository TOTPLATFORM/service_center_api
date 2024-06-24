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
        var result = await _ComplaintService.GetAllComplaintsAsync(2,1);

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
    [InlineData("Desc1", "0d133c1a-804f-4548-8f7e-8c3f504844u0", 1, Status.Pending)]
    public async Task AddComplaint(string ComplaintDesc,  string customerId,int branchId,  Status status)
    {
        // Arrange
        CheckService();
        var ComplaintRequestDto = new ComplaintRequestDto { ComplaintDescription = ComplaintDesc, ComplaintStatus = status,ContactId=customerId ,BranchId=branchId};

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
    [Theory, TestPriority(8)]
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
    [InlineData(9,Status.Cancelled, true)]
    [InlineData(78,Status.Approved, false)]
    public async Task UpdateComplaint(int id,Status complaintStatus, bool expectedResult)
    {
        // Arrange
        CheckService();
      
     
        // Act
        var result = await _ComplaintService.UpdateComplaintStatusAsync(id, complaintStatus);

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
    /// Tests the search functionality in the complaint service to ensure it can find complaint based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchComplaint()
    {
        // Arrange
        CheckService();
        Status complaintStatus =Status.Approved;

        // Act
        var result = await _ComplaintService.SearchComplaintByStatusAsync(complaintStatus, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the complaint service to ensure it can find complaint based on a search term.
    /// </summary>
    [Fact, TestPriority(5)]
    public async Task GetComplaintsForSpecificCustomer()
    {
        // Arrange
        CheckService();
        string customerId = "0d133c1a-804f-4548-8f7e-8c3f504844u0";

        // Act
        var result = await _ComplaintService.GetComplaintsForSpecificCustomerAsync(customerId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the complaint service to ensure it can find complaint based on a search term.
    /// </summary>
    [Fact, TestPriority(6)]
    public async Task GetComplaintsForSpecificBranch()
    {
        // Arrange
        CheckService();
        int branchId = 1;

        // Act
        var result = await _ComplaintService.GetComplaintsForSpecificBranchAsync(branchId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
    /// <summary>
    /// Tests the search functionality in the complaint service to ensure it can find complaint based on a search term.
    /// </summary>
    [Fact, TestPriority(7)]
    public async Task GetComplaintsForSpecificServiceProvider()
    {
        // Arrange
        CheckService();
        string serviceProviderId = "0d133c1a-804f-4548-8f7e-8c3f504844u0";

        // Act
        var result = await _ComplaintService.GetComplaintsForSpecificServiceProviderAsync(serviceProviderId, 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }
  

}

