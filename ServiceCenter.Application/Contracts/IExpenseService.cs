using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for expense-related services that manages exoense data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IExpenseService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously retrieves all expenses in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of expense response DTOs.</returns>
    public Task<Result<PaginationResult<ExpenseResponseDto>>> GetAllExpensesAsync(int pageSize, int index);

    /// <summary>
    /// asynchronously retrieves all expenses based on the provided transaction type.
    /// </summary>
    /// <param name="transactionType">the transaction type o get its expenses data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of expense response DTOs that match the transaction type.</returns>
    public Task<Result<PaginationResult<ExpenseResponseDto>>> GetAllExpensesByTransactionTypeAsync(TransactionType transactionType, int pageSize, int index);

    /// <summary>
    /// asynchronously searches for expenses based on the provided date.
    /// </summary>
    /// <param name="date">the date to search within expense data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of expense response DTOs that match the search criteria.</returns>
    public Task<Result<PaginationResult<ExpenseResponseDto>>> SearchExpensesByDateAsync(DateOnly date, int pageSize, int index);

    /// <summary>
    /// asynchronously adds a new expense to the database.
    /// </summary>
    /// <param name="expenseRequestDto">the expense data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the expense addition.</returns>
    public Task<Result<ExpenseResponseDto>> AddExpenseAsync(ExpenseRequestDto expenseRequestDto);

    /// <summary>
    /// asynchronously updates the data of an existing expense.
    /// </summary>
    /// <param name="id">the unique identifier of the expense to update.</param>
    /// <param name="expenseRequestDto">the expense data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<ExpenseResponseDto>> UpdateExpenseAsync(int id, ExpenseRequestDto expenseRequestDto);

    /// <summary>
    /// asynchronously deletes a expense from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the expense to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteExpenseAsync(int id);

    /// <summary>
    /// asynchronously calculates the total expenses incurred between two specified dates.
    /// </summary>
    /// <param name="startDate">the start date of the period for which to calculate total expenses.</param>
    /// <param name="endDate">the end date of the period for which to calculate total expenses.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the total expenses as a decimal within a result object.</returns>
    public Task<Result<decimal>> TotalExpensesAsync(DateOnly startDate, DateOnly endDate);
}