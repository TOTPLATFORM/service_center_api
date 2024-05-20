using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class ServicePackageController(IServicePackageService ServicePackageService) : BaseController
{
    private readonly IServicePackageService _ServicePackageService = ServicePackageService;

    /// <summary>
    /// action for add ServicePackage  action that take  ServicePackage dto   
    /// </summary>
    /// <param name="ServicePackageDto">ServicePackage  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for ServicePackage  added successfully.</returns>
    [HttpPost]
    //[Authorize(Roles = "Manager,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddServicePackage(ServicePackageRequestDto ServicePackageDto)
    {
        return await _ServicePackageService.AddServicePackageAsync(ServicePackageDto);
    }
    /// <summary>
    /// get all ServicePackage  in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    //[Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<List<ServicePackageResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<ServicePackageResponseDto>>> GetAllServicePackage()
    {
        return await _ServicePackageService.GetAllServicePackageAsync();
    }
    /// <summary>
    /// delete  ServicePackage  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    //[Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteServicePackageAsycn(int id)
    {
        return await _ServicePackageService.DeleteServicePackageAsync(id);
    }
    /// </summary>
    ///<param name="id">id of ServicePackage.</param>
    ///<param name="ServicePackageRequestDto">ServicePackage dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    //[Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<ServicePackageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServicePackageResponseDto>> UpdateServicePackage(int id, ServicePackageRequestDto ServicePackageRequestDto)
    {
        return await _ServicePackageService.UpdateServicePackageAsync(id, ServicePackageRequestDto);
    }
    /// <summary>
    /// get ServicePackage by id in the system.
    /// </summary>
    ///<param name="id">id of ServicePackage.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    //[Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<ServicePackageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServicePackageResponseDto>> GetServicePackageById(int id)
    {
        return await _ServicePackageService.GetServicePackageByIdAsync(id);
    }
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    //[Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(Result<ServicePackageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<List<ServicePackageResponseDto>>> SearchServicePackageByText(string text)
    {
        return await _ServicePackageService.SearchServicePackageByTextAsync(text);
    }
}
