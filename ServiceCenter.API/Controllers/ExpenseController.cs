using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;

public class ExpenseController(IExpenseService expenseService) : BaseController
{
    private readonly IExpenseService _expenseService = expenseService;

    /// <summary>
    /// retrieves all expenses in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all expenses.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<ExpenseResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ExpenseResponseDto>>> GetAllExpenses(int itemCount, int index)
    {
        return await _expenseService.GetAllExpensesAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves all expenses based on transaction type.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of expenses.</returns>
    [HttpGet("TransactionType/{transactionType}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<ExpenseResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ExpenseResponseDto>>> GetAllExpensesByTransactionType(TransactionType transactionType, int itemCount, int index)
    {
        return await _expenseService.GetAllExpensesByTransactionTypeAsync(transactionType, itemCount, index);
    }

    /// <summary>
    /// searches expenses based on a query text.
    /// </summary>
    /// <param name="date">the search query date.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of expenses that match the search criteria.</returns>
    [HttpGet("Search/{date}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ExpenseResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ExpenseResponseDto>>> SearchExpense(DateOnly date, int itemCount, int index)
    {
        return await _expenseService.SearchExpensesByDateAsync(date, itemCount, index);
    }

    /// <summary>
    /// adds a new expense to the system.
    /// </summary>
    /// <param name="expenseRequestDto">the data transfer object containing expense details for creation.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ExpenseResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<ExpenseResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<Result<ExpenseResponseDto>> AddExpense(ExpenseRequestDto expenseRequestDto)
    {
        return await _expenseService.AddExpenseAsync(expenseRequestDto);
    }

    /// <summary>
    /// updates an existing expense's information.
    /// </summary>
    /// <param name="id">the unique identifier of the expense to update.</param>
    /// <param name="expenseRequestDto">the data transfer object containing updated details for the expense.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ExpenseResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ExpenseResponseDto>> UpdateExpense(int id, ExpenseRequestDto expenseRequestDto)
    {
        return await _expenseService.UpdateExpenseAsync(id, expenseRequestDto);
    }

    /// <summary>
    /// deletes a expense from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the expense to delete.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteExpense(int id)
    {
        return await _expenseService.DeleteExpenseAsync(id);
    }

    /// <summary>
    /// retrieves the total expenses within the specified date range.
    /// </summary>
    /// <param name="startDate">the start date of the period from which to begin calculating expenses.</param>
    /// <param name="endDate">the end date of the period until which to calculate expenses.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the total expenses as a decimal for the specified period.</returns>
    [HttpGet("startDate/{startDate}/endDate/{endDate}")]
    [ProducesResponseType(typeof(Result<decimal>), StatusCodes.Status200OK)]
    public async Task<Result<decimal>> GetTotalExpenses(DateOnly startDate, DateOnly endDate)
    {
        return await _expenseService.TotalExpensesAsync(startDate, endDate);
    }
}