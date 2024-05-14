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
    /// fuction to add branch as a test case that take   branch name , branch phone number , Center avaliability , center id  ,email address
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("branch1", 2, "012656268","kjsaih@pojs.com")]
    public async Task AddCenter(string branchName, int centerId, string branchPhoneNumber,string emailAddress)
    {
        // Arrange
        CheckService();
        var branchRequestDto = new BranchRequestDto { BranchName = branchName,CenterId= centerId, BranchPhoneNumber= branchPhoneNumber, EmailAddress= emailAddress };
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
        var result = await _branchService.GetAllBranchesAsync();

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
    public async Task GetByIdCenter(int id)
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
    /// fuction to update branch as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1,"branch1", 2, "012656268", "kjsaih@pojs.com",true)]
    [InlineData(10,"branch1", 2, "012656268", "kjsaih@pojs.com",false)]
    public async Task UpdateCenter(int id, string branchName, int centerId, string branchPhoneNumber, string emailAddress,bool expectedResult)
    {
        //Arrange
        CheckService();
        var branchRequestDto = new BranchRequestDto { BranchName = branchName, CenterId = centerId, BranchPhoneNumber = branchPhoneNumber, EmailAddress = emailAddress };

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
    [Theory, TestPriority(4)]
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
}
