 using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Reports;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using Microsoft.AspNetCore.Authorization;
using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;

public class ReportController(IReportService ReportService) : BaseController
{
    private readonly IReportService _ReportService = ReportService;

    /// <summary>
    /// action for add Report action that take Report dto   
    /// </summary>
    /// <param name="ReportDto">Report dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for Report  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Sales")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddReport(ReportRequestDto ReportDto)
    {
        return await _ReportService.AddReportAsync(ReportDto);
    }


    /// <summary>
    /// get all Report in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ReportResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ReportResponseDto>>> GetAllReport(int itemCount, int index)
    {
        return await _ReportService.GetAllReportAsync(itemCount,index);
    }
    /// <summary>
    /// get Report by id in the system.
    /// </summary>
    ///<param name="id">id of Report.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<ReportResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ReportResponseDto>> GetReportById(int id)
    {
        return await _ReportService.GetReportByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Report.</param>
    ///<param name="ReportRequestDto">Report dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Sales")]
    [ProducesResponseType(typeof(Result<ReportResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ReportResponseDto>> UpdateReport(int id, string task)
    {
        return await _ReportService.UpdateReportAsync(id,task );
    }
    /// <summary>
    /// delete  Report  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Sales")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteReportAsycn(int id)
    {
        return await _ReportService.DeleteReportAsync(id);
    }

    [HttpPut("reportId/{id}/status/{status}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<ReportResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ReportResponseDto>> UpdateReportStatus( int id,ReportStatus status )
    {
        return await _ReportService.UpdateReportStatusAsync(id, status);
    }

}
