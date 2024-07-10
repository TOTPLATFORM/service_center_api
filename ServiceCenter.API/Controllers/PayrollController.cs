using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class PayrollController(IPayrollService payrollService) : BaseController
{
    private readonly IPayrollService _payrollService = payrollService;

    /// <summary>
    /// Retrieves the payroll details for a specific employee asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee for whom to retrieve payroll details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result containing the payroll response DTO for the specified employee.</returns>
    [HttpGet("{employeeId}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PayrollResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PayrollResponseDto>> GetPayroll(string employeeId)
    {
        return await _payrollService.PayrollCalculate(employeeId);
    }

    /// <summary>
    /// Calculates the salary for a specific employee within a given period asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee for whom to calculate the salary.</param>
    /// <param name="startDate">The start date of the period.</param>
    /// <param name="endDate">The end date of the period.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result containing the payroll response DTO for the specified period.</returns>
    [HttpGet("calculate")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PayrollResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PayrollResponseDto>> CalculateSalaryInSpecificPeriod(string employeeId, DateTime startDate, DateTime endDate)
    {
        return await _payrollService.CalculateSalaryInSpecificPeriod(employeeId, startDate, endDate);
    }

    /// <summary>
    /// Calculates the salary for a specific employee within a given period asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee for whom to calculate the salary.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result containing the payroll response DTO for the specified period.</returns>
    [HttpGet("month")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PayrollResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PayrollResponseDto>> CalculatePayrollForSpecificMonth(string employeeId, int year, int month)
    {
        return await _payrollService.CalculatePayrollForSpecificMonth(employeeId, year, month);
    }

    /// <summary>
    /// Calculates the salary for a specific employee within a given period asynchronously.
    /// </summary>
    /// <param name="employeeId">The ID of the employee for whom to calculate the salary.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result containing the payroll response DTO for the specified period.</returns>
    [HttpGet("allMonths")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PayrollResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<PayrollResponseDto>>> CalculatePayrollsForAllMonths(string employeeId)
    {
        return await _payrollService.CalculatePayrollsForAllMonths(employeeId);
    }
}