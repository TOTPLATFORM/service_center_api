using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ServiceProviderController(IServiceProviderService serviceproviderService) : BaseController
{
    private readonly IServiceProviderService _serviceproviderService = serviceproviderService;

    /// <summary>
    /// Adds a new serviceprovider to the system.
    /// </summary>
    /// <param name="serviceproviderRequestDto">The data transfer object containing serviceprovider details for creation.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddServiceProvider(ServiceProviderRequestDto serviceproviderRequestDto)
    {
        return await _serviceproviderService.AddServiceProviderAsync(serviceproviderRequestDto);
    }

    /// <summary>
    /// get all serviceprovider in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<List<ServiceProviderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ServiceProviderResponseDto>>> GetAllServiceProvider(int itemCount, int index)
    {
        return await _serviceproviderService.GetAllServiceProviderAsync(itemCount,index);
    }
    /// <summary>
    /// get all serviceprovider in the system.
    /// </summary>
    ///<param name="id">id of serviceprovider.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,ServiceProvider,Admin")]
    [ProducesResponseType(typeof(Result<ServiceProviderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServiceProviderResponseDto>> GetServiceProviderById(string id)
    {
        return await _serviceproviderService.GetServiceProviderByIdAsync(id);
    }

    /// <summary>
    /// get  serviceprovider by id in the system.
    /// </summary>
    ///<param name="id">id of serviceprovider.</param>
    ///<param name="serviceproviderRequestDto">serviceprovider dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<ServiceProviderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServiceProviderResponseDto>> UpdateServiceProvider(string id, ServiceProviderRequestDto serviceproviderRequestDto)
    {
        return await _serviceproviderService.UpdateServiceProviderAsync(id, serviceproviderRequestDto);
    }
    /// <summary>
    /// search  serviceprovider by text in the system.
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<ServiceProviderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ServiceProviderResponseDto>>> SerachServiceProviderByText(string text, int itemCount, int index)
    {
        return await _serviceproviderService.SearchServiceProviderByTextAsync(text,itemCount,index);
    }
    /// <summary>
    /// delete  serviceprovider by id from the system.
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
    public async Task<Result> DeleteServiceProviderAsycn(string id)
    {
        return await _serviceproviderService.DeleteServiceProviderAsync(id);
    }
}
