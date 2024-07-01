using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// Service interface for handling recruitment record-related operations.
/// </summary>
public interface IRecruitmentRecordService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new recruitmentRecord.
    /// </summary>
    /// <param name="recruitmentRecordRequestDto">The data transfer object containing recruitmentRecord information.</param>
    /// <returns>The result indicating the success of adding the recruitmentRecord.</returns>
    public Task<Result> AddRecruitmentRecordAsync(RecruitmentRecordRequestDto recruitmentRecordRequestDto);

    /// <summary>
    /// Retrieves all recruitmentRecords.
    /// </summary>
    /// <returns>The result containing a list of recruitmentRecord response data transfer objects.</returns>
    public Task<Result<List<RecruitmentRecordResponseDto>>> GetAllRecruitmentRecordsAsync();

    /// <summary>
    /// Retrieves a recruitmentRecord by its ID.
    /// </summary>
    /// <param name="id">The ID of the recruitmentRecord to retrieve.</param>
    /// <returns>The result containing the recruitmentRecord response data transfer object.</returns>
    public Task<Result<RecruitmentRecordResponseDto>> GetRecruitmentRecordByIdAsync(int id);

    /// <summary>
    /// Updates a recruitmentRecord by its ID.
    /// </summary>
    /// <param name="id">The ID of the recruitmentRecord to update.</param>
    /// <param name="recruitmentRecordRequestDto">The data transfer object containing updated recruitmentRecord information.</param>
    /// <returns>The result containing the updated recruitmentRecord response data transfer object.</returns>
    public Task<Result<RecruitmentRecordResponseDto>> UpdateRecruitmentRecordAsync(int id, RecruitmentRecordRequestDto recruitmentRecordRequestDto);

    /// <summary>
    /// Removes a recruitmentRecord by its ID.
    /// </summary>
    /// <param name="id">The ID of the recruitmentRecord to remove.</param>
    /// <returns>The result indicating the success of removing the recruitmentRecord.</returns>
    public Task<Result> DeleteRecruitmentRecordAsync(int id);
}