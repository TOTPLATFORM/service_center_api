using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for serviceCategory-related services that manages serviceCategory data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IServiceCategoryService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  service category that take  service category Dto   
    /// </summary>
    /// <param name="serviceCategoryRequestDto">service category request dto</param>
    /// <returns> ServiceCategory added successfully </returns>
    public Task<Result> AddServiceCategoryAsync(ServiceCategoryRequestDto serviceCategoryRequestDto);
    /// <summary>
    /// function to get all service category 
    /// </summary>
    /// <returns>list all service category response dto </returns>
    public Task<Result<List<ServiceCategoryResponseDto>>> GetAllServiceCategoryAsync();
    /// <summary>
    /// function to get  service category by id that take   service category id
    /// </summary>
    /// <param name="id"> service category id</param>
    /// <returns> service category response dto</returns>
    public Task<Result<ServiceCategoryResponseDto>> GetServiceCategoryByIdAsync(int id);
    /// <summary>
    /// function to update service category that take service category request dto   
    /// </summary>
    /// <param name="id">service category id</param>
    /// <param name="ServiceCategoryRequestDto">service category dto</param>
    /// <returns>Updated ServiceCategory </returns>
    public Task<Result<ServiceCategoryResponseDto>> UpdateServiceCategoryAsync(int id, ServiceCategoryRequestDto ServiceCategoryRequestDto);
    /// <summary>
    /// function to delete service category that take service category id   
    /// </summary>
    /// <param name="id">service category id</param>
    /// <returns>service category removed successfully </returns>
    public Task<Result> DeleteServiceCategoryAsync(int id);
    /// <summary>
    /// function to search by service category name  that take  service category name
    /// </summary>
    /// <param name="text">service category name</param>
    /// <returns>service category response dto </returns>
    public Task<Result<List<ServiceCategoryResponseDto>>> SearchServiceCategoryByTextAsync(string text);
}
