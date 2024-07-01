using ServiceCenter.Application.Contracts;
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
/// provides an interface for revenue-related services that manages revenue data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IRevenueService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously retrieves all revenues in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of revenue response DTOs.</returns>
    public Task<Result<PaginationResult<RevenueResponseDto>>> GetAllRevenuesAsync(int pageSize, int index);

    /// <summary>
    /// asynchronously retrieves all revenues based on the provided transaction type.
    /// </summary>
    /// <param name="transactionType">the transaction type o get its revenues data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of revenues response DTOs that match the transaction type.</returns>
    public Task<Result<PaginationResult<RevenueResponseDto>>> GetAllRevenuesByTransactionTypeAsync(TransactionType transactionType, int pageSize, int index);

    /// <summary>
    /// asynchronously searches for revenues based on the provided date.
    /// </summary>
    /// <param name="date">the date to search within revenue data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of revenue response DTOs that match the search criteria.</returns>
    public Task<Result<PaginationResult<RevenueResponseDto>>> SearchRevenuesByTextAsync(DateOnly date, int pageSize, int index);

    /// <summary>
    /// asynchronously adds a new revenue to the database.
    /// </summary>
    /// <param name="revenueRequestDto">the revenue data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the revenue addition.</returns>
    public Task<Result<RevenueResponseDto>> AddRevenueAsync(RevenueRequestDto revenueRequestDto);

    /// <summary>
    /// asynchronously updates the data of an existing revenue.
    /// </summary>
    /// <param name="id">the unique identifier of the revenue to update.</param>
    /// <param name="revenueRequestDto">the revenue data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<RevenueResponseDto>> UpdateRevenueAsync(int id, RevenueRequestDto revenueRequestDto);

    /// <summary>
    /// asynchronously deletes a revenue from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the revenue to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteRevenueAsync(int id);

    /// <summary>
    /// asynchronously calculates the total revenue accrued between two specified dates.
    /// </summary>
    /// <param name="startDate">the start date of the period for which to calculate total revenue.</param>
    /// <param name="endDate">the end date of the period for which to calculate total revenue.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the total revenue as a decimal within a result object.</returns>
    public Task<Result<decimal>> TotalRevenuesAsync(DateOnly startDate, DateOnly endDate);
}