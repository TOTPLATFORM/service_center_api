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

public class InventoryService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<InventoryService> logger, IUserContextService userContext) : IInventoryService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<InventoryService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddInventoryAsync(InventoryRequestDto inventoryRequestDto)
	{
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

		_dbContext.Inventories.Add(result);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Inventory added successfully to the database");

		return Result.SuccessWithMessage("Inventory added successfully");
	}
	///<inheritdoc/>
	public async Task<Result<List<InventoryResponseDto>>> GetAllInventoriesAsync()
	{
		var result = await _dbContext.Inventories
				 .ProjectTo<InventoryResponseDto>(_mapper.ConfigurationProvider)
				 .ToListAsync();

		_logger.LogInformation("Fetching all Inventories. Total count: {Inventory}.", result.Count);

		return Result.Success(result);
	}
	///<inheritdoc/>
	public async Task<Result<InventoryResponseDto>> GetInventoryByIdAsync(int id)
	{
		var result = await _dbContext.Inventories
				.ProjectTo<InventoryResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(timeslot => timeslot.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Inventory Id not found,Id {InevntoryId}", id);

			return Result.NotFound(["Inventory not found"]);
		}

		_logger.LogInformation("Fetching Inventory");

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<InventoryResponseDto>> UpdateInventoryAsync(int id, InventoryRequestDto timeSlotRequestDto)
	{
		var result = await _dbContext.Inventories.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Inventory Id not found,Id {InventoryId}", id);
			return Result.NotFound(["Inventory not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(timeSlotRequestDto, result);

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
	public async Task<Result<List<InventoryResponseDto>>> SearchInventoryByTextAsync(string text)
	{


		//if (string.IsNullOrWhiteSpace(text))
		//{
		//	_logger.LogError("Search text cannot be empty", text);

		//	return new Result.Invalid(new List<ValidationError>
		//	{
		//		new ValidationError
		//		{
		//			ErrorMessage = "Validation Errror : Search text cannot be empty"
		//		}
		//	});
		//}

		var Days = await _dbContext.Inventories
					   .ProjectTo<InventoryResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.InventoryName.Contains(text))
					   .ToListAsync();

		_logger.LogInformation("Fetching search time slot by name . Total count: {time slot}.", Days.Count);

		return Result.Success(Days);

	}

	///<inheritdoc/>
	public async Task<Result> DeleteInventoryAsync(int id)
	{
		var timeSlot = await _dbContext.Inventories.FindAsync(id);

		if (timeSlot is null)
		{
			_logger.LogWarning("Inventory Invaild Id ,Id {InventoryId}", id);

			return Result.NotFound(["Inventory Invaild Id"]);
		}

		_dbContext.Inventories.Remove(timeSlot);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Inventory removed successfully in the database");

		return Result.SuccessWithMessage("Inventory removed successfully");
	}
}
