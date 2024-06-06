﻿using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for productCategory-related services that manages productCategory data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IProductCategoryService :IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  product category that take  ProductCategoryDto   
    /// </summary>
    /// <param name="productCategoryRequestDto">product category request dto</param>
    /// <returns> product category added successfully </returns>
    public Task<Result> AddProductCategoryAsync(ProductCategoryRequestDto productCategoryRequestDto);
    /// <summary>
    /// function to get all product category 
    /// </summary>
    /// <returns>list all Product category response dto </returns>
    public Task<Result<PaginationResult<ProductCategoryResponseDto>>> GetAllProductCategoryAsync(int itemCount, int index);
    /// <summary>
    /// function to get  product category by id that take   ProductCategory id
    /// </summary>
    /// <param name="id"> product category id</param>
    /// <returns> product category response dto</returns>
    public Task<Result<ProductCategoryResponseDto>> GetProductCategoryByIdAsync(int id);
    /// <summary>
    /// function to update Product category that take ProductCategoryRequestDto   
    /// </summary>
    /// <param name="id">ProductCategory id</param>
    /// <param name="productCategoryRequestDto">ProductCategory dto</param>
    /// <returns>Updated ProductCategory </returns>
    public Task<Result<ProductCategoryResponseDto>> UpdateProductCategoryAsync(int id, ProductCategoryRequestDto productCategoryRequestDto);
    /// <summary>
    /// function to delete product category that take product category id   
    /// </summary>
    /// <param name="id">product category id</param>
    /// <returns>product category removed successfully </returns>
    public Task<Result> DeleteProductCategoryAsync(int id);
    /// <summary>
    /// function to search by ProductCategory name  that take  ProductCategory name
    /// </summary>
    /// <param name="text">ProductCategory name</param>
    /// <returns>ProductCategory response dto </returns>
    public Task<Result<List<ProductCategoryResponseDto>>> SearchProductCategoryByTextAsync(string text);
    public Task<Result<List<ProductCategoryResponseDto>>> AssignProductCategoryToProductBrandAsync(int productCategoryId, int productBrandId);
}
