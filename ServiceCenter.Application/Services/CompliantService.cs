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

public class ComplaintService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ComplaintService> logger, IUserContextService userContext) : IComplaintService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ComplaintService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddComplaintAsync(ComplaintRequestDto ComplaintRequestDto)
    {
        var result = _mapper.Map<Complaint>(ComplaintRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ComplaintRequestDto to Complaint. ComplaintRequestDto: {@ComplaintRequestDto}", ComplaintRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Complaints.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Complaint added successfully to the database");
        return Result.SuccessWithMessage("Complaint added successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<List<ComplaintResponseDto>>> GetAllComplaintsAsync()
    {
        var result = await _dbContext.Complaints
             .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Complaint. Total count: { Complaint}.", result.Count);

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<ComplaintResponseDto>> GetComplaintByIdAsync(int id)
    {
        var result = await _dbContext.Complaints
            .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Complaint Id not found,Id {ComplaintId}", id);

            return Result.NotFound(["Complaint not found"]);
        }

        _logger.LogInformation("Fetching Complaint");

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<ComplaintResponseDto>> UpdateComplaintAsync(int id, ComplaintRequestDto ComplaintRequestDto)
    {
        var result = await _dbContext.Complaints.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Complaint Id not found,Id {ComplaintId}", id);
            return Result.NotFound(["Complaint not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ComplaintRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ComplaintResponse = _mapper.Map<ComplaintResponseDto>(result);
        if (ComplaintResponse is null)
        {
            _logger.LogError("Failed to map ComplaintRequestDto to ComplaintResponseDto. ComplaintRequestDto: {@ComplaintRequestDto}", ComplaintResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Complaint , Id {Id}", id);

        return Result.Success(ComplaintResponse);
    }
}

