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

public class PerformanceReviewService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<PerformanceReviewService> logger, IUserContextService userContext) : IPerformanceReviewService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PerformanceReviewService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result> AddPerformanceReviewAsync(PerformanceReviewRequestDto performanceReviewRequestDto)
    {
        var performanceReview = _mapper.Map<PerformanceReview>(performanceReviewRequestDto);
        var employee = await _dbContext.Employees.FindAsync(performanceReviewRequestDto.EmployeeId);

        if (performanceReview is null)
        {
            _logger.LogError("Failed to map performanceReviewRequestDto to PerformanceReview. performanceReviewRequestDto: {@performanceReviewRequestDto}", performanceReviewRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Error"
                }
            });
        }

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", performanceReviewRequestDto.EmployeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        performanceReview.Employee = employee;

        performanceReview.CreatedBy = _userContext.Email;
        await _dbContext.PerformanceReviews.AddAsync(performanceReview);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("PerformanceReview added successfully in the database");
        return Result.SuccessWithMessage("PerformanceReview added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeletePerformanceReviewAsync(int id)
    {
        var performanceReview = await _dbContext.PerformanceReviews.FindAsync(id);

        if (performanceReview is null)
        {
            _logger.LogWarning("performanceReview Invaild Id ,Id {id}", id);
            return Result.NotFound(["performanceReview Invaild Id"]);
        }

        _dbContext.PerformanceReviews.Remove(performanceReview);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("performanceReview removed successfully in the database");
        return Result.SuccessWithMessage("PerformanceReview removed successfully ");
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<PerformanceReviewResponseDto>>> GetAllPerformanceReviewsAsync(int itemCount,int index)
    {
        var performanceReviews = await _dbContext.PerformanceReviews
                  .ProjectTo<PerformanceReviewResponseDto>(_mapper.ConfigurationProvider)
                  .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching all performanceReviews. Total count: {performanceReviews}.", performanceReviews.Data.Count);
        return Result.Success(performanceReviews);
    }

    /// <inheritdoc/>
    public async Task<Result<PerformanceReviewResponseDto>> GetPerformanceReviewByIdAsync(int id)
    {
        var performanceReview = await _dbContext.PerformanceReviews
            .ProjectTo<PerformanceReviewResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (performanceReview is null)
        {
            _logger.LogWarning("performanceReview Id not found,Id {id}", id);
            return Result.NotFound(["performanceReview not found"]);
        }
        _logger.LogInformation("Fetching performanceReview");
        return Result.Success(performanceReview);
    }

    /// <inheritdoc/>
    public async Task<Result<PerformanceReviewResponseDto>> UpdatePerformanceReviewAsync(int id, PerformanceReviewRequestDto performanceReviewRequestDto)
    {
        var performanceReview = await _dbContext.PerformanceReviews.FindAsync(id);
        var employee = _dbContext.Employees.Find(performanceReviewRequestDto.EmployeeId);

        if (performanceReview is null)
        {
            _logger.LogWarning("performanceReview Id not found,Id {id}", id);
            return Result.NotFound(["PerformanceReview not found"]);
        }

        if (employee is null)
        {
            _logger.LogWarning("employee Invaild Id ,Id {employeeId}", performanceReviewRequestDto.EmployeeId);
            return Result.NotFound(["employee Invaild Id"]);
        }

        performanceReview.ModifiedBy = _userContext.Email;

        _mapper.Map(performanceReviewRequestDto, performanceReview);

        await _dbContext.SaveChangesAsync();

        var performanceReviewResponse = _mapper.Map<PerformanceReviewResponseDto>(performanceReview);
        if (performanceReviewResponse is null)
        {
            _logger.LogError("Failed to map PerformanceReviewRequestDto to performanceReviewResponseDto. PerformanceReviewRequestDto: {@performanceReviewRequestDto}", performanceReviewRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }

        _logger.LogInformation("Updated performanceReview , Id {id}", id);

        return Result.Success(performanceReviewResponse);
    }
}