using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class WareHouseManagerController(IWareHouseManagerService wareHousManager) : BaseController
{
    private readonly IWareHouseManagerService _wareHousManager = wareHousManager;

    /// <summary>
    /// adds a new warehouse manager to the system.
    /// </summary>
    /// <param name="wareHouseManagerRequest">the data transfer object containing warehouse manager details for creation.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<Result> AddWareHouseManager(WareHouseManagerRequestDto wareHouseManagerRequest)
    {
        return await _wareHousManager.AddWareHouseManagerServiceAsync(wareHouseManagerRequest);
    }  /// <summary>
       /// retrieves all wareHouse manager in the system.
       /// </summary>
       /// <param name = "itemCount" > item count of wareHouse manager to retrieve</param>
       ///<param name="index">index of wareHouse manager to retrieve</param>
       /// <remarks>
       /// access is limited to users with the "Admin" role.
       /// </remarks>
       /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all wareHouse manager.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<WareHouseManagerResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<WareHouseManagerResponseDto>>> GetAllWareHouseManager(int itemCount, int index)
    {
        return await _wareHousManager.GetAllWareHouseManagerServicesAsync( itemCount,  index);
    }

    /// <summary>
    /// retrieves a wareHouse manager  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the wareHouse manager .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the wareHouse manager category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<WareHouseManagerGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<WareHouseManagerGetByIdResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<Result<WareHouseManagerGetByIdResponseDto>> GetWareHouseManagerById(string id)
    {
        return await _wareHousManager.GetWareHouseManagerServiceByIdAsync(id);
    }

    /// <summary>
    /// updates an existing warehouse manager   by its ID.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse manager  to update.</param>
    /// <param name="wareHouseManagerRequest">the data transfer object containing updated details for the warehouse manager.</param>
    /// <remarks>
    /// access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<WareHouseManagerGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<WareHouseManagerGetByIdResponseDto>> UpdateWareHouseManager(string id, WareHouseManagerRequestDto wareHouseManagerRequest)
    {
        return await _wareHousManager.UpdateWareHouseManagerServiceAsync(id, wareHouseManagerRequest);
    }


    /// <summary>
    /// searches wareHouse manager  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of wareHouse managers to retrieve</param>
    ///<param name="index">index of wareHouse managers to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of wareHouse manager  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<WareHouseManagerResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<WareHouseManagerResponseDto>>> SerachWarehouseManagerByText(string text, int itemCount, int index)
    {
        return await _wareHousManager.SearchWareHouseManagerByTextAsync(text,  itemCount,  index);
    }
}
