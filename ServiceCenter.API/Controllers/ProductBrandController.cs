using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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
    }
    }
