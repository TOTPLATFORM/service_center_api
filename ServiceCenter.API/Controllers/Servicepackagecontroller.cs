using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
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
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
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
    [ProducesResponseType(typeof(Result<PaginationResult<ServicePackageResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ServicePackageResponseDto>>> GetAllServicePackage(int itemCont,int index)
    {
        return await _ServicePackageService.GetAllServicePackageAsync(itemCont,index);
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
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteServicePackageAsycn(int id)
    {
        return await _ServicePackageService.DeleteServicePackageAsync(id);
    }  
    /// <summary>
    /// Updates an existing service package by its ID.
    /// </summary>
    ///<param name="id">id of ServicePackage.</param>
    ///<param name="ServicePackageRequestDto">ServicePackage dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Manager,Admin")]
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
    [ProducesResponseType(typeof(Result<ServicePackageGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServicePackageGetByIdResponseDto>> GetServicePackageById(int id)
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
    [ProducesResponseType(typeof(Result<ServicePackageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ServicePackageResponseDto>>> SearchServicePackageByText(string text,int itemCount,int index)
    {
        return await _ServicePackageService.SearchServicePackageByTextAsync(text,itemCount,index );
    }
}
