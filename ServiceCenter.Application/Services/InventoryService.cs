using AutoMapper;
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
			_logger.LogError("Failed to map InventoryRequestDto to TimeSlot. InventoryRequestDto: {@InevntoryRequestDto}", inventoryRequestDto);

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
}
