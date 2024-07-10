using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddSales(SalesRequestDto salesRequestDto)
    {
        return await _salesService.AddSalesAsync(salesRequestDto);
    }
    /// <summary>
    /// retrieves all sales in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of sales to retrieve</param>
    ///<param name="index">index of sales to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all sales.</returns> [HttpGet]

    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<List<SalesResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<SalesResponseDto>>> GetAllSales(int itemCount, int index)
    {
        return await _salesService.GetAllSalesAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a sales  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the sales .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the sales category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Sales,Admin")]
    [ProducesResponseType(typeof(Result<SalesGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<SalesGetByIdResponseDto>> GetSalesById(string id)
    {
        return await _salesService.GetSalesByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing sales by its ID.
    /// </summary>
    ///<param name="id">id of sales.</param>
    ///<param name="salesRequestDto">sales dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<SalesGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<SalesGetByIdResponseDto>> UpdateSales(string id, SalesRequestDto salesRequestDto)
    {
        return await _salesService.UpdateSalesAsync(id, salesRequestDto);
    }
    /// <summary>
    /// searches sales  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of saless to retrieve</param>
    ///<param name="index">index of saless to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of sales  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<SalesResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<SalesResponseDto>>> SerachSalesByText(string text, int itemCount, int index)
    {
        return await _salesService.SearchSalesByTextAsync(text,itemCount,index);
    }
   
}
