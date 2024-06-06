﻿using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
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
    /// function to add Report that take ReportDto   
    /// </summary>
    /// <param name="ReportRequestDto">Report request dto</param>
    /// <returns> Report  added successfully </returns>
    public Task<Result> AddReportAsync(ReportRequestDto ReportRequestDto);

    /// <summary>
    /// function to get all Report  
    /// </summary>
    /// <returns>list all Report  response dto </returns>
    public Task<Result<List<ReportResponseDto>>> GetAllReportAsync();
    /// <summary>
    /// function to get  Report  by id that take   Report id
    /// </summary>
    /// <param name="id"> Report  id</param>
    /// <returns> Report  response dto</returns>
    public Task<Result<ReportResponseDto>> GetReportByIdAsync(int id);

    /// <summary>
    /// function to update Report  that take ReportRequestDto   
    /// </summary>
    /// <param name="id">Report id</param>
    /// <param name="ReportRequestDto">Report dto</param>
    /// <returns>Updated Report </returns>
    public Task<Result<ReportResponseDto>> UpdateReportAsync(int id, ReportRequestDto ReportRequestDto);
    /// <summary>
    /// function to delete Report  that take Report  id   
    /// </summary>
    /// <param name="id">Report  id</param>
    /// <returns>Report  removed successfully </returns>
    public Task<Result> DeleteReportAsync(int id);
}
