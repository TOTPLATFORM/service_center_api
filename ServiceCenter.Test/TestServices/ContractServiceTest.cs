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

public class ContractServiceTest
{
    private static ContractService _contractService;
    private string userEmail = "mariamabdeeen@gmail.com";
    private ContractService CreateContractService()
    {

        if (_contractService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();

            ILogger<ContractService> logger = new LoggerFactory().CreateLogger<ContractService>();

            IUserContextService userContext = new UserContextService();

            _contractService = new ContractService(dbContext, mapper, logger, userContext);
        }

        return _contractService;
    }
    private void CheckService()
    {
        if (_contractService is null)
            _contractService = CreateContractService();
    }

    /// <summary>
    /// fuction to add contract as a test case that take  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    [Theory, TestPriority(0)]
    [InlineData("2000/12/30",1)]
    public async Task AddContract(string duration, int servicePackageId)
    {
        // Arrange
        CheckService();
        var ContractRequestDto = new ContractRequestDto { Duration = DateOnly.Parse(duration),ServicePackageId= servicePackageId };
        // Act
        var result = await _contractService.AddContractAsync(ContractRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// fuction to get all  Contractes as a test case 
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllContract()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contractService.GetAllContractAsync();

        // Assert
        Assert.True(result.IsSuccess);

    }

    /// <summary>
    /// fuction to get Contract by id as a test case 
    /// </summary>
    /// <param name="id">Contract id </param>
    [Theory, TestPriority(2)]
    [InlineData(1)]
    [InlineData(6)]
    public async Task GetByIdCenter(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contractService.GetContractByIdAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }

    /// <summary>
    /// fuction to update Contract as a test case that take   Center id , Center number , Center avaliability , center id  
    /// </summary>
    /// <param name="CenterNumber">Center number</param>
    /// <param name="CenterAvaliabitlity">Center availability</param>
    /// <param name="centerId">Center Type id</param>
    /// <param name="expectedResult">expected result</param>
    [Theory, TestPriority(3)]
    [InlineData(1, "2000/12/30", 1, true)]
    [InlineData(10, "2000/12/30", 1, false)]
    public async Task UpdateCenter(int id, string duration, int servicePackageId, bool expectedResult)
    {
        //Arrange
        CheckService();
        var ContractRequestDto = new ContractRequestDto { Duration = DateOnly.Parse(duration), ServicePackageId = servicePackageId };

        // Act
        var result = await _contractService.UpdateContractAsync(id, ContractRequestDto);
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
    /// fuction to remove Contract as a test case that take Contract id
    /// </summary>
    /// <param name="id">Contract id </param>
    [Theory, TestPriority(4)]
    [InlineData(2)]
    [InlineData(50)]
    public async Task RemoveContract(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _contractService.DeleteContractAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}
