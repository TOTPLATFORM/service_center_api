using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddServicePackage(ServicePackageRequestDto ServicePackageDto)
    {
        return await _ServicePackageService.AddServicePackageAsync(ServicePackageDto);
    }

}
