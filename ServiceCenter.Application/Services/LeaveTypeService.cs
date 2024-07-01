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

public class LeaveTypeService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<LeaveTypeService> logger, IUserContextService userContext) : ILeaveTypeService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<LeaveTypeService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result> AddLeaveTypeAsync(LeaveTypeRequestDto leaveTypeRequestDto)
    {
        var leaveType = _mapper.Map<LeaveType>(leaveTypeRequestDto);

        if (leaveType is null)
        {
            _logger.LogError("Failed to map leaveTypeRequestDto to leaveType. leaveTypeRequestDto: {@leaveTypeRequestDto}", leaveTypeRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Error"
                }
            });
        }
        leaveType.CreatedBy = _userContext.Email;
        await _dbContext.LeaveTypes.AddAsync(leaveType);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("LeaveType added successfully to the database");
        return Result.SuccessWithMessage("LeaveType added successfully");
    }

    /// <inheritdoc/>
    public async Task<Result> DeleteLeaveTypeAsync(int id)
    {
        var type = await _dbContext.LeaveTypes.FindAsync(id);

        if (type is null)
        {
            _logger.LogWarning("type Invaild Id ,Id {LeaveTypeId}", id);
            return Result.NotFound(["type Invaild Id"]);
        }

        _dbContext.LeaveTypes.Remove(type);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("type remove successfully in the database");
        return Result.SuccessWithMessage("type removed successfully");
    }

    /// <inheritdoc/>

    public async Task<Result<PaginationResult<LeaveTypeResponseDto>>> GetAllLeaveTypesAsync(int itemCount, int index)
    {
        var types = await _dbContext.LeaveTypes.ProjectTo<LeaveTypeResponseDto>(_mapper.ConfigurationProvider)
         .GetAllWithPagination(itemCount, index);
        if (index > types.TotalCount)
        {
            types.End = types.TotalCount;
        }

        _logger.LogInformation("Fetching all prescriptions. Total count: {Type}.", types.TotalCount);
        return Result.Success(types);
    }
    /// <inheritdoc/>

    public async Task<Result<LeaveTypeResponseDto>> GetLeaveTypeByIdAsync(int id)
    {
        var type = await _dbContext.LeaveTypes
          .ProjectTo<LeaveTypeResponseDto>(_mapper.ConfigurationProvider)
          .FirstOrDefaultAsync(s => s.Id == id);
        if (type is null)
        {
            _logger.LogWarning("type Id not found,Id {LeaveTypeId}", id);
            return Result.NotFound(["type not found"]);
        }
        _logger.LogInformation("Fetching department");
        return Result.Success(type);
    }

    /// <inheritdoc/>
    public async Task<Result<PaginationResult<LeaveTypeResponseDto>>> SearchLeaveTypeByTextAsync(string text, int itemCount, int index)
    {
        var type = await _dbContext.LeaveTypes.ProjectTo<LeaveTypeResponseDto>(_mapper.ConfigurationProvider)
                                .Where(d => d.TypeName.Contains(text)).GetAllWithPagination(itemCount, index);

        if (index > type.TotalCount)
        {
            type.End = type.TotalCount;
        }

        _logger.LogInformation("Fetching search leave type by name . Total count: {type}.", type.TotalCount);
        return Result.Success(type);
    }

    /// <inheritdoc/>

    public async Task<Result<LeaveTypeResponseDto>> UpdateLeaveTypeAsycn(int id, LeaveTypeRequestDto leaveTypeRequestDto)
    {
        var type = await _dbContext.LeaveTypes.FindAsync(id);

        if (type is null)
        {
            _logger.LogWarning("type Id not found,Id {typeId}", id);
            return Result.NotFound(["type not found"]);
        }

        _mapper.Map(leaveTypeRequestDto, type);
        type.ModifiedBy = _userContext.Email;
        await _dbContext.SaveChangesAsync();

        var typeResponse = _mapper.Map<LeaveTypeResponseDto>(type);
        if (typeResponse is null)
        {
            _logger.LogError("Failed to map leaveTypeRequestDto to leaveTypeResponseDto. leaveTypeRequestDto: {@LeaveTypeRequestDto}", leaveTypeRequestDto);
            return Result.Invalid(new List<ValidationError>
                {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
                });
        }

        _logger.LogInformation("Updated type , Id {Id}", id);

        return Result.Success(typeResponse);
    }
}