using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ServiceCategoryController(IServiceCategoryService itemCategoryService) : BaseController
{
    private readonly IServiceCategoryService _itemCategoryService = itemCategoryService;

    /// <summary>
    /// action for add ServiceCategory action that take  ServiceCategory dto   
    /// </summary>
    /// <param name="itemCategoryDto">ServiceCategory dto</param>
    /// <returns>result for ServiceCategory added successfully.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddServiceCategory(ServiceCategoryRequestDto  serviceCategoryRequestDto)
    {
        return await _itemCategoryService.AddServiceCategoryAsync(serviceCategoryRequestDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<List<ServiceCategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<ServiceCategoryResponseDto>>> GetAllServiceCategories()
    {
        return await _itemCategoryService.GetAllServiceCategoryAsync();
    }
    /// <summary>
    /// action for get ServiceCategory by id that take ServiceCategory id.  
    /// </summary>
    /// <returns>result of ServiceCategory response dto</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ServiceCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServiceCategoryResponseDto>> GetServiceCategoryById(int id)
    {
        return await _itemCategoryService.GetServiceCategoryByIdAsync(id);
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ServiceCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServiceCategoryResponseDto>> UpdateServiceCategory(int id, ServiceCategoryRequestDto itemCategoryDto)
    {
        return await _itemCategoryService.UpdateServiceCategoryAsync(id, itemCategoryDto);
    }
    /// <summary>
    ///  action for remove ServiceCategory that take ServiceCategory id   
    /// </summary>
    /// <param name="id">ServiceCategory id</param>
    /// <returns>result of ServiceCategory removed successfully </returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteServiceCategory(int id)
    {
        return await _itemCategoryService.DeleteServiceCategoryAsync(id);
    }

    /// <summary>
    /// function to search by ServiceCategory name  that take  ServiceCategory name
    /// </summary>
    /// <param name="text">ServiceCategory name</param>
    /// <returns>ServiceCategory response dto </returns>
    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<ServiceCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<List<ServiceCategoryResponseDto>>> SearchServiceCategoryByText(string text)
    {
        return await _itemCategoryService.SearchServiceCategoryByTextAsync(text);
    }
}