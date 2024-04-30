using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts
{
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
        public Task<Result<List<ProductCategoryResponseDto>>> GetAllProductCategoryAsync();
        /// <summary>
        /// function to get  product category by id that take   item id
        /// </summary>
        /// <param name="id"> product category id</param>
        /// <returns> product category response dto</returns>
        public Task<Result<ProductCategoryResponseDto>> GetProductCategoryByIdAsync(int id);
    }
}
