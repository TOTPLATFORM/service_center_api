using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ApplicantService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ApplicantService> logger, IAuthService authService) : IApplicantService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ApplicantService> _logger = logger;
    private readonly IAuthService _authService = authService;

    /// <inheritdoc/>
    public async Task<Result> AddApplicantAsync(ApplicantRequestDto applicantRequestDto)
    {
        string role = "Applicant";

        var applicant = _mapper.Map<Applicant>(applicantRequestDto);

        var department = await _dbContext.Departments.FindAsync(applicantRequestDto.DepartmentId);

        if (department is null)
        {
            _logger.LogWarning("department Invaild Id ,Id {departmentId}", applicantRequestDto.DepartmentId);
            return Result.NotFound(["department Invaild Id"]);
        }

        applicant.Department = department;

        var applicantAdded = await _authService.RegisterUserWithRoleAsync(applicant, applicantRequestDto.Password, role);

        if (!applicantAdded.IsSuccess)
        {
            return Result.Error(applicantAdded.Errors.FirstOrDefault());
        }

        _logger.LogInformation("Applicant added successfully in the database");
        return Result.SuccessWithMessage("Applicant added successfully");
    }

    public async Task<Result<PaginationResult<ApplicantResponseDto>>> GetAllApplicantsAsync(int itemCount, int index)
    {
        var applicants = await _dbContext.Applicants.ProjectTo<ApplicantResponseDto>(_mapper.ConfigurationProvider).GetAllWithPagination(itemCount, index);
        if (index > applicants.TotalCount)
        {
            applicants.End = applicants.TotalCount;
        }
        _logger.LogInformation("Fetching all cities with pagination. Total count: {Applicant}.", applicants);

        return Result.Success(applicants);
    }


    /// <inheritdoc/>
    public async Task<Result<ApplicantResponseDto>> GetApplicantByIdAsync(string id)
    {
        var applicant = await _dbContext.Applicants
            .ProjectTo<ApplicantResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == id);
        if (applicant is null)
        {
            _logger.LogWarning("applicant Id not found,Id {applicantId}", id);
            return Result.NotFound(["applicant not found"]);
        }
        _logger.LogInformation("Fetching applicant");
        return Result.Success(applicant);
    }
    /// <inheritdoc/>

    public async Task<Result<PaginationResult<ApplicantResponseDto>>> SearchApplicantByTextAsync(string text, int itemCount, int index)
    {
        var applicant = await _dbContext.Applicants.ProjectTo<ApplicantResponseDto>(_mapper.ConfigurationProvider)
                                    .Where(d => d.FirstName.Contains(text)).GetAllWithPagination(itemCount, index);

        if (index > applicant.TotalCount)
        {
            applicant.End = applicant.TotalCount;
        }

        _logger.LogInformation("Fetching search applicant by name . Total count: {applicant}.", applicant.TotalCount);
        return Result.Success(applicant);

    }

    /// <inheritdoc/>

    public async Task<Result<ApplicantResponseDto>> UpdateApplicantAsync(string id, ApplicantRequestDto applicantRequestDto)
    {
        var applicant = await _dbContext.Applicants.FindAsync(id);

        if (applicant is null)
        {
            _logger.LogWarning("applicant Id not found,Id {applicantId}", id);
            return Result.NotFound(["applicant not found"]);
        }

        applicantRequestDto.UserName = applicant.UserName;

        _mapper.Map(applicantRequestDto, applicant);

        var result = await _authService.UpdateUserAsync(applicant);

        if (!result.IsSuccess)
        {
            return Result.Error(result.Errors.FirstOrDefault());
        }

        var updatedApplicant = _mapper.Map<ApplicantResponseDto>(applicant);

        _logger.LogInformation("applicant  updated successfully");
        return Result.Success(updatedApplicant, "applicant  updated successfully");
    }
}