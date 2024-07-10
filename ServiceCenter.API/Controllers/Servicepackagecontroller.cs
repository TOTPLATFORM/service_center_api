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
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>  
    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddServicePackage(ServicePackageRequestDto ServicePackageDto)
    {
        return await _ServicePackageService.AddServicePackageAsync(ServicePackageDto);
    }
    /// <summary>
    /// retrieves all service package in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of service package to retrieve</param>
    ///<param name="index">index of service package to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all service package.</returns> [HttpGet]
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ServicePackageResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ServicePackageResponseDto>>> GetAllServicePackage(int itemCount,int index)
    {
        return await _ServicePackageService.GetAllServicePackageAsync(itemCount,index);
    }

    /// <summary>
    /// deletes a service package from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the service package to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
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
    /// retrieves a service package  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the service package .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service package category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ServicePackageGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServicePackageGetByIdResponseDto>> GetServicePackageById(int id)
    {
        return await _ServicePackageService.GetServicePackageByIdAsync(id);
    }
    /// <summary>
    /// searches service package  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of service packages to retrieve</param>
    ///<param name="index">index of service packages to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service package  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<ServicePackageResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ServicePackageResponseDto>>> SearchServicePackageByText(string text,int itemCount,int index)
    {
        return await _ServicePackageService.SearchServicePackageByTextAsync(text,itemCount,index );
    }
}
