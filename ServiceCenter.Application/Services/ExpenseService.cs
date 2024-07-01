using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Application.Utils;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ExpenseService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ExpenseService> logger, IUserContextService userContext) : IExpenseService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ExpenseService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<ExpenseResponseDto>>> GetAllExpensesAsync(int pageSize, int index)
    {
        var expensesResponseDto = await _dbContext.Expenses
            .ProjectTo<ExpenseResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(pageSize, index);
        if (index > expensesResponseDto.TotalCount)
        {
            expensesResponseDto.End = expensesResponseDto.TotalCount;
        }
        _logger.LogInformation("Fetching all expenses. Total count: {Expense}.", expensesResponseDto.Data.Count);

        return Result.Success(expensesResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<ExpenseResponseDto>>> GetAllExpensesByTransactionTypeAsync(TransactionType transcationType, int pageSize, int index)
    {
        var expensesResponseDto = await _dbContext.Expenses.Where(e => e.TransactionType == transcationType)
            .ProjectTo<ExpenseResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(pageSize, index);
        if (index > expensesResponseDto.TotalCount)
        {
            expensesResponseDto.End = expensesResponseDto.TotalCount;
        }
        _logger.LogInformation("Fetching all expenses based on the transaction type. Total count: {Expense}.", expensesResponseDto.Data.Count);

        return Result.Success(expensesResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<ExpenseResponseDto>>> SearchExpensesByDateAsync(DateOnly date, int pageSize, int index)
    {
        DateTime startDate = date.ToDateTime(new TimeOnly(0, 0));
        DateTime endDate = date.ToDateTime(new TimeOnly(23, 59, 59));

        var expensesResponseDto = await _dbContext.Expenses
            .ProjectTo<ExpenseResponseDto>(_mapper.ConfigurationProvider)
            .Where(d => d.CreatedDate >= startDate && d.CreatedDate <= endDate)
            .GetAllWithPagination(pageSize, index);
        if (index > expensesResponseDto.TotalCount)
        {
            expensesResponseDto.End = expensesResponseDto.TotalCount;
        }
        _logger.LogInformation("Searching expenses. Total count: {Expense}.", expensesResponseDto.Data.Count);

        return Result.Success(expensesResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<ExpenseResponseDto>> AddExpenseAsync(ExpenseRequestDto expenseRequestDto)
    {
        var expense = _mapper.Map<Expense>(expenseRequestDto);

        expense.CreatedBy = _userContext.Email;

        _dbContext.Expenses.Add(expense);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Expense added successfully to the database");
        return Result.Success(_mapper.Map<ExpenseResponseDto>(expense));
    }

    /// <inheritdoc/>
    public async Task<Result<ExpenseResponseDto>> UpdateExpenseAsync(int id, ExpenseRequestDto expenseRequestDto)
    {
        var expense = await _dbContext.Expenses.FindAsync(id);

        if (expense is null)
        {
            _logger.LogWarning("Expense Id not found,Id {id}", id);
            return Result.NotFound(["The Expense is not found"]);
        }

        expense.ModifiedBy = _userContext.Email;
        _mapper.Map(expenseRequestDto, expense);

        await _dbContext.SaveChangesAsync();

        var updatedExpense = _mapper.Map<ExpenseResponseDto>(expense);

        _logger.LogInformation("Expense updated successfully");
        return Result.Success(updatedExpense, "Expense updated successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteExpenseAsync(int id)
    {
        var expense = await _dbContext.Expenses.FindAsync(id);

        if (expense is null)
        {
            _logger.LogWarning($"Expense with id {id} was not found while attempting to delete");
            return Result.NotFound(["The Expense is not found"]);
        }

        _dbContext.Expenses.Remove(expense);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully removed expense {expense}");
        return Result.SuccessWithMessage("Expense removed successfully");
    }

    /// <inheritdoc/>
    public async Task<Result<decimal>> TotalExpensesAsync(DateOnly startDate, DateOnly endDate)
    {
        (DateTime startDateTime, DateTime endDateTime) dateRange = ConvertDateOnly.ToDateTime(startDate, endDate);

        var sumExpense = await _dbContext.Expenses
            .Where(r => r.CreatedDate >= dateRange.startDateTime && r.CreatedDate <= dateRange.endDateTime)
            .SumAsync(r => r.Value);

        _logger.LogInformation("Calculating sum of expenses. Sum: {Expense}.", sumExpense);

        return Result<decimal>.Success(sumExpense);
    }
}
