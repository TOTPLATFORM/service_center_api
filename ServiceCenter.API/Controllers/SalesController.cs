using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class SalesController(ISalesService salesService) : BaseController
{
    private readonly ISalesService _salesService = salesService;

    /// <summary>
    /// Adds a new sales to the system.
    /// </summary>
    /// <param name="salesRequestDto">The data transfer object containing sales details for creation.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPost]
  [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSales(SalesRequestDto salesRequestDto)
    {
        return await _salesService.AddSalesAsync(salesRequestDto);
    }

    /// <summary>
    /// get all sales in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
  [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<List<SalesResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<SalesResponseDto>>> GetAllSales()
    {
        return await _salesService.GetAllSalesAsync();
    }
    /// <summary>
    /// get all sales in the system.
    /// </summary>
    ///<param name="id">id of sales.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Sales")]
    [ProducesResponseType(typeof(Result<SalesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<SalesResponseDto>> GetSalesById(string id)
    {
        return await _salesService.GetSalesByIdAsync(id);
    }

    /// <summary>
    /// get  sales by id in the system.
    /// </summary>
    ///<param name="id">id of sales.</param>
    ///<param name="salesRequestDto">sales dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Sales")]
    [ProducesResponseType(typeof(Result<SalesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SalesResponseDto>> UpdateSales(string id, SalesRequestDto salesRequestDto)
    {
        return await _salesService.UpdateSalesAsync(id, salesRequestDto);
    }
    /// <summary>
    /// search  sales by text in the system.
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
  [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<SalesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<List<SalesResponseDto>>> SerachSalesByText(string text)
    {
        return await _salesService.SearchSalesByTextAsync(text);
    }
    /// <summary>
    /// delete  sales by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
  [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteSalesAsycn(string id)
    {
        return await _salesService.DeleteSalesAsync(id);
    }
}
