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
    /// Access is limited to users with the "Admin,Manager" role.
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
    /// retrieves all service provider in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of service provider to retrieve</param>
    ///<param name="index">index of service provider to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all service provider.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<List<ServiceProviderResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ServiceProviderResponseDto>>> GetAllServiceProvider(int itemCount, int index)
    {
        return await _serviceproviderService.GetAllServiceProviderAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a service provider  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the service provider .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,ServiceProvider" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service provider category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [Authorize(Roles = "Manager,ServiceProvider,Admin")]
    [ProducesResponseType(typeof(Result<ServiceProviderGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServiceProviderGetByIdResponseDto>> GetServiceProviderById(string id)
    {
        return await _serviceproviderService.GetServiceProviderByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing service provider by its ID.
    /// </summary>
    ///<param name="id">id of serviceprovider.</param>
    ///<param name="serviceproviderRequestDto">serviceprovider dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<ServiceProviderGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServiceProviderGetByIdResponseDto>> UpdateServiceProvider(string id, ServiceProviderRequestDto serviceproviderRequestDto)
    {
        return await _serviceproviderService.UpdateServiceProviderAsync(id, serviceproviderRequestDto);
    }
    /// <summary>
    /// searches service provider  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of service providers to retrieve</param>
    ///<param name="index">index of service providers to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service provider  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<ServiceProviderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ServiceProviderResponseDto>>> SerachServiceProviderByText(string text, int itemCount, int index)
    {
        return await _serviceproviderService.SearchServiceProviderByTextAsync(text,itemCount,index);
    }

}
