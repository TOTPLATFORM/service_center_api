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
/// provides an interface for product-related services that manages product data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IProductBrandService : IApplicationService, IScopedService
{

	/// <summary>
	/// asynchronously adds a new product brand to the database.
	/// </summary>
	/// <param name="productBrandRequestDto">the product brand data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the product brand addition.</returns>
	public Task<Result> AddProductBrandAsync( ProductBrandRequestDto  productBrandRequestDto);

	/// <summary>
	/// asynchronously retrieves all product brands in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of product brandes to retrieve</param>
	///<param name="index">index of product brandes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product brand response DTOs.</returns>
	public Task<Result<PaginationResult<ProductBrandResponseDto>>> GetAllProductBrandAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a product brand by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the product brand to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product brand response DTO.</returns>
	public Task<Result<ProductBrandGetByIdResponseDto>> GetProductBrandByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing product brand.
	/// </summary>
	/// <param name="id">the unique identifier of the product brand to update.</param>
	/// <param name="ProductBrandRequestDto">the product brand data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ProductBrandResponseDto>> UpdateProductBrandAsync(int id, ProductBrandRequestDto ProductBrandRequestDto);

	/// <summary>
	/// asynchronously deletes a product brand from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the product brand to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteProductBrandAsync(int id);

	/// <summary>
	/// asynchronously searches for product brands based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within product brand data.</param>
	/// <param name = "itemCount" > item count of product brandes to retrieve</param>
	///<param name="index">index of product brandes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product brand response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ProductBrandResponseDto>>> SearchProductBrandByTextAsync(string text,int itemCount,int index);

	/// <summary>
	/// asynchronously updates the data of an existing inventory.
	/// </summary>
	/// <param name="id">the unique identifier of the inventory to update.</param>
	/// <param name="productBrandId">the product category data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<List<ProductBrandResponseDto>>> AssignProductBrandToInventoryAsync(int inventoryId, int productBrandId);
}
