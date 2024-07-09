using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ServiceCategoryController(IServiceCategoryService itemCategoryService) : BaseController
{
    private readonly IServiceCategoryService _itemCategoryService = itemCategoryService;

    /// <summary>
    /// action for add ServiceCategory action that take  ServiceCategory dto   
    /// </summary>
    /// <param name="serviceCategoryRequestDto">ServiceCategory dto</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddServiceCategory(ServiceCategoryRequestDto serviceCategoryRequestDto)
    {
        return await _itemCategoryService.AddServiceCategoryAsync(serviceCategoryRequestDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ServiceCategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ServiceCategoryResponseDto>>> GetAllServiceCategories(int itemCount, int index)
    {
        return await _itemCategoryService.GetAllServiceCategoryAsync(itemCount,index);
    }
    /// <summary>
    /// action for get ServiceCategory by id that take ServiceCategory id.  
    /// </summary>
    /// <returns>result of ServiceCategory response dto</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ServiceCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServiceCategoryGetByIdResponseDto>> GetServiceCategoryById(int id)
    {
        return await _itemCategoryService.GetServiceCategoryByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing service category by its ID.
    /// </summary>
    ///<param name="id">id of service category.</param>
    ///<param name="serviceCategoryRequestDto">service category dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<ServiceCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<ServiceCategoryResponseDto>> UpdateServiceCategory(int id, ServiceCategoryRequestDto serviceCategoryRequestDto)
    {
        return await _itemCategoryService.UpdateServiceCategoryAsync(id, serviceCategoryRequestDto);
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
    [ProducesResponseType(typeof(Result<PaginationResult<ServiceCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ServiceCategoryResponseDto>>> SearchServiceCategoryByText(string text, int itemCount, int index)
    {
        return await _itemCategoryService.SearchServiceCategoryByTextAsync(text,  itemCount,  index);
    }
}