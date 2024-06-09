using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class WareHouseManagerController(IWareHousManagerService wareHousManager) : BaseController
{
    private readonly IWareHousManagerService _wareHousManager = wareHousManager;

    /// <summary>
    /// adds a new warehouse manager to the system.
    /// </summary>
    /// <param name="wareHouseManagerRequest">the data transfer object containing warehouse manager details for creation.</param>
    /// <remarks>
    /// access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<Result> AddWareHouseManager(WareHouseManagerRequestDto wareHouseManagerRequest)
    {
        return await _wareHousManager.AddWareHouseManagerServiceAsync(wareHouseManagerRequest);
    }

    /// <summary>
    /// retrieves all warehouse manager in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all warehouse manager.</returns>
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<WareHouseManagerResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<WareHouseManagerResponseDto>>> GetAllWareHouseManager(int itemcount, int index)
    {
        return await _wareHousManager.GetAllWareHouseManagerServicesAsync( itemcount,  index);
    }

    /// <summary>
    /// retrieves a warehouse manager  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse manager .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the warehouse manager category details.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<WareHouseManagerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<WareHouseManagerResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<Result<WareHouseManagerResponseDto>> GetWareHouseManagerById(string id)
    {
        return await _wareHousManager.GetWareHouseManagerServiceByIdAsync(id);
    }

    /// <summary>
    /// updates an existing warehouse manager's information.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse manager  to update.</param>
    /// <param name="wareHouseManagerRequest">the data transfer object containing updated details for the warehouse manager.</param>
    /// <remarks>
    /// access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<WareHouseManagerResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<WareHouseManagerResponseDto>> UpdateWareHouseManager(string id, WareHouseManagerRequestDto wareHouseManagerRequest)
    {
        return await _wareHousManager.UpdateWareHouseManagerServiceAsync(id, wareHouseManagerRequest);
    }

    /// <summary>
    /// deletes a warehouse manager  from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse manager  to delete.</param>
    /// <remarks>
    /// access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteWareHouseManager(string id)
    {
        return await _wareHousManager.DeleteWareHouseManagerServiceAsync(id);
    }

    /// <summary>
    /// search  warehouse manager by text in the system.
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Manager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<PaginationResult<WareHouseManagerResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<WareHouseManagerResponseDto>>> SerachWarehouseManagerByText(string text, int itemcount, int index)
    {
        return await _wareHousManager.SearchWareHouseManagerByTextAsync(text,  itemcount,  index);
    }
}
