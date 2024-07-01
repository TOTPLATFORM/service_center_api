using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ProfitService(IRevenueService revenueService, IExpenseService expenseService, ILogger<ProfitService> logger) : IProfitService
{
    private readonly IRevenueService _revenueService = revenueService;
    private readonly IExpenseService _expenseService = expenseService;
    private readonly ILogger<ProfitService> _logger = logger;

    /// <inheritdoc/>
    public async Task<Result<decimal>> CalculateProfitAsync(DateOnly startDate, DateOnly endDate)
    {
        var totalRevenues = await _revenueService.TotalRevenuesAsync(startDate, endDate);
        var totalExpenses = await _expenseService.TotalExpensesAsync(startDate, endDate);

        var profit = totalRevenues.Value - totalExpenses.Value;

        _logger.LogInformation("Calculating total profit. Profit: {Profit}.", profit);

        return profit;
    }
}
