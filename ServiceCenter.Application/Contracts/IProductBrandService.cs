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
    /// function to add  product brand that take  productBrandDto   
    /// </summary>
    /// <param name=" productBrandRequestDto">product brand request dto</param>
    /// <returns> product brand added successfully </returns>
    public Task<Result> AddProductBrandAsync( ProductBrandRequestDto  productBrandRequestDto);
    /// <summary>
		/// function to get all productBrand 
		/// </summary>
		/// <returns>list all product brand response dto </returns>
		public Task<Result<PaginationResult<ProductBrandResponseDto>>> GetAllProductBrandAsync(int itemCount, int index);

    /// <summary>
    /// function to get  product brand by id that take   product brand id
    /// </summary>
    /// <param name="id"> product brand id</param>
    /// <returns> product brand response dto</returns>
    public Task<Result<ProductBrandResponseDto>> GetProductBrandByIdAsync(int id);
    /// <summary>
		/// function to update product brand that take ProductBrandRequestDto   
		/// </summary>
		/// <param name="id">product brand id</param>
		/// <param name="ProductBrandRequestDto">product brand dto</param>
		/// <returns>Updated product brand </returns>
		public Task<Result<ProductBrandResponseDto>> UpdateProductBrandAsync(int id, ProductBrandRequestDto ProductBrandRequestDto);
    /// <summary>
    /// function to delete ProductBrand that take product brand id   
    /// </summary>
    /// <param name="id">product brand id</param>
    /// <returns>ProductBrand removed successfully </returns>
    public Task<Result> DeleteProductBrandAsync(int id);
    /// <summary>
    /// function to search by ProductBrand name  that take  ProductBrand name
    /// </summary>
    /// <param name="text">ProductBrand name</param>
    /// <returns>ProductBrand response dto </returns>
    public  Task<Result<List<ProductBrandResponseDto>>> SearchProductBrandByTextAsync(string text);
    public Task<Result<List<ProductBrandResponseDto>>> AssignProductBrandToInventoryAsync(int inventoryId, int productBrandId);
}
