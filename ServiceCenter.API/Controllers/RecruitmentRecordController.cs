using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

/// <summary>
/// Controller responsible for handling recruitment record-related HTTP requests.
/// </summary>
/// <param name="recruitmentRecordService">The service for performing recruitment record-related operations.</param>
/// <seealso cref="BaseController"/>
public class RecruitmentRecordController(IRecruitmentRecordService recruitmentRecordService) : BaseController
{
    private readonly IRecruitmentRecordService _recruitmentRecordService = recruitmentRecordService;

    /// <summary>
    /// Retrieves all recruitment records.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <returns>A result containing a list of recruitment record response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<RecruitmentRecordResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<RecruitmentRecordResponseDto>>> GetAllRecruitmentRecords()
    {
        return await _recruitmentRecordService.GetAllRecruitmentRecordsAsync();
    }

    /// <summary>
    /// Retrieves a recruitment record by its ID.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the recruitment record to retrieve.</param>
    /// <returns>A result containing the recruitment record response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RecruitmentRecordResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<RecruitmentRecordResponseDto>> GetRecruitmentRecordById(int id)
    {
        return await _recruitmentRecordService.GetRecruitmentRecordByIdAsync(id);
    }

    /// <summary>
    /// Adds a new recruitment record.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="recruitmentRecordDto">The DTO containing the recruitment record details.</param>
    /// <returns>A result indicating the success of the operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddRecruitmentRecord(RecruitmentRecordRequestDto recruitmentRecordDto)
    {
        return await _recruitmentRecordService.AddRecruitmentRecordAsync(recruitmentRecordDto);
    }

    /// <summary>
    /// Updates an existing recruitment record by its ID.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the recruitment record to update.</param>
    /// <param name="recruitmentRecordDto">The DTO containing the updated recruitment record details.</param>
    /// <returns>A result containing the updated recruitment record response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RecruitmentRecordResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<RecruitmentRecordResponseDto>> UpdateRecruitmentRecord(int id, RecruitmentRecordRequestDto recruitmentRecordDto)
    {
        return await _recruitmentRecordService.UpdateRecruitmentRecordAsync(id, recruitmentRecordDto);
    }

    /// <summary>
    /// Deletes a recruitment record by its ID.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the recruitment record to delete.</param>
    /// <returns>A result indicating the success of the deletion.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteRecruitmentRecord(int id)
    {
        return await _recruitmentRecordService.DeleteRecruitmentRecordAsync(id);
    }
}