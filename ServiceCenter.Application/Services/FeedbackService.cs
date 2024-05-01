using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class FeedbackService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<FeedbackService> logger, IUserContextService userContext) : IFeedbackService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<FeedbackService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddFeedbackAsync(FeedbackRequestDto FeedbackRequestDto)
    {
        var result = _mapper.Map<Feedback>(FeedbackRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map FeedbackRequestDto to Feedback. FeedbackRequestDto: {@FeedbackRequestDto}", FeedbackRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Feedbacks.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Feedback added successfully to the database");
        return Result.SuccessWithMessage("Feedback added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<FeedbackResponseDto>>> GetAllFeedbackAsync()
    {
        var result = await _dbContext.Feedbacks
             .ProjectTo<FeedbackResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Feedback. Total count: { Feedback}.", result.Count);

        return Result.Success(result);
    }
}