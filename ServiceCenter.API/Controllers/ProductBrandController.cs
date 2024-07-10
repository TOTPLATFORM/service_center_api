using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;


public class ProductBrandController(IProductBrandService productBrandService) : BaseController
{
    private readonly IProductBrandService _productBrandService = productBrandService;

    /// <summary>
    /// action for add product brand action that take  productBrand dto   
    /// </summary>
    /// <param name="productBrandDto">product brand dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,WarehouseManager,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddProductBrand(ProductBrandRequestDto productBrandDto)
    {
        return await _productBrandService.AddProductBrandAsync(productBrandDto);
    }
    /// <summary>
    /// retrieves all product brand in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of product brand to retrieve</param>
    ///<param name="index">index of product brand to retrieve</param>    
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all product brand.</returns> [HttpGet]
    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductBrandResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ProductBrandResponseDto>>> GetAllProductBrands(int itemCount, int index)
    {
        return await _productBrandService.GetAllProductBrandAsync(itemCount, index);
    }

    /// <summary>
    /// retrieves a product brand  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the product brand .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product brand category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ProductBrandGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ProductBrandGetByIdResponseDto>> GetProductBrandById(int id)
    {
        return await _productBrandService.GetProductBrandByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing product brand  by its ID.
    /// </summary>
    ///<param name="id">id of product brand.</param>
    ///<param name="productBrandDto">Product brand dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,WareHouseManager" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<ProductBrandResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ProductBrandResponseDto>> UpdateProductBrand(int id, ProductBrandRequestDto productBrandDto)
    {
        return await _productBrandService.UpdateProductBrandAsync(id, productBrandDto);
    }

    /// <summary>
    /// deletes a product brand from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the product brand to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteProductBrand(int id)
    {
        return await _productBrandService.DeleteProductBrandAsync(id);
    }
    /// <summary>
    /// searches product brand  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of product brands to retrieve</param>
    ///<param name="index">index of product brands to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of product brand  that match the search criteria.</returns>

    [HttpGet("search /{text}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ProductBrandResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ProductBrandResponseDto>>> SearchProductBrandByTextAsync(string text,int itemCount,int index)
    {
        return await _productBrandService.SearchProductBrandByTextAsync(text,itemCount,index);
    }
    /// <summary>
    /// assigns a product brand to inventory.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager,WarehouseManager" role.
    /// </remarks>
    /// <param name="productBrandId">the unique identifier of the product brand to assign.</param>
    /// <param name="inventoryId">the unique identifier of the inventory  to assign.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpGet("assign")]
    [Authorize(Roles = "Admin,Manager,WarehouseManager")]
    [ProducesResponseType(typeof(Result<List<ProductBrandResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<ProductBrandResponseDto>>> AssignProductBrandToInventory(int inventoryId, int productBrandId)
    {
        return await _productBrandService.AssignProductBrandToInventoryAsync(inventoryId, productBrandId);
    }
}
