﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
}
