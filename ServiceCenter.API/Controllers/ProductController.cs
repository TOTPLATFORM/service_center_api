using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ProductController(IProductService productService) : BaseController
{
    private readonly IProductService _productService = productService;

    /// <summary>
    /// action for add product  action that take  Product dto   
    /// </summary>
    /// <param name="productDto">product  dto</param>
 	/// <remarks>
    /// Access is limited to users with the "Admin,Manager,WarehouseManager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddProduct(ProductRequestDto productDto)
    {
        return await _productService.AddProductAsync(productDto);
    }
    /// <summary>
    /// retrieves all product  in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of product  to retrieve</param>
    ///<param name="index">index of product  to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all product .</returns> [HttpGet]

    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductGetByIdResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ProductGetByIdResponseDto>>> GetAllProducts(int itemCount, int index)
    {
        return await _productService.GetAllProductAsync(itemCount, index);
    }
    /// <summary>
     /// retrieves a product   by their unique identifier.
     /// </summary>
     /// <param name="id">the unique identifier of the product  .</param>
     /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product  category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProductGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ProductGetByIdResponseDto>> GetProductById(int id)
    {
        return await _productService.GetProductByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing product  by its ID.
    /// </summary>
    ///<param name="id">id of product.</param>
    ///<param name="productRequestDto">product dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,WareHouseManager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin ,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ProductGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ProductGetByIdResponseDto>> UpdateProduct(int id, ProductRequestDto productRequestDto)
    {
        return await _productService.UpdateProductAsync(id, productRequestDto);
    }

    /// <summary>
    /// deletes a product  from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the product  to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteProduct(int id)
    {
        return await _productService.DeleteProductAsync(id);
    }

    /// <summary>
    /// searches product   based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of product s to retrieve</param>
    ///<param name="index">index of product s to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product   that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductGetByIdResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ProductGetByIdResponseDto>>> SearchProductByText(string text, int itemCount, int index)
    {
        return await _productService.SearchProductByTextAsync(text, itemCount, index);
    }
    /// <summary>
    /// retrieves products by their product category unique identifier.
    /// </summary>
    ///<param name="categoryId">the unique identifier of the product category</param>  
    /// <param name = "itemCount" > item count of product  to retrieve</param>
    ///<param name="index">index of product  to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product category's products.</returns>

    [HttpGet("searchByProductCategory/{categoryId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductGetByIdResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ProductGetByIdResponseDto>>> SearchProductByProductCategory(int categoryId, int itemCount, int index)
    {
        return await _productService.GetProductsForProductCategoryAsync(categoryId,  itemCount,  index);
    }
}
