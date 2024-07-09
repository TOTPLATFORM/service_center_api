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
/// Service interface for handling applicant operations.
/// </summary>
public interface IApplicantService : IApplicationService, IScopedService
{
    /// <summary>
    /// Adds a new applicant asynchronously.
    /// </summary>
    /// <param name="applicantRequestDto">The data transfer object containing applicant information.</param>
    /// <returns>The result indicating the success of adding the applicant.</returns>
    public Task<Result> AddApplicantAsync(ApplicantRequestDto applicantRequestDto);

    /// <summary>
    /// Retrieves all applicants asynchronously.
    /// </summary>
    /// <returns>The result containing a list of applicant response data transfer objects.</returns>
    public Task<Result<PaginationResult<ApplicantResponseDto>>> GetAllApplicantsAsync(int itemCount, int index);

    /// <summary>
    /// Retrieves an applicant by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the applicant to retrieve.</param>
    /// <returns>The result containing the applicant response data transfer object.</returns>
    public Task<Result<ApplicantResponseDto>> GetApplicantByIdAsync(string id);

	/// <summary>
	/// function to search by applicant name  that take  applicant name
	/// </summary>
	/// <param name="text">applicant name</param>
	/// <returns>The result containing applicant response data transfer object.</returns>
	public Task<Result<PaginationResult<ApplicantResponseDto>>> SearchApplicantByTextAsync(string text, int itemCount, int index);

    /// <summary>
    /// Updates an applicant by their ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the applicant to update.</param>
    /// <param name="applicantRequestDto">The data transfer object containing updated applicant information.</param>
    /// <returns>The result containing the updated applicant response data transfer object.</returns>
    public Task<Result<ApplicantResponseDto>> UpdateApplicantAsync(string id, ApplicantRequestDto applicantRequestDto);
}