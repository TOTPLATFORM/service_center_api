using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;

public class RevenueController(IRevenueService revenueService) : BaseController
{
    private readonly IRevenueService _revenueService = revenueService;

    /// <summary>
    /// retrieves all revenues in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all revenues.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<RevenueResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RevenueResponseDto>>> GetAllRevenues(int itemCount, int index)
    {
        return await _revenueService.GetAllRevenuesAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves all revenues based on the transaction type.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of revenues.</returns>
    [HttpGet("TransactionType/{transactionType}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<RevenueResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RevenueResponseDto>>> GetAllRevenuesByTransactionType(TransactionType transactionType, int itemCount, int index)
    {
        return await _revenueService.GetAllRevenuesByTransactionTypeAsync(transactionType, itemCount, index);
    }

    /// <summary>
    /// searches revenues based on a query text.
    /// </summary>
    /// <param name="date">the search query date.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of revenues that match the search criteria.</returns>
    [HttpGet("Search/{date}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<RevenueResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RevenueResponseDto>>> SearchRevenue(DateOnly date, int itemCount, int index)
    {
        return await _revenueService.SearchRevenuesByTextAsync(date, itemCount, index);
    }

    /// <summary>
    /// adds a new revenue to the system.
    /// </summary>
    /// <param name="revenueRequestDto">the data transfer object containing revenue details for creation.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RevenueResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<RevenueResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<Result<RevenueResponseDto>> AddRevenue(RevenueRequestDto revenueRequestDto)
    {
        return await _revenueService.AddRevenueAsync(revenueRequestDto);
    }

    /// <summary>
    /// updates an existing revenue's information.
    /// </summary>
    /// <param name="id">the unique identifier of the revenue to update.</param>
    /// <param name="revenueRequestDto">the data transfer object containing updated details for the revenue.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RevenueResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<RevenueResponseDto>> UpdateRevenueAsync(int id, RevenueRequestDto revenueRequestDto)
    {
        return await _revenueService.UpdateRevenueAsync(id, revenueRequestDto);
    }

    /// <summary>
    /// deletes a revenue from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the revenue to delete.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteRevenue(int id)
    {
        return await _revenueService.DeleteRevenueAsync(id);
    }

    /// <summary>
    /// retrieves the total revenues within the specified date range.
    /// </summary>
    /// <param name="startDate">the start date of the period from which to begin calculating revenues.</param>
    /// <param name="endDate">the end date of the period until which to calculate revenues.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the total revenues as a decimal for the specified period.</returns>
    [HttpGet("startDate/{startDate}/endDate/{endDate}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<decimal>), StatusCodes.Status200OK)]
    public async Task<Result<decimal>> GetTotalRevenues(DateOnly startDate, DateOnly endDate)
    {
        return await _revenueService.TotalRevenuesAsync(startDate, endDate);
    }
}
