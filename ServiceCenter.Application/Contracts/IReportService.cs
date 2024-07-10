using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Reports;

/// <summary>
/// provides an interface for report-related services that manages report data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IReportService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new report to the database.
	/// </summary>
	/// <param name="reportRequestDto">the report data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the report addition.</returns>
	public Task<Result> AddReportAsync(ReportRequestDto reportRequestDto);

	/// <summary>
	/// asynchronously retrieves all reports in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of report to retrieve</param>
	///<param name="index">index of report to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of report response DTOs.</returns>
	public Task<Result<PaginationResult<ReportResponseDto>>> GetAllReportAsync(int itemCount,int index);

	/// <summary>
	/// asynchronously retrieves a report by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the report to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the report response DTO.</returns>
	public Task<Result<ReportResponseDto>> GetReportByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing report.
	/// </summary>
	/// <param name="id">the unique identifier of the report to update.</param>
	/// <param name="reportRequestDto">the report data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ReportResponseDto>> UpdateReportAsync(int id, string task);

	/// <summary>
	/// asynchronously deletes a report from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the report to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteReportAsync(int id);

	/// <summary>
	/// asynchronously updates the status of order of an existing order.
	/// </summary>
	/// <param name="id">the unique identifier of the report to update.</param>
	/// <param name="status">the status of report containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ReportResponseDto>> UpdateReportStatusAsync(int id, ReportStatus status);
}
