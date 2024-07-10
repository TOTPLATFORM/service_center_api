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
	/// asynchronously adds a new vendor to the database.
	/// </summary>
	/// <param name="vendorRequestDto">the vendor data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the vendor addition.</returns>
	public Task<Result> AddVendorAsync(VendorRequestDto vendorRequestDto);
	/// <summary>
	/// asynchronously retrieves all vendors in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of vendors to retrieve</param>
	///<param name="index">index of vendors to retrieve</param>
	///	<returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of vendor response DTOs.</returns>

	public Task<Result<PaginationResult<VendorResponseDto>>> GetAllVendorsAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a vendor by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the vendor to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the vendor response DTO.</returns>
	public Task<Result<VendorGetByIdResponseDto>> GetVendorByIdAsync(string id);

	/// <summary>
	/// asynchronously updates the data of an existing vendor.
	/// </summary>
	/// <param name="id">the unique identifier of the vendor to update.</param>
	/// <param name="vendorRequestDto">the vendor data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<VendorGetByIdResponseDto>> UpdateVendorAsync(string id, VendorRequestDto vendorRequestDto);

	/// <summary>
	/// asynchronously searches for vendors based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within vendor data.</param>
	/// /// <param name = "itemCount" > item count of depatments to retrieve</param>
	///<param name="index">index of vendors to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of vendor response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<VendorResponseDto>>> SearchVendorByTextAsync(string text, int itemcount, int index);

    
}
