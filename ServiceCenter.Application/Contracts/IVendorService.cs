using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for vendor-related services that manages vendor data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IVendorService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add vendor that take vendorDto   
    /// </summary>
    /// <param name="vendorRequestDto">vendor request dto</param>
    /// <returns>Vendor added successfully </returns>
    public Task<Result> AddVendorAsync(VendorRequestDto vendorRequestDto);

    /// <summary>
    /// function to get all vendor 
    /// </summary>
    /// <returns>list all vendorResponseDto </returns>
    public Task<Result<PaginationResult<VendorResponseDto>>> GetAllVendorsAsync(int itemcount, int index);

    /// <summary>
    /// function to get vendor by id that take  vendor id
    /// </summary>
    /// <param name="id">vendor id</param>
    /// <returns>vendor response dto</returns>
    public Task<Result<VendorResponseDto>> GetVendorByIdAsync(string id);

    /// <summary>
    /// function to update vendor that take VendorRequestDto   
    /// </summary>
    /// <param name="id">vendor id</param>
    /// <param name="vendorRequestDto">vendor dto</param>
    /// <returns>Updated Vendor </returns>
    public Task<Result<VendorResponseDto>> UpdateVendorAsync(string id, VendorRequestDto vendorRequestDto);


    /// <summary>
    /// function to search vendor by text  that take text   
    /// </summary>
    /// <param name="text">text</param>
    /// <returns>all vendores that contain this text </returns>
    public Task<Result<PaginationResult<VendorResponseDto>>> SearchVendorByTextAsync(string text, int itemcount, int index);

    
}
