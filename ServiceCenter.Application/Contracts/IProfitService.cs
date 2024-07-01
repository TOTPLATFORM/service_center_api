using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for profit-related services that manages profit data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IProfitService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously calculates profit based on revenues and expenses between two dates.
    /// </summary>
    /// <param name="startDate">the start date of the period for which to calculate profit.</param>
    /// <param name="endDate">the end date of the period for which to calculate profit.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the profit value as a decimal within a result object.</returns>
    public Task<Result<decimal>> CalculateProfitAsync(DateOnly startDate, DateOnly endDate);
}