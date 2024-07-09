using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class SalaryController(ISalaryService salaryService) : BaseController
{
    private readonly ISalaryService _salaryService = salaryService;

    /// <summary>
    /// Retrieves all salaries.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result containing a list of salary response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<SalaryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<SalaryResponseDto>>> GetAllSalaries()
    {
        return await _salaryService.GetAllSalariesAsync();
    }

    /// <summary>
    /// Retrieves a salary by its ID.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">The ID of the salary to retrieve.</param>
    /// <returns>A result containing the salary response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<SalaryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SalaryResponseDto>> GetSalaryById(int id)
    {
        return await _salaryService.GetSalaryByIdAsync(id);
    }

    /// <summary>
    /// Adds a new salary.
    /// </summary>
    /// <param name="salaryDto">The DTO containing the salary details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSalary(SalaryRequestDto salaryDto)
    {
        return await _salaryService.AddSalaryAsync(salaryDto);
    }

    /// <summary>
    /// Updates an existing salary by its ID.
    /// </summary>
    /// <param name="id">The ID of the salary to update.</param>
    /// <param name="salaryDto">The DTO containing the updated salary details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<SalaryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SalaryResponseDto>> UpdateSalary(int id, SalaryRequestDto salaryDto)
    {
        return await _salaryService.UpdateSalaryAsync(id, salaryDto);
    }

    /// <summary>
    /// Retrieves salaries by employee ID.
    /// </summary>
    /// <param name="employeeId">The employee ID to filter salaries.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Employee" role.
    /// </remarks>
    /// <returns>A result containing the salary response DTO.</returns>
    [HttpGet("searchByEmployee/{employeeId}")]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(Result<SalaryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<SalaryResponseDto>>> GetSalaryByEmployeeId(string employeeId)
    {
        return await _salaryService.GetSalaryByEmployeeIdAsync(employeeId);
    }

    /// <summary>
    /// Adds a bonus or deduction to a salary by its ID.
    /// </summary>
    /// <param name="id">The ID of the salary to update.</param>
    /// <param name="salaryDto">The DTO containing the bonus or deduction details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result containing the updated salary response DTO.</returns>
    [HttpPut("BonusDeduction/{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<SalaryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SalaryResponseDto>> AddBonusOrDeduction(int id, SalaryUpdateDto salaryDto)
    {
        return await _salaryService.AddBonusOrDeductionAsync(id, salaryDto);
    }

    /// <summary>
    /// Deletes a salary by its ID.
    /// </summary>
    /// <param name="id">The ID of the salary to delete.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A result indicating the success of the deletion.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteSalary(int id)
    {
        return await _salaryService.DeleteSalaryAsync(id);
    }
}