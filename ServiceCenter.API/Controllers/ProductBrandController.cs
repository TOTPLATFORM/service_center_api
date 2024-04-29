using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers
{

    public class ProductBrandController(IProductBrandService productBrandService) : BaseController
    {
        private readonly IProductBrandService _productBrandService = productBrandService;

        /// <summary>
        /// action for add product brand action that take  productBrand dto   
        /// </summary>
        /// <param name="productBrandDto">product brand dto</param>
        /// <returns>result for product brand added successfully.</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<Result> AddProductBrand(ProductBrandRequestDto productBrandDto)
        {
            return await _productBrandService.AddProductBrandAsync(productBrandDto);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result<List<ProductBrandResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<Result<List<ProductBrandResponseDto>>> GetAllProductBrands()
        {
            return await _productBrandService.GetAllProductBrandAsync();
        }
        /// <summary>
        /// action for get product brand by id that take product brand id.  
        /// </summary>
        /// <returns>result of product brand response dto</returns>
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result<ProductBrandResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<Result<ProductBrandResponseDto>> GetProductBrandById(int id)
        {
            return await _productBrandService.GetProductBrandByIdAsync(id);
        }
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result<ProductBrandResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<Result<ProductBrandResponseDto>> UpdateProductBrand(int id, ProductBrandRequestDto productBrandDto)
        {
            return await _productBrandService.UpdateProductBrandAsync(id, productBrandDto);
        }
        /// <summary>
        ///  action for remove product brand that take product brand id   
        /// </summary>
        /// <param name="id">product brand id</param>
        /// <returns>result of product brand removed successfully </returns>
        [HttpDelete]
      //  [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<Result> DeleteProductBrandAsycn(int id)
        {
            return await _productBrandService.DeleteProductBrandAsync(id);
        }

    }
}
