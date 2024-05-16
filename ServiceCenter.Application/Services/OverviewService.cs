using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Overviews;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Domain.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ServiceCenter.Application.Services;

public class OverviewService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<OverviewService> logger, IUserContextService userContext) : IOverviewService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<OverviewService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddOverviewAsync(OverviewRequestDto OverviewRequestDto)
    {
        var sales = await _dbContext.Users.OfType<Sales>().FirstOrDefaultAsync(s => s.Id == OverviewRequestDto.SalesId);
        var result = _mapper.Map<Overview>(OverviewRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map OverviewRequestDto to Overview. OverviewRequestDto: {@OverviewRequestDto}", OverviewRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Overviews.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Overview added successfully to the database");
        return Result.SuccessWithMessage("Overview added successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<List<OverviewResponseDto>>> GetAllOverviewAsync()
    {
        var result = await _dbContext.Overviews
             .ProjectTo<OverviewResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Overview. Total count: { Overview}.", result.Count);

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<OverviewResponseDto>> GetOverviewByIdAsync(int id)
    {
        var result = await _dbContext.Overviews
            .ProjectTo<OverviewResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Overview Id not found,Id {OverviewId}", id);

            return Result.NotFound(["Overview not found"]);
        }

        _logger.LogInformation("Fetching Overview");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<OverviewResponseDto>> UpdateOverviewAsync(int id, OverviewRequestDto OverviewRequestDto)
    {
        var result = await _dbContext.Overviews.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Overview Id not found,Id {OverviewId}", id);
            return Result.NotFound(["Overview not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(OverviewRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var OverviewResponse = _mapper.Map<OverviewResponseDto>(result);
        if (OverviewResponse is null)
        {
            _logger.LogError("Failed to map OverviewRequestDto to OverviewResponseDto. OverviewRequestDto: {@OverviewRequestDto}", OverviewResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Overview , Id {Id}", id);

        return Result.Success(OverviewResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteOverviewAsync(int id)
    {
        var Overview = await _dbContext.Overviews.FindAsync(id);

        if (Overview is null)
        {
            _logger.LogWarning("Overview Invaild Id ,Id {OverviewId}", id);
            return Result.NotFound(["Overview Invaild Id"]);
        }

        _dbContext.Overviews.Remove(Overview);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Overview removed successfully in the database");
        return Result.SuccessWithMessage("Overview removed successfully");
    }


}