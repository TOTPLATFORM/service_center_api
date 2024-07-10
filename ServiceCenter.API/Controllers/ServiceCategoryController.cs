using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class ServiceCategoryController(IServiceCategoryService serviceCategoryService) : BaseController
{
    private readonly IServiceCategoryService _serviceCategoryService = serviceCategoryService;

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
        return await _serviceCategoryService.AddServiceCategoryAsync(serviceCategoryRequestDto);
    }
    /// <summary>
    /// retrieves all service category in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of service category to retrieve</param>
    ///<param name="index">index of service category to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all service category.</returns> [HttpGet]

    [HttpGet]
    [ProducesResponseType(typeof(Result<PaginationResult<ServiceCategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ServiceCategoryResponseDto>>> GetAllServiceCategories(int itemCount, int index)
    {
        return await _serviceCategoryService.GetAllServiceCategoryAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a service category  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the service category .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service category category details.</returns>[HttpGet("{id}")]

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<ServiceCategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<ServiceCategoryGetByIdResponseDto>> GetServiceCategoryById(int id)
    {
        return await _serviceCategoryService.GetServiceCategoryByIdAsync(id);
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
        return await _serviceCategoryService.UpdateServiceCategoryAsync(id, serviceCategoryRequestDto);
    }
    /// <summary>
    ///  action for remove ServiceCategory that take ServiceCategory id   
    /// </summary>
    /// <param name="id">ServiceCategory id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>result of ServiceCategory removed successfully </returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteServiceCategory(int id)
    {
        return await _serviceCategoryService.DeleteServiceCategoryAsync(id);
    }

    /// <summary>
    /// searches service category  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of service categorys to retrieve</param>
    ///<param name="index">index of service categorys to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service category  that match the search criteria.</returns>

    [HttpGet("search/{text}")]
    [ProducesResponseType(typeof(Result<PaginationResult<ServiceCategoryResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<ServiceCategoryResponseDto>>> SearchServiceCategoryByText(string text, int itemCount, int index)
    {
        return await _serviceCategoryService.SearchServiceCategoryByTextAsync(text,  itemCount,  index);
    }
}