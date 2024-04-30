using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers
{
   
    public class ProductCategoryController (IProductCategoryService productCategoryService) : BaseController
    {
        private readonly IProductCategoryService _productCategoryService = productCategoryService;

        /// <summary>
        /// action for add product category action that take  ProductCategory dto   
        /// </summary>
        /// <param name="productCategoryDto">product category dto</param>
        /// <returns>result for product category added successfully.</returns>
        [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddProductCategory(ProductCategoryRequestDto productCategoryDto)
    {
        return await _productCategoryService.AddProductCategoryAsync(productCategoryDto);
    }


}
}
