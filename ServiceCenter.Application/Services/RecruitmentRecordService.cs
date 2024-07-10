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

public class RecruitmentRecordService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<RecruitmentRecordService> logger, IUserContextService userContext) : IRecruitmentRecordService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<RecruitmentRecordService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result> AddRecruitmentRecordAsync(RecruitmentRecordRequestDto recruitmentRecordRequestDto)
    {
        var recruitmentRecord = _mapper.Map<RecruitmentRecord>(recruitmentRecordRequestDto);
        var applicant = await _dbContext.Applicants.FindAsync(recruitmentRecordRequestDto.ApplicantId);

        if (recruitmentRecord is null)
        {
            _logger.LogError("Failed to map recruitmentRecordRequestDto to RecruitmentRecord. recruitmentRecordRequestDto: {@recruitmentRecordRequestDto}", recruitmentRecordRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Error"
                }
            });
        }

        if (applicant is null)
        {
            _logger.LogWarning("applicant Invaild Id ,Id {applicantId}", recruitmentRecordRequestDto.ApplicantId);
            return Result.NotFound(["applicant Invaild Id"]);
        }

        recruitmentRecord.Applicant = applicant;

        recruitmentRecord.CreatedBy = _userContext.Email;
        await _dbContext.RecruitmentRecords.AddAsync(recruitmentRecord);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("RecruitmentRecord added successfully in the database");
        return Result.SuccessWithMessage("RecruitmentRecord added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteRecruitmentRecordAsync(int id)
    {
        var recruitmentRecord = await _dbContext.RecruitmentRecords.FindAsync(id);

        if (recruitmentRecord is null)
        {
            _logger.LogWarning("recruitmentRecord Invaild Id ,Id {id}", id);
            return Result.NotFound(["recruitmentRecord Invaild Id"]);
        }

        _dbContext.RecruitmentRecords.Remove(recruitmentRecord);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("recruitmentRecord removed successfully in the database");
        return Result.SuccessWithMessage("RecruitmentRecord removed successfully ");
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<RecruitmentRecordResponseDto>>> GetAllRecruitmentRecordsAsync(int itemCount, int index)
    {
        var recruitmentRecords = await _dbContext.RecruitmentRecords
                  .ProjectTo<RecruitmentRecordResponseDto>(_mapper.ConfigurationProvider)
                  .GetAllWithPagination( itemCount,index);
        _logger.LogInformation("Fetching all recruitmentRecords. Total count: {recruitmentRecords}.", recruitmentRecords.Data.Count);
        return Result.Success(recruitmentRecords);
    }

    /// <inheritdoc/>
    public async Task<Result<RecruitmentRecordResponseDto>> GetRecruitmentRecordByIdAsync(int id)
    {
        var recruitmentRecord = await _dbContext.RecruitmentRecords
            .ProjectTo<RecruitmentRecordResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (recruitmentRecord is null)
        {
            _logger.LogWarning("recruitmentRecord Id not found,Id {id}", id);
            return Result.NotFound(["recruitmentRecord not found"]);
        }
        _logger.LogInformation("Fetching recruitmentRecord");
        return Result.Success(recruitmentRecord);
    }

    /// <inheritdoc/>
    public async Task<Result<RecruitmentRecordResponseDto>> UpdateRecruitmentRecordAsync(int id, RecruitmentRecordRequestDto recruitmentRecordRequestDto)
    {
        var recruitmentRecord = await _dbContext.RecruitmentRecords.FindAsync(id);
        var applicant = _dbContext.Applicants.Find(recruitmentRecordRequestDto.ApplicantId);

        if (recruitmentRecord is null)
        {
            _logger.LogWarning("recruitmentRecord Id not found,Id {id}", id);
            return Result.NotFound(["RecruitmentRecord not found"]);
        }

        if (applicant is null)
        {
            _logger.LogWarning("applicant Invaild Id ,Id {applicantId}", recruitmentRecordRequestDto.ApplicantId);
            return Result.NotFound(["applicant Invaild Id"]);
        }

        recruitmentRecord.ModifiedBy = _userContext.Email;

        _mapper.Map(recruitmentRecordRequestDto, recruitmentRecord);

        await _dbContext.SaveChangesAsync();

        var recruitmentRecordResponse = _mapper.Map<RecruitmentRecordResponseDto>(recruitmentRecord);
        if (recruitmentRecordResponse is null)
        {
            _logger.LogError("Failed to map RecruitmentRecordRequestDto to recruitmentRecordResponseDto. RecruitmentRecordRequestDto: {@recruitmentRecordRequestDto}", recruitmentRecordRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }

        _logger.LogInformation("Updated recruitmentRecord , Id {id}", id);

        return Result.Success(recruitmentRecordResponse);
    }
}