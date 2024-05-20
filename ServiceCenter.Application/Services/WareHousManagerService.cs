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

public class WareHousManagerService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<WareHousManagerService> logger, IUserContextService userContext) : IWareHousManagerService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<WareHousManagerService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;
    ///<inheritdoc/>
    public async Task<Result> AddWareHouseManagerServiceAsync(WareHouseManagerRequestDto wareHouseManagerRequestDto)
    {
        var wareHouseManager = _mapper.Map<WareHouseManager>(wareHouseManagerRequestDto);
        var inventory = _dbContext.Inventories.FirstOrDefault(C => C.Id == wareHouseManagerRequestDto.InventoryId);

        if (inventory is null)
        {
            _logger.LogInformation("inventory  not found");
            return Result.Error("wareHouseManager added failed to the database");
        }
        wareHouseManager.Inventory = inventory;

        _dbContext.WareHouseManagers.Add(wareHouseManager);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("wareHouseManager added successfully to the database");
        return Result.SuccessWithMessage("wareHouseManager added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteWareHouseManagerServiceAsync(string id)
    {
        var wareHouseManager = await _dbContext.WareHouseManagers.FirstOrDefaultAsync(W => W.Id == id);

        if (wareHouseManager is null)
        {
            _logger.LogInformation("wareHouseManager  not found");
            return Result.Error("wareHouseManager not found in database");
        }

        _dbContext.WareHouseManagers.Remove(wareHouseManager);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("wareHouseManager remove successfully to the database");
        return Result.SuccessWithMessage("wareHouseManager remove successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<WareHouseManagerResponseDto>>> GetAllWareHouseManagerServicesAsync()
    {
        
        var wareHouseManager = await _dbContext.WareHouseManagers.ProjectTo<WareHouseManagerResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        _logger.LogInformation("Fetching all wareHouseManager . Total count: {inventories}.", wareHouseManager.Count);

        return Result.Success(wareHouseManager);
    }
    ///<inheritdoc/>
    public async Task<Result<WareHouseManagerResponseDto>> GetWareHouseManagerServiceByIdAsync(string id)
    {

        var wareHouseManagerResponseDto = await _dbContext.WareHouseManagers.ProjectTo<WareHouseManagerResponseDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(W => W.Id == id);

        if (wareHouseManagerResponseDto is null)
        {
            _logger.LogWarning("wareHouseManager  Id not found,Id {id}", id);
            return Result.NotFound(["The wareHouseManager  is not found"]);
        }

        _logger.LogInformation("Fetched wareHouseManager  details");
        return Result.Success(wareHouseManagerResponseDto);
    }
    ///<inheritdoc/>
    public async Task<Result<List<WareHouseManagerResponseDto>>> SearchWareHouseManagerByTextAsync(string text)
    {
        var wareHouseManagerResponseDto = await _dbContext.WareHouseManagers.Where(w => w.FirstName.Contains(text)|| w.LastName.Contains(text)|| w.Email.Contains(text)|| w.Inventory.InventoryName.Contains(text)).ProjectTo<WareHouseManagerResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        if (wareHouseManagerResponseDto is null)
        {
            _logger.LogWarning("wareHouseManager  Id not found,text {text}", text);
            return Result.NotFound(["The wareHouseManager  is not found"]);
        }

        _logger.LogInformation("Fetched wareHouseManager  by text");
        return Result.Success(wareHouseManagerResponseDto);
    }
    ///<inheritdoc/>
    public async Task<Result<WareHouseManagerResponseDto>> UpdateWareHouseManagerServiceAsync(string id, WareHouseManagerRequestDto wareHouseManagerRequestDto)
    {
        var wareHouseManagerResponseDto = await _dbContext.WareHouseManagers.FindAsync(id);
        var inventoryResponseDto = await _dbContext.Inventories.FindAsync(id);

        if (wareHouseManagerResponseDto is null)
        {
            _logger.LogWarning("wareHouse manager Id not found,Id {wareHouse managerId}", id);
            return Result.NotFound(["wareHouse manager not found"]);
        }

        _mapper.Map(wareHouseManagerRequestDto, wareHouseManagerResponseDto);
        wareHouseManagerResponseDto.Inventory = inventoryResponseDto;
        await _dbContext.SaveChangesAsync();

        var WareHouseManager = _mapper.Map<WareHouseManagerResponseDto>(wareHouseManagerResponseDto);

        if (WareHouseManager is null)
        {
            _logger.LogError("Failed to map wareHouseManagerRequestDto to wareHouseManagerResponseDto. wareHouseManagerRequestDto: {@wareHouseManagerRequestDto}", WareHouseManager);

            return Result.Invalid(new List<ValidationError>
            {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }

        _logger.LogInformation("Updated ware house manager , Id {Id}", id);

        return Result.Success(WareHouseManager);
    }
}
