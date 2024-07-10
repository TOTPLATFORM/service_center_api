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
/// provides an interface for product category-related services that manages product category data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IProductCategoryService :IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new product category to the database.
	/// </summary>
	/// <param name="productCategoryRequestDto">the product category data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the product category addition.</returns>
	public Task<Result> AddProductCategoryAsync(ProductCategoryRequestDto productCategoryRequestDto);

	/// <summary>
	/// asynchronously retrieves all product categorys in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of product categoryes to retrieve</param>
	///<param name="index">index of product categoryes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product category response DTOs.</returns>
	public Task<Result<PaginationResult<ProductCategoryResponseDto>>> GetAllProductCategoryAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a product category by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the product category to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product category response DTO.</returns>
	public Task<Result<ProductCategoryResponseDto>> GetProductCategoryByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing product category.
	/// </summary>
	/// <param name="id">the unique identifier of the product category to update.</param>
	/// <param name="productCategoryRequestDto">the product category data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ProductCategoryResponseDto>> UpdateProductCategoryAsync(int id, ProductCategoryRequestDto productCategoryRequestDto);

	/// <summary>
	/// asynchronously deletes a product category from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the product category to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteProductCategoryAsync(int id);

	/// <summary>
	/// asynchronously searches for product categorys based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within product category data.</param>
	/// <param name = "itemCount" > item count of product categoryes to retrieve</param>
	///<param name="index">index of product categoryes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product category response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ProductCategoryResponseDto>>> SearchProductCategoryByTextAsync(string text,int itemCount,int index);

	/// <summary>
	/// asynchronously updates the data of an existing product category.
	/// </summary>
	/// <param name="productCategoryId">the unique identifier of the product category to update.</param>
	/// <param name="productBrandId">the unique identifier of the product brand to update.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<List<ProductCategoryResponseDto>>> AssignProductCategoryToProductBrandAsync(int productCategoryId, int productBrandId);
}
