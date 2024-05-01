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

public class RoomService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<RoomService> logger, IUserContextService userContext) : IRoomService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<RoomService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddRoomAsync(RoomRequestDto RoomRequestDto)
    {
        var result = _mapper.Map<Room>(RoomRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map RoomRequestDto to Room. RoomRequestDto: {@RoomRequestDto}", RoomRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Rooms.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Room added successfully to the database");
        return Result.SuccessWithMessage("Room added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<RoomResponseDto>>> GetAllRoomAsync()
    {
        var result = await _dbContext.Rooms
             .ProjectTo<RoomResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Room. Total count: { Room}.", result.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<RoomResponseDto>> GetRoomByIdAsync(int id)
    {
        var result = await _dbContext.Rooms
            .ProjectTo<RoomResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Room Id not found,Id {RoomId}", id);

            return Result.NotFound(["Room not found"]);
        }

        _logger.LogInformation("Fetching Room");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<RoomResponseDto>> UpdateRoomAsync(int id, RoomRequestDto RoomRequestDto)
    {
        var result = await _dbContext.Rooms.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Room Id not found,Id {RoomId}", id);
            return Result.NotFound(["Room not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(RoomRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var RoomResponse = _mapper.Map<RoomResponseDto>(result);
        if (RoomResponse is null)
        {
            _logger.LogError("Failed to map RoomRequestDto to RoomResponseDto. RoomRequestDto: {@RoomRequestDto}", RoomResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Room , Id {Id}", id);

        return Result.Success(RoomResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteRoomAsync(int id)
    {
        var Room = await _dbContext.Rooms.FindAsync(id);

        if (Room is null)
        {
            _logger.LogWarning("Room Invaild Id ,Id {RoomId}", id);
            return Result.NotFound(["Room Invaild Id"]);
        }

        _dbContext.Rooms.Remove(Room);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Room removed successfully in the database");
        return Result.SuccessWithMessage("Room removed successfully");
    }
}