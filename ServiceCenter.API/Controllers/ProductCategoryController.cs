using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class ProductCategoryController(IProductCategoryService productCategoryService) : BaseController
{
    private readonly IProductCategoryService _productCategoryService = productCategoryService;

    /// <summary>
    /// action for add product category action that take  ProductCategory dto   
    /// </summary>
    /// <param name="productCategoryDto">product category dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddProductCategory(ProductCategoryRequestDto productCategoryDto)
    {
        return await _productCategoryService.AddProductCategoryAsync(productCategoryDto);
    }
    /// <summary>
    /// retrieves all product category in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of product category to retrieve</param>
    ///<param name="index">index of product category to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all product category.</returns> [HttpGet]
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductCategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ProductCategoryResponseDto>>> GetAllProductCategories(int itemCount, int index)
    {
        return await _productCategoryService.GetAllProductCategoryAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves a product category  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the product category .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product category category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProductCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ProductCategoryResponseDto>> GetProductCategoryById(int id)
    {
        return await _productCategoryService.GetProductCategoryByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing product category by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the product category to update.</param>
    /// <param name="productCategoryRequestDto">The DTO representing the updated product category.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>]
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ProductCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ProductCategoryResponseDto>> UpdateProductCategory(int id, ProductCategoryRequestDto productCategoryRequestDto)
    {
        return await _productCategoryService.UpdateProductCategoryAsync(id, productCategoryRequestDto);
    }

    /// <summary>
    /// deletes a product category from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the product category to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager ,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteProductCategory(int id)
    {
        return await _productCategoryService.DeleteProductCategoryAsync(id);
    }
    /// <summary>
    /// searches product category  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of product categorys to retrieve</param>
    ///<param name="index">index of product categorys to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product category  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<PaginationResult<ProductCategoryResponseDto>>> SearchProductCategoryByText(string text,int itemCount,int index)
    {
        return await _productCategoryService.SearchProductCategoryByTextAsync(text,itemCount,index );
    }
    /// <summary>
    /// assigns a product category to product brand.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager,WarehouseManager" role.
    /// </remarks>
    /// <param name="productBrandId">the unique identifier of the product brand to assign.</param>
    /// <param name="productCategory">the unique identifier of the product category to assign.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("assign")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<List<ProductCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ProductCategoryResponseDto>>> AssignProductCategoryToProductBrand(int productCategory, int productBrandId)
    {
        return await _productCategoryService.AssignProductCategoryToProductBrandAsync(productCategory, productBrandId);
    }
}
