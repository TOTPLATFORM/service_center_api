using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class ApplicantController(IApplicantService applicantService) : BaseController
{
    private readonly IApplicantService _applicantService = applicantService;

    /// <summary>
    /// Adds a new applicant asynchronously.
    /// </summary>
    /// <param name="applicantDto">The DTO representing the applicant to create.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns> a task that represents the asynchronous operation, which encapsulates the result of the addition process .</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddApplicant(ApplicantRequestDto applicantDto)
    {
        return await _applicantService.AddApplicantAsync(applicantDto);
    }

    /// <summary>
    /// retrieves all applicant in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of applicant to retrieve</param>
    ///<param name="index">index of applicant to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all applicant.</returns> [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ApplicantResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ApplicantResponseDto>>> GetAllApplicants(int itemCount, int index)
    {
        return await _applicantService.GetAllApplicantsAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves a applicant  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the applicant .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the applicant category details.</returns>[HttpGet("{id}")]
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
    /// <param name="id">The ID of the applicant to update.</param>
    /// <param name="applicantDto">The DTO representing the updated applicant.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>]
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ApplicantResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ApplicantResponseDto>> UpdateApplicant(string id, ApplicantRequestDto applicantDto)
    {
        return await _applicantService.UpdateApplicantAsync(id, applicantDto);
    }

    /// <summary>
    /// searches applicant  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of applicants to retrieve</param>
    ///<param name="index">index of applicants to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of applicant  that match the search criteria.</returns>
    [HttpGet("search/{text}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ApplicantResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ApplicantResponseDto>>> SearchApplicantByText(string text, int itemCount, int index)
    {
        return await _applicantService.SearchApplicantByTextAsync(text, itemCount, index);
    }
}
