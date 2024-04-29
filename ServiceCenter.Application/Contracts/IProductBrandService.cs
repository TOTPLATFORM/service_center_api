using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts
{
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
		public Task<Result<List<ProductBrandResponseDto>>> GetAllProductBrandAsync();
        /// <summary>
        /// function to get  product brand by id that take   product brand id
        /// </summary>
        /// <param name="id"> product brand id</param>
        /// <returns> product brand response dto</returns>
        public Task<Result<ProductBrandResponseDto>> GetProductBrandByIdAsync(int id);

    }
}
