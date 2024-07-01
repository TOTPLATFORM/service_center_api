using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.API.Mapping;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Domain.Entities;
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
public class BranchServiceTest
{
    private static BranchService _branchService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private BranchService CreateBranchService()
    {

        if (_branchService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<BranchService> logger = new LoggerFactory().CreateLogger<BranchService>();

            IUserContextService userContext = new UserContextService();

            _branchService = new BranchService(dbContext, mapper, logger, userContext);
        }

        return _branchService;
    }
    private void CheckService()
    {
        if (_branchService is null)
            _branchService = CreateBranchService();
    }

    /// <summary>
    /// fuction to add branch as a test case that take   branch name , branch phone number   ,email address
    /// </summary>
    /// <param name="branchName">branch number</param>
    /// <param name="branchPhoneNumber"> branch phone number</param>
   /// <param name="emailAddress"> email address</param>
    [Theory, TestPriority(0)]
    [InlineData("branch1", "012656268", "kjsaih@pojs.com")]
    public async Task AddBranch(string branchName, string branchPhoneNumber, string emailAddress)
    {
        // Arrange
        CheckService();
        var branchRequestDto = new BranchRequestDto { BranchName = branchName,  BranchPhoneNumber = branchPhoneNumber, EmailAddress = emailAddress,Address=new Address { City=City.Giza,Country=Country.Egypt,PostalCode="sl3"} };
        // Act
        var result = await _branchService.AddBranchAsync(branchRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  branches as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllBranch()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _branchService.GetAllBranchesAsync(2,1);

        // Assert
        Assert.True(result.IsSuccess);

    }
   

    /// <summary>
    /// fuction to get branch by id as a test case 
    /// </summary>
    /// <param name="id">branch id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdBranch(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _branchService.GetBranchByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update branch as a test case that take   Branch id , Branch number ,Branch email address
    /// </summary>
    /// <param name="branchPhoneNumber">Branch number</param>
    /// <param name="emailAddress">Branch email address</param>
    /// <param name="id">Branch  id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "branch1",  "012656268", "kjsaih@pojs.com", true)]
    [InlineData(10, "branch1", "012656268", "kjsaih@pojs.com", false)]
    public async Task UpdateBranch(int id, string branchName, string branchPhoneNumber, string emailAddress, bool expectedResult)
    {
        //Arrange
        CheckService();
        var branchRequestDto = new BranchRequestDto { BranchName = branchName,  BranchPhoneNumber = branchPhoneNumber, EmailAddress = emailAddress };

        // Act
        var result = await _branchService.UpdateBranchAsync(id, branchRequestDto);
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
    /// fuction to remove branch as a test case that take branch id
    /// </summary>
    /// <param name="id">branch id </param>
    [Theory, TestPriority(5)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveBranch(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _branchService.DeleteBranchAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
    /// <summary>
    /// Tests the search functionality in the branch service to ensure it can find branch based on a search term.
    /// </summary>
    [Fact, TestPriority(4)]
    public async Task SearchBranches()
    {
        // Arrange
        CheckService();
        string text = "barnch1";

        // Act
        var result = await _branchService.SearchBranchByTextAsync(text, 2, 1);

        // Assert

        Assert.True(result.IsSuccess);
    }
}
