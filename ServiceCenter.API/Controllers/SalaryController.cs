using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class SalaryController(ISalaryService salaryService) : BaseController
{
    private readonly ISalaryService _salaryService = salaryService;

    /// <summary>
    /// retrieves all salary in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of salary to retrieve</param>
    ///<param name="index">index of salary to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all salary.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<SalaryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<SalaryResponseDto>>> GetAllSalaries(int itemCount, int index)
    {
        return await _salaryService.GetAllSalariesAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves a salary  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the salary .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the salary category details.</returns>[HttpGet("{id}")]

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
    /// retrieves salaries by their employee unique identifier.
    /// </summary>
    ///<param name="employeeId">the unique identifier of the employee</param>  
    /// <param name = "itemCount" > item count of salary to retrieve</param>
    ///<param name="index">index of salary to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the employee's salaries.</returns>

    [HttpGet("searchByEmployee/{employeeId}")]
    [Authorize(Roles = "Admin, Employee")]
    [ProducesResponseType(typeof(Result<SalaryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<SalaryResponseDto>>> GetSalaryByEmployeeId(string employeeId,int itemCount,int index)
    {
        return await _salaryService.GetSalaryByEmployeeIdAsync(employeeId,itemCount,index);
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