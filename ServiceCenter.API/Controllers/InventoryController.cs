using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class InventoryController(IInventoryService inventoryService) : BaseController
{
	private readonly IInventoryService _inventoryService = inventoryService;

	/// <summary>
	/// action for add inventory action that take inventory dto   
	/// </summary>
	/// <param name="inventoryRequestDto">inventory dto</param>
	/// <returns>result for inventory added successfully.</returns>
	[HttpPost]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddTimeSlot(InventoryRequestDto inventoryRequestDto)
	{
		return await _inventoryService.AddInventoryAsync(inventoryRequestDto);
	}
}
