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
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Sales")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddReport(ReportRequestDto ReportDto)
    {
        return await _ReportService.AddReportAsync(ReportDto);
    }  /// <summary>
       /// retrieves all report in the system.
       /// </summary>
       /// <param name = "itemCount" > item count of report to retrieve</param>
       ///<param name="index">index of report to retrieve</param>
       /// <remarks>
       /// access is limited to users with the "Admin,MAnager" role.
       /// </remarks>
       /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all report.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ReportResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ReportResponseDto>>> GetAllReport(int itemCount, int index)
    {
        return await _ReportService.GetAllReportAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a report  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the report .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the report category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<ReportResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ReportResponseDto>> GetReportById(int id)
    {
        return await _ReportService.GetReportByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing  report  by its ID.
    /// </summary>
    ///<param name="id">id of Report.</param>
    ///<param name="task">task of Report .</param>
    /// <remarks>
    /// Access is limited to users with the "Sales" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Sales")]
    [ProducesResponseType(typeof(Result<ReportResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ReportResponseDto>> UpdateReport(int id, string task)
    {
        return await _ReportService.UpdateReportAsync(id,task );
    }

    /// <summary>
    /// deletes a report from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Manager,Sales" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the report to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Sales")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteReportAsycn(int id)
    {
        return await _ReportService.DeleteReportAsync(id);
    }
    /// <summary>
    /// Updates an existing  report status  by its ID.
    /// </summary>
    ///<param name="id">id of Report.</param>
    ///<param name="status"> Report status .</param>
    /// <remarks>
    /// Access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("reportId/{id}/status/{status}")]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<ReportResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ReportResponseDto>> UpdateReportStatus( int id,ReportStatus status )
    {
        return await _ReportService.UpdateReportStatusAsync(id, status);
    }

}
