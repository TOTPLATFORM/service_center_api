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

public class RevenueService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<RevenueService> logger, IUserContextService userContext) : IRevenueService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<RevenueService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<RevenueResponseDto>>> GetAllRevenuesAsync(int pageSize, int index)
    {
        var revenuesResponseDto = await _dbContext.Revenues
            .ProjectTo<RevenueResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(pageSize, index);
        if (index > revenuesResponseDto.TotalCount)
        {
            revenuesResponseDto.End = revenuesResponseDto.TotalCount;
        }
        _logger.LogInformation("Fetching all revenues. Total count: {Revenue}.", revenuesResponseDto.Data.Count);

        return Result.Success(revenuesResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<RevenueResponseDto>>> GetAllRevenuesByTransactionTypeAsync(TransactionType transcationType, int pageSize, int index)
    {
        var revenuesResponseDto = await _dbContext.Expenses.Where(e => e.TransactionType == transcationType)
            .ProjectTo<RevenueResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(pageSize, index);
        if (index > revenuesResponseDto.TotalCount)
        {
            revenuesResponseDto.End = revenuesResponseDto.TotalCount;
        }
        _logger.LogInformation("Fetching all revenues based on transaction type. Total count: {Expense}.", revenuesResponseDto.Data.Count);

        return Result.Success(revenuesResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<RevenueResponseDto>>> SearchRevenuesByTextAsync(DateOnly date, int pageSize, int index)
    {
        DateTime startDate = date.ToDateTime(new TimeOnly(0, 0));
        DateTime endDate = date.ToDateTime(new TimeOnly(23, 59, 59));

        var revenuesResponseDto = await _dbContext.Revenues
            .ProjectTo<RevenueResponseDto>(_mapper.ConfigurationProvider)
            .Where(d => d.CreatedDate >= startDate && d.CreatedDate <= endDate)
            .GetAllWithPagination(pageSize, index);
        if (index > revenuesResponseDto.TotalCount)
        {
            revenuesResponseDto.End = revenuesResponseDto.TotalCount;
        }
        _logger.LogInformation("Searching revenues. Total count: {Revenue}.", revenuesResponseDto.Data.Count);

        return Result.Success(revenuesResponseDto);
    }

    /// <inheritdoc/>
    public async Task<Result<RevenueResponseDto>> AddRevenueAsync(RevenueRequestDto revenueRequestDto)
    {
        var revenue = _mapper.Map<Revenue>(revenueRequestDto);

        revenue.CreatedBy = _userContext.Email;

        _dbContext.Revenues.Add(revenue);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Revenue added successfully to the database");
        return Result.Success(_mapper.Map<RevenueResponseDto>(revenue));
    }

    /// <inheritdoc/>
    public async Task<Result<RevenueResponseDto>> UpdateRevenueAsync(int id, RevenueRequestDto revenueRequestDto)
    {
        var revenue = await _dbContext.Revenues.FindAsync(id);

        if (revenue is null)
        {
            _logger.LogWarning("Revenue Id not found,Id {id}", id);
            return Result.NotFound(["The Revenue is not found"]);
        }

        revenue.ModifiedBy = _userContext.Email;
        _mapper.Map(revenueRequestDto, revenue);

        await _dbContext.SaveChangesAsync();

        var updatedRevenue = _mapper.Map<RevenueResponseDto>(revenue);

        _logger.LogInformation("Revenue updated successfully");
        return Result.Success(updatedRevenue, "Revenue updated successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteRevenueAsync(int id)
    {
        var revenue = await _dbContext.Revenues.FindAsync(id);

        if (revenue is null)
        {
            _logger.LogWarning($"Revenue with id {id} was not found while attempting to delete");
            return Result.NotFound(["The Revenue is not found"]);
        }

        _dbContext.Revenues.Remove(revenue);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Successfully removed revenue {revenue}");
        return Result.SuccessWithMessage("Revenue removed successfully");
    }

    /// <inheritdoc/>
    public async Task<Result<decimal>> TotalRevenuesAsync(DateOnly startDate, DateOnly endDate)
    {
        (DateTime startDateTime, DateTime endDateTime) dateRange = ConvertDateOnly.ToDateTime(startDate, endDate);

        var sumRevenue = await _dbContext.Revenues
            .Where(r => r.CreatedDate >= dateRange.startDateTime && r.CreatedDate <= dateRange.endDateTime)
            .SumAsync(r => r.Value);

        _logger.LogInformation("Calculating sum of revenues. Sum: {Revenue}.", sumRevenue);

        return Result<decimal>.Success(sumRevenue);
    }
}