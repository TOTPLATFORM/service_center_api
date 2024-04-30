using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IInventoryService : IApplicationService ,IScopedService
{
	/// <summary>
	/// function to add inventory that take inventoryDto   
	/// </summary>
	/// <param name="timeSlotRequestDto">time slot request dto</param>
	/// <returns>Inventory added successfully </returns>
	public Task<Result> AddInventoryAsync(InventoryRequestDto inventoryRequestDto);

	/// <summary>
	/// function to get all inventories 
	/// </summary>
	/// <returns>list all inventoryResponseDto </returns>
	public Task<Result<List<InventoryResponseDto>>> GetAllInventoriesAsync();
}
