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

public class WareHouseManagerService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<WareHouseManagerService> logger, IUserContextService userContext, IAuthService authService) : IWareHouseManagerService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<WareHouseManagerService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService = authService;

	///<inheritdoc/>
	public async Task<Result> AddWareHouseManagerServiceAsync(WareHouseManagerRequestDto wareHouseManagerRequestDto)
    {
        string role = "WareHouseManager";
        var wareHouseManager = _mapper.Map<WareHouseManager>(wareHouseManagerRequestDto);
        var inventory = _dbContext.Inventories.FirstOrDefault(C => C.Id == wareHouseManagerRequestDto.InventoryId);
        var department = await _dbContext.Departments.FindAsync(wareHouseManagerRequestDto.DepartmentId);
        var wareHouseManagerInInventoy = await _dbContext.WareHouseManagers.Where(b => b.InventoryId == wareHouseManagerRequestDto.InventoryId).FirstOrDefaultAsync();

        if (department is null)
        {
            _logger.LogWarning("Department Invaild Id ,Id {departmentId}", wareHouseManagerRequestDto.DepartmentId);

            return Result.NotFound(["Department Invaild Id"]);
        }
        wareHouseManager.Department = department;

        if (inventory is null)
        {
            _logger.LogInformation("inventory  not found");
        }
        else
        {
			wareHouseManager.Inventory = inventory;
		}
        if (wareHouseManagerInInventoy != null)
        {
            _logger.LogError("Failed to added in database this  warehousemanager in inventory");
            return Result.Error("Failed to added in database this warehousemanager in inventory");
        }
        var warehouseMangerAdded = await _authService.RegisterUserWithRoleAsync(wareHouseManager, wareHouseManagerRequestDto.Password, role);
        await _dbContext.SaveChangesAsync();

		if (!warehouseMangerAdded.IsSuccess)
		{
			return Result.Error(warehouseMangerAdded.Errors.FirstOrDefault());
		}

		_logger.LogInformation("WareHouseManager added successfully in the database");

		return Result.SuccessWithMessage("WareHouseManager added successfully");
       
       

       
    }
  
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<WareHouseManagerResponseDto>>> GetAllWareHouseManagerServicesAsync(int itemcount, int index)
    {
        
        var wareHouseManager = await _dbContext.WareHouseManagers.ProjectTo<WareHouseManagerResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination( itemcount,  index);

        _logger.LogInformation("Fetching all wareHouseManager . Total count: {inventories}.", wareHouseManager.Data.Count);

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
    public async Task<Result<PaginationResult<WareHouseManagerResponseDto>>> SearchWareHouseManagerByTextAsync(string text, int itemcount, int index)
    {
        var wareHouseManagerResponseDto = await _dbContext.WareHouseManagers.Where(w => w.FirstName.Contains(text)|| w.LastName.Contains(text)|| w.Email.Contains(text)|| w.Inventory.InventoryName.Contains(text))
            .ProjectTo<WareHouseManagerResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination( itemcount,  index);

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
        var inventoryResponseDto = await _dbContext.Inventories.FindAsync(wareHouseManagerRequestDto.InventoryId);

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
