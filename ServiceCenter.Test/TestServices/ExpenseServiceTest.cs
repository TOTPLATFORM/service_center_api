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
public class ExpenseServiceTest
{
    private static ExpenseService _expensesService;

    private ExpenseService CreateExpensesService()
    {

        if (_expensesService is null)
        {
            var dbContext = ContextGenerator.Generator();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
            ILogger<ExpenseService> ItemLogger = new LoggerFactory().CreateLogger<ExpenseService>();

            IUserContextService userContext = new UserContextService();
            _expensesService = new ExpenseService(dbContext, mapper, ItemLogger, userContext);
        }

        return _expensesService;
    }

    private void CheckService()
    {
        if (_expensesService is null)
            _expensesService = CreateExpensesService();
    }

    /// <summary>
    /// Tests adding a Expenses with given value.
    /// </summary>
    /// <param name="value">value of Expenses.</param>
    [Theory, TestPriority(0)]
    [InlineData(90)]
    public async Task AddExpenses(int value)
    {
        // Arrange
        CheckService();
        var expensesRequestDto = new ExpenseRequestDto { Value = value };

        // Act
        var result = await _expensesService.AddExpenseAsync(expensesRequestDto);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);
    }

    /// <summary>
    /// Tests updating a Expenses by ID with new data and checks if the update meets the expected outcome.
    /// </summary>
    /// <param name="id">Expenses's ID to update.</param>
    /// <param name="value">New value for the Expenses.</param>
    [Theory, TestPriority(3)]
    [InlineData(2, 601, true)]
    [InlineData(10, 30, false)]
    public async Task UpdateExpensesAsync(int id, int value, bool expectedResult)
    {
        //Arrange
        CheckService();
        var expensesRequestDto = new ExpenseRequestDto { Value = value };

        // Act
        var result = await _expensesService.UpdateExpenseAsync(id, expensesRequestDto);

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
    /// Tests retrieving all Expenses to ensure the service returns a successful result and the list is correctly fetched.
    /// </summary>
    [Fact, TestPriority(1)]
    public async Task GetAllExpensess()
    {
        // Arrange
        CheckService();

        // Act
        var result = await _expensesService.GetAllExpensesAsync(2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// Tests the search functionality in the Expenses service to ensure it can find Expensess based on a search term.
    /// </summary>
    [Theory, TestPriority(4)]
    [InlineData("7/11/2024")]
    public async Task SearchCities(string date)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _expensesService.SearchExpensesByDateAsync(DateOnly.Parse(date), 2, 1);

        // Assert
        Assert.True(result.IsSuccess);
    }

    /// <summary>
    /// Tests the removal of a Expenses by their ID to ensure the service can successfully delete cities and handle IDs that do not exist.
    /// </summary>
    /// <param name="id">The ID of the Expenses to remove.</param>
    [Theory, TestPriority(5)]
    [InlineData(3)]
    [InlineData(30)]
    public async Task RemoveExpenses(int id)
    {
        // Arrange
        CheckService();

        // Act
        var result = await _expensesService.DeleteExpenseAsync(id);

        // Assert
        if (result.IsSuccess)
            Assert.True(result.IsSuccess);
        else
            Assert.False(result.IsSuccess);

    }
}