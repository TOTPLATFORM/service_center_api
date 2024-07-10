using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


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
    public async Task<Result<PaginationResult<RecruitmentRecordResponseDto>>> GetAllRecruitmentRecords(int itemCount, int index)
    {
        return await _recruitmentRecordService.GetAllRecruitmentRecordsAsync(itemCount, index);
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
    /// <param name="recruitmentRecordDto">The DTO containing the recruitment record details.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
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
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
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