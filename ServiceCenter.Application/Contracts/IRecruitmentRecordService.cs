using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for recruitment record-related services that manages recruitment record data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IRecruitmentRecordService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new recruitment record to the database.
	/// </summary>
	/// <param name="recruitmentRecordRequestDto">the recruitment record data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the recruitment record addition.</returns>
	public Task<Result> AddRecruitmentRecordAsync(RecruitmentRecordRequestDto recruitmentRecordRequestDto);

	/// <summary>
	/// asynchronously retrieves all recruitment records in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of recruitment record to retrieve</param>
	///<param name="index">index of recruitment record to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of recruitment record response DTOs.</returns>
	public Task<Result<PaginationResult<RecruitmentRecordResponseDto>>> GetAllRecruitmentRecordsAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a recruitment record by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the recruitment record to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the recruitment record response DTO.</returns>
	public Task<Result<RecruitmentRecordResponseDto>> GetRecruitmentRecordByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing recruitment record.
	/// </summary>
	/// <param name="id">the unique identifier of the recruitment record to update.</param>
	/// <param name="recruitmentRecordRequestDto">the recruitment record data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<RecruitmentRecordResponseDto>> UpdateRecruitmentRecordAsync(int id, RecruitmentRecordRequestDto recruitmentRecordRequestDto);

	/// <summary>
	/// asynchronously deletes a recruitment record from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the recruitment record to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteRecruitmentRecordAsync(int id);
}