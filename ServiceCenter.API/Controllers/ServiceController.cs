using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
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
    /// <returns>result for Service  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddService(ServiceRequestDto ServiceDto)
    {
        return await _ServiceService.AddServiceAsync(ServiceDto);
    }
    /// <summary>
    /// get all Service  in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<List<ServiceResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<ServiceResponseDto>>> GetAllService()
    {
        return await _ServiceService.GetAllServiceAsync();
    }
    /// <summary>
    /// get Service by id in the system.
    /// </summary>
    ///<param name="id">id of Service.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServiceGetByIdResponseDto>> GetServiceById(int id)
    {
        return await _ServiceService.GetServiceByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Service.</param>
    ///<param name="ServiceRequestDto">Service dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServiceResponseDto>> UpdateService(int id, ServiceRequestDto ServiceRequestDto)
    {
        return await _ServiceService.UpdateServiceAsync(id, ServiceRequestDto);
    }
    /// <summary>
    /// delete  Service  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteServiceAsycn(int id)
    {
        return await _ServiceService.DeleteServiceAsync(id);
    }
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<ServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<List<ServiceResponseDto>>> SearchServiceByText(string text)
    {
        return await _ServiceService.SearchServiceByTextAsync(text);
    }

	/// </summary>
	///<param name="id">id of Service.</param>
	///<param name="ServiceRequestDto">Service dto.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("assign/{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<ServiceResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<ServiceResponseDto>>> AssignServiceToPacakge(int serviceId , int servicePackageId)
	{
		return await _ServiceService.AssignServiceToPackagesAsync(serviceId , servicePackageId);
	}

	[HttpGet("search/ByPackage/{servicePackageId}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<ServiceResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<ServiceResponseDto>>> GetServicesByPacakge(int servicePackageId)
	{
		return await _ServiceService.GetServicesByPackageAsync( servicePackageId);
	}
}
