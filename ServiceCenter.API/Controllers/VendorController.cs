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
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddVendor(VendorRequestDto vendorRequestDto)
    {
        return await _vendorService.AddVendorAsync(vendorRequestDto);
    }
    /// <summary>
    /// retrieves all vendor in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of vendor to retrieve</param>
    ///<param name="index">index of vendor to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all vendor.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<VendorResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<VendorResponseDto>>> GetAllVendor(int itemCount, int index)
    {
        return await _vendorService.GetAllVendorsAsync( itemCount,  index);
    }
    /// <summary>
    /// retrieves a vendor  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the vendor .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the vendor category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<VendorGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<VendorGetByIdResponseDto>> GetVendorById(string id)
    {
        return await _vendorService.GetVendorByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing vendor by its ID.
    /// </summary>
    ///<param name="id">id of vendor.</param>
    ///<param name="vendorRequestDto">vendor dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<VendorGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<VendorGetByIdResponseDto>> UpdateVendor(string id, VendorRequestDto vendorRequestDto)
    {
        return await _vendorService.UpdateVendorAsync(id, vendorRequestDto);
    }
    /// <summary>
    /// searches vendor  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of vendors to retrieve</param>
    ///<param name="index">index of vendors to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Managerr" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of vendor  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<VendorResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<VendorResponseDto>>> SerachVendorByText(string text, int itemCount, int index)
    {
        return await _vendorService.SearchVendorByTextAsync(text,  itemCount,  index);
    }
 
    }