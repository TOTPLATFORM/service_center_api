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
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result for product  added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddProduct(ProductRequestDto productDto)
    {
        return await _productService.AddProductAsync(productDto);
    }
    /// <summary>
    /// get all product categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ProductResponseDto>>> GetAllProduct(int itemCount, int index)
    {
        return await _productService.GetAllProductAsync(itemCount, index);
    }
  
    /// <summary>
    /// get product by id in the system.
    /// </summary>
    ///<param name="id">id of Product.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProductResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ProductResponseDto>> GetProductById(int id)
    {
        return await _productService.GetProductByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Product.</param>
    ///<param name="ProductRequestDto">Product dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin ,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ProductResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ProductResponseDto>> UpdateProduct(int id, ProductRequestDto ProductRequestDto)
    {
        return await _productService.UpdateProductAsync(id, ProductRequestDto);
    }
    /// <summary>
    /// delete  Product  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteProduct(int id)
    {
        return await _productService.DeleteProductAsync(id);
    }

    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ProductResponseDto>>> SearchProductByText(string text, int itemCount, int index)
    {
        return await _productService.SearchProductByTextAsync(text, itemCount, index);
    }
    [HttpGet("searchByProductCategory/{categoryId}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ProductResponseDto>>> SearchProductByProductCategory(int categoryId, int itemCount, int index)
    {
        return await _productService.GetProductsForProductCategoryAsync(categoryId,  itemCount,  index);
    }
  

   

    //[HttpGet("searchByProductBrand/{brandId}")]
    //[Authorize(Roles = "Manager")]
    //[ProducesResponseType(typeof(Result<ProductResponseDto>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    //public async Task<Result<List<ProductResponseDto>>> SearchProductByProductBrand(int brandId)
    //{
    //    return await _productService.GetProductsForProductBrandAsync(brandId);
    //}
}
