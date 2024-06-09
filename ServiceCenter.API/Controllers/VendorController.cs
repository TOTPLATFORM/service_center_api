using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class VendorController(IVendorService vendorService) : BaseController
{
    private readonly IVendorService _vendorService = vendorService;

    /// <summary>
    /// Adds a new vendor to the system.
    /// </summary>
    /// <param name="vendorRequestDto">The data transfer object containing vendor details for creation.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddVendor(VendorRequestDto vendorRequestDto)
    {
        return await _vendorService.AddVendorAsync(vendorRequestDto);
    }

    /// <summary>
    /// get all vendor in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<VendorResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<VendorResponseDto>>> GetAllVendor(int itemcount, int index)
    {
        return await _vendorService.GetAllVendorsAsync( itemcount,  index);
    }
    /// <summary>
    /// get all vendor in the system.
    /// </summary>
    ///<param name="id">id of vendor.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<VendorResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<VendorResponseDto>> GetVendorById(string id)
    {
        return await _vendorService.GetVendorByIdAsync(id);
    }

    /// <summary>
    /// get  vendor by id in the system.
    /// </summary>
    ///<param name="id">id of vendor.</param>
    ///<param name="vendorRequestDto">vendor dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<VendorResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<VendorResponseDto>> UpdateVendor(string id, VendorRequestDto vendorRequestDto)
    {
        return await _vendorService.UpdateVendorAsync(id, vendorRequestDto);
    }
    /// <summary>
    /// search  vendor by text in the system.
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<VendorResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<VendorResponseDto>>> SerachVendorByText(string text, int itemcount, int index)
    {
        return await _vendorService.SearchVendorByTextAsync(text,  itemcount,  index);
    }
    /// <summary>
    /// delete  vendor by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteVendorAsycn(string id)
    {
        return await _vendorService.DeleteVendorAsync(id);
    }
}