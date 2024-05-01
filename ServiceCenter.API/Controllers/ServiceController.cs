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
    //[Authorize(Roles = "Admin")]
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
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<ServiceResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
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
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServiceResponseDto>> GetServiceById(int id)
    {
        return await _ServiceService.GetServiceByIdAsync(id);
    }
}
