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

public class InventoryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<InventoryService> logger, IUserContextService userContext) : IInventoryService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<InventoryService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddInventoryAsync(InventoryRequestDto inventoryRequestDto)
	{
		var branch = await _dbContext.Branches.FirstOrDefaultAsync(e => e.Id == inventoryRequestDto.BranchId);

		if (branch == null)
		{
			_logger.LogError("Branch not found. BranchId: {BranchId}", inventoryRequestDto.BranchId);

			return Result.Invalid(new List<ValidationError>
		    {
			      new ValidationError
			      {
				    ErrorMessage = "Branch not found"
			      }
		    });
		}

		var existingInventory = await _dbContext.Inventories.FirstOrDefaultAsync(e => e.BranchId == inventoryRequestDto.BranchId);

		if (existingInventory != null)
		{
			_logger.LogError("Inventory already exists for the branch. BranchId: {BranchId}", inventoryRequestDto.BranchId);

			return Result.Invalid(new List<ValidationError>
		    {
			       new ValidationError
			       {
				        ErrorMessage = "Inventory already exists for the branch"
			       }
		    });
		}

		var result = _mapper.Map<Inventory>(inventoryRequestDto);

		if (result is null)
		{
			_logger.LogError("Failed to map InventoryRequestDto to Inventory. InventoryRequestDto: {@InevntoryRequestDto}", inventoryRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

		result.Branch = branch;

		_dbContext.Inventories.Add(result);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Inventory added successfully to the database");

		return Result.SuccessWithMessage("Inventory added successfully");
	}
	///<inheritdoc/>
	public async Task<Result<PaginationResult<InventoryResponseDto>>> GetAllInventoriesAsync(int itemCount, int index)
	{
		var result = await _dbContext.Inventories
				 .ProjectTo<InventoryResponseDto>(_mapper.ConfigurationProvider)
				 .GetAllWithPagination( itemCount,  index);

		_logger.LogInformation("Fetching all Inventories. Total count: {Inventory}.", result.Data.Count);

		return Result.Success(result);
	}
	///<inheritdoc/>
	public async Task<Result<InventoryGetByIdResponseDto>> GetInventoryByIdAsync(int id)
	{
		var result = await _dbContext.Inventories
				.ProjectTo<InventoryGetByIdResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(inventory => inventory.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Inventory Id not found,Id {InevntoryId}", id);

			return Result.NotFound(["Inventory not found"]);
		}

		_logger.LogInformation("Fetching Inventory");

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<InventoryResponseDto>> UpdateInventoryAsync(int id, InventoryUpdatedRequestDto inventoryRequestDto)
	{
		var result = await _dbContext.Inventories.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Inventory Id not found,Id {InventoryId}", id);
			return Result.NotFound(["Inventory not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(inventoryRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var inventory = _mapper.Map<InventoryResponseDto>(result);

		if (inventory is null)
		{
			_logger.LogError("Failed to map InventoryRequestDto to InventoryResponseDto. InventoryRequestDto: {@InventoryRequestDto}", inventory);

			return Result.Invalid(new List<ValidationError>
			{
					new ValidationError
					{
						ErrorMessage = "Validation Errror"
					}
			});
		}

		_logger.LogInformation("Updated Inventory , Id {Id}", id);

		return Result.Success(inventory);
	}

	///<inheritdoc/>
	public async Task<Result<PaginationResult<InventoryResponseDto>>> SearchInventoryByTextAsync(string text, int itemCount, int index)
	{
		var name = await _dbContext.Inventories
					   .ProjectTo<InventoryResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.InventoryName.Contains(text))
					   .GetAllWithPagination( itemCount,  index);

		_logger.LogInformation("Fetching search time slot by name . Total count: {time slot}.", name.Data.Count);

		return Result.Success(name);

	}

	///<inheritdoc/>
	public async Task<Result> DeleteInventoryAsync(int id)
	{
		var inventory = await _dbContext.Inventories.FindAsync(id);

		if (inventory is null)
		{
			_logger.LogWarning("Inventory Invaild Id ,Id {InventoryId}", id);

			return Result.NotFound(["Inventory Invaild Id"]);
		}

		_dbContext.Inventories.Remove(inventory);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Inventory removed successfully in the database");

		return Result.SuccessWithMessage("Inventory removed successfully");
	}
}
