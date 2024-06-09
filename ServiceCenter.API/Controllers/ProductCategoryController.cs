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
    /// <returns>result for product category added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddProductCategory(ProductCategoryRequestDto productCategoryDto)
    {
        return await _productCategoryService.AddProductCategoryAsync(productCategoryDto);
    }
    /// <summary>
    /// get all product categories in the system.
    /// </summary>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductCategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ProductCategoryResponseDto>>> GetAllProductCategories(int itemCount, int index)
    {
        return await _productCategoryService.GetAllProductCategoryAsync(itemCount, index);
    }
  
    /// <summary>
    /// get all inventories in the system.
    /// </summary>
    ///<param name="id">id of ProductCategory.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProductCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ProductCategoryResponseDto>> GetProductCategoryById(int id)
    {
        return await _productCategoryService.GetProductCategoryByIdAsync(id);
    }
    /// <summary>
    /// get  ProductCategory by id in the system.
    /// </summary>
    ///<param name="id">id of ProductCategory.</param>
    ///<param name="ProductCategoryRequestDto">ProductCategory dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ProductCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ProductCategoryResponseDto>> UpdateProductCategory(int id, ProductCategoryRequestDto ProductCategoryRequestDto)
    {
        return await _productCategoryService.UpdateProductCategoryAsync(id, ProductCategoryRequestDto);
    }
    /// <summary>
    /// delete  Product category by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager ,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteProductCategory(int id)
    {
        return await _productCategoryService.DeleteProductCategoryAsync(id);
    }
    /// </summary>
    ///<param name="text">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<ProductCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ProductCategoryResponseDto>>> SearchProductCategoryByText(string text)
    {
        return await _productCategoryService.SearchProductCategoryByTextAsync(text);
    }
    [HttpGet("assign")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<List<ProductCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ProductCategoryResponseDto>>> AssignProductCategoryToProductBrand(int productCategory, int productBrandId)
    {
        return await _productCategoryService.AssignProductCategoryToProductBrandAsync(productCategory, productBrandId);
    }
}
