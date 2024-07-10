using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;
public class ServiceController(IServiceService ServiceService) : BaseController
{
	private readonly IServiceService _ServiceService = ServiceService;

    /// <summary>
    /// action for add Service  action that take  Service dto   
    /// </summary>
    /// <param name="ServiceDto">Service  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddService(ServiceRequestDto ServiceDto)
	{
		return await _ServiceService.AddServiceAsync(ServiceDto);
	}
    /// <summary>
    /// retrieves all service in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of service to retrieve</param>
    ///<param name="index">index of service to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all service.</returns> [HttpGet]
    [HttpGet]
	[ProducesResponseType(typeof(Result<PaginationResult<ServiceResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<ServiceResponseDto>>> GetAllService(int itemCount , int index)
	{
		return await _ServiceService.GetAllServiceAsync(itemCount,index);
	}
    /// <summary>
    /// retrieves a service  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the service .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
	[ProducesResponseType(typeof(Result<ServiceGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<ServiceGetByIdResponseDto>> GetServiceById(int id)
	{
		return await _ServiceService.GetServiceByIdAsync(id);
	}
    /// <summary>
    /// Updates an existing service by its ID.
    /// </summary>
    ///<param name="id">id of Service.</param>
    ///<param name="ServiceRequestDto">Service dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<ServiceResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<ServiceResponseDto>> UpdateService(int id, ServiceRequestDto ServiceRequestDto)
	{
		return await _ServiceService.UpdateServiceAsync(id, ServiceRequestDto);
	}

    /// <summary>
    /// deletes a service from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the service to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result> DeleteServiceAsycn(int id)
	{
		return await _ServiceService.DeleteServiceAsync(id);
	}
    /// <summary>
    /// searches service  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of services to retrieve</param>
    ///<param name="index">index of services to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
	[ProducesResponseType(typeof(Result<PaginationResult<ServiceResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<PaginationResult<ServiceResponseDto>>> SearchServiceByText(string text, int itemCount, int index)
	{
		return await _ServiceService.SearchServiceByTextAsync(text,itemCount,index);
	}

    /// <summary>
    /// assigns a service to service package.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="serviceId">the unique identifier of the service to assign.</param>
    /// <param name="servicePackageId">the unique identifier of the service package to assign.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>


    [HttpGet("assign")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<ServiceGetByIdResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<ServiceGetByIdResponseDto>>> AssignServiceToPacakge(int serviceId, int servicePackageId)
	{
		return await _ServiceService.AssignServiceToPackagesAsync(serviceId, servicePackageId);
	}
    /// <summary>
    /// retrieves services by their service package unique identifier.
    /// </summary>
    ///<param name="servicePackageId">the unique identifier of the service package</param>  
    /// <param name = "itemCount" > item count of service to retrieve</param>
    ///<param name="index">index of service to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service package's services.</returns>

    [HttpGet("search/ByPackage/{servicePackageId}")]
	[ProducesResponseType(typeof(Result<PaginationResult<ServiceResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<ServiceResponseDto>>> GetServicesByPacakge(int servicePackageId, int itemCount, int index)
	{
		return await _ServiceService.GetServicesByPackageAsync(servicePackageId,itemCount,index);
	}
    /// <summary>
    /// retrieves services by their service category unique identifier.
    /// </summary>
    ///<param name="serviceCategoryId">the unique identifier of the service category</param>  
    /// <param name = "itemCount" > item count of service to retrieve</param>
    ///<param name="index">index of service to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service category's services.</returns>

    [HttpGet("search/ByCategory/{serviceCategoryId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ServiceResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ServiceResponseDto>>> GetServicesByCategory(int serviceCategoryId, int itemCount, int index)
    {
        return await _ServiceService.GetServicesByCategoryAsync(serviceCategoryId,itemCount,index);
    }
}
