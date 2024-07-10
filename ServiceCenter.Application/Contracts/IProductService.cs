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
public interface IProductService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new product to the database.
	/// </summary>
	/// <param name="productRequestDto">the product data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the product addition.</returns>
	public Task<Result> AddProductAsync(ProductRequestDto productRequestDto);


	/// <summary>
	/// asynchronously retrieves all products in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of productes to retrieve</param>
	///<param name="index">index of productes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product response DTOs.</returns>
	public Task<Result<PaginationResult<ProductGetByIdResponseDto>>> GetAllProductAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a product by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the product to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product response DTO.</returns>
	public Task<Result<ProductGetByIdResponseDto>> GetProductByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing product.
	/// </summary>
	/// <param name="id">the unique identifier of the product to update.</param>
	/// <param name="productRequestDto">the product data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ProductGetByIdResponseDto>> UpdateProductAsync(int id, ProductRequestDto productRequestDto);

	/// <summary>
	/// asynchronously deletes a product from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the product to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteProductAsync(int id);

	/// <summary>
	/// asynchronously searches for products based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within product data.</param>
	/// <param name = "itemCount" > item count of productes to retrieve</param>
	///<param name="index">index of productes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ProductGetByIdResponseDto>>> SearchProductByTextAsync(string text, int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves all products in the system.
	/// </summary>
	/// /// <param name="text">the text to search within category data.</param>
	/// <param name = "itemCount" > item count of productes to retrieve</param>
	///<param name="index">index of productes to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product response DTOs.</returns>
	public Task<Result<PaginationResult<ProductGetByIdResponseDto>>> GetProductsForProductCategoryAsync(int categoryId, int itemCount, int index);
 
}