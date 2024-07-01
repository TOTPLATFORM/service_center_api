using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

/// <summary>
/// Controller responsible for handling applicant-related HTTP requests.
/// </summary>
/// <param name="applicantService">The service for performing applicant-related operations.</param>
/// <seealso cref="BaseController"/>
public class ApplicantController(IApplicantService applicantService) : BaseController
{
    private readonly IApplicantService _applicantService = applicantService;

    /// <summary>
    /// Adds a new applicant asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="applicantDto">The DTO representing the applicant to create.</param>
    /// <returns>A result indicating the outcome of the add operation.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddApplicant(ApplicantRequestDto applicantDto)
    {
        return await _applicantService.AddApplicantAsync(applicantDto);
    }

    /// <summary>
    /// Retrieves all applicants asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <returns>A result containing a list of applicant response DTOs.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ApplicantResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ApplicantResponseDto>>> GetAllApplicants(int itemCount, int index)
    {
        return await _applicantService.GetAllApplicantsAsync(itemCount, index);
    }

    /// <summary>
    /// Retrieves an applicant by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the applicant to retrieve.</param>
    /// <returns>A result containing the applicant response DTO.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ApplicantResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ApplicantResponseDto>> GetApplicantById(string id)
    {
        return await _applicantService.GetApplicantByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing applicant by its ID asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="id">The ID of the applicant to update.</param>
    /// <param name="applicantDto">The DTO representing the updated applicant.</param>
    /// <returns>A result containing the updated applicant response DTO.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ApplicantResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ApplicantResponseDto>> UpdateApplicant(string id, ApplicantRequestDto applicantDto)
    {
        return await _applicantService.UpdateApplicantAsync(id, applicantDto);
    }

    /// <summary>
    /// Searches for applicants by text asynchronously.
    /// </summary>
    /// <remarks>Available to users with the role: Admin.</remarks>
    /// <param name="applicantName">The text to search for in applicant names.</param>
    /// <returns>A result containing a list of applicant response DTOs that match the search criteria.</returns>
    [HttpGet("search/{applicantName}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ApplicantResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ApplicantResponseDto>>> SearchApplicantByText(string applicantName, int itemCount, int index)
    {
        return await _applicantService.SearchApplicantByTextAsync(applicantName, itemCount, index);
    }
}
