using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ProfitController(IProfitService profitService) : BaseController
{
    private readonly IProfitService _profitService = profitService;

    /// <summary>
    /// calculates the profit by subtracting total expenses from total revenues within the specified date range.
    /// </summary>
    /// <param name="startDate">the start date of the period from which to begin aggregating revenues and expenses.</param>
    /// <param name="endDate">the end date of the period until which to aggregate revenues and expenses.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the net profit as a decimal for the specified period.</returns>
    [HttpGet("startDate/{startDate}/endDate/{endDate}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<decimal>), StatusCodes.Status200OK)]
    public async Task<Result<decimal>> CalculateProfit(DateOnly startDate, DateOnly endDate)
    {
        return await _profitService.CalculateProfitAsync(startDate, endDate);
    }
}
