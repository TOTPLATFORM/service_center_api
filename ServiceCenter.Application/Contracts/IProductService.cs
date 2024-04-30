using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IProductService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  product  that take  ProductDto   
    /// </summary>
    /// <param name="productRequestDto">product  request dto</param>
    /// <returns> product  added successfully </returns>
    public Task<Result> AddProductAsync(ProductRequestDto productRequestDto);
    /// <summary>
    /// function to get all product  
    /// </summary>
    /// <returns>list all Product  response dto </returns>
    public Task<Result<List<ProductResponseDto>>> GetAllProductAsync();
    /// <summary>
    /// function to get  product  by id that take   Product id
    /// </summary>
    /// <param name="id"> product  id</param>
    /// <returns> product  response dto</returns>
    public Task<Result<ProductResponseDto>> GetProductByIdAsync(int id);
    /// <summary>
    /// function to update Product  that take ProductRequestDto   
    /// </summary>
    /// <param name="id">Product id</param>
    /// <param name="productRequestDto">Product dto</param>
    /// <returns>Updated Product </returns>
    public Task<Result<ProductResponseDto>> UpdateProductAsync(int id, ProductRequestDto productRequestDto);
}