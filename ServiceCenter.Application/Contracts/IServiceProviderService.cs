using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for serviceprovider-related services that manages serviceprovider data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IServiceProviderService:IApplicationService,IScopedService
{
    /// <summary>
    /// function to add serviceprovider that take serviceproviderDto   
    /// </summary>
    /// <param name="serviceproviderRequestDto">serviceprovider request dto</param>
    /// <returns>ServiceProvider added successfully </returns>
    public Task<Result> AddServiceProviderAsync(ServiceProviderRequestDto serviceproviderRequestDto);

    /// <summary>
    /// function to get all serviceprovider 
    /// </summary>
    /// <returns>list all serviceproviderResponseDto </returns>
    public Task<Result<PaginationResult<ServiceProviderResponseDto>>> GetAllServiceProviderAsync(int itemCount, int index);

    /// <summary>
    /// function to get serviceprovider by id that take  serviceprovider id
    /// </summary>
    /// <param name="id">serviceprovider id</param>
    /// <returns>serviceprovider response dto</returns>
    public Task<Result<ServiceProviderResponseDto>> GetServiceProviderByIdAsync(string id);

    /// <summary>
    /// function to update serviceprovider that take ServiceProviderRequestDto   
    /// </summary>
    /// <param name="id">serviceprovider id</param>
    /// <param name="serviceproviderRequestDto">serviceprovider dto</param>
    /// <returns>Updated ServiceProvider </returns>
    public Task<Result<ServiceProviderResponseDto>> UpdateServiceProviderAsync(string id, ServiceProviderRequestDto serviceproviderRequestDto);


    /// <summary>
    /// function to search serviceprovider by text  that take text   
    /// </summary>
    /// <param name="text">text</param>
    /// <returns>all serviceprovideres that contain this text </returns>
    public Task<Result<PaginationResult<ServiceProviderResponseDto>>> SearchServiceProviderByTextAsync(string text, int itemCount, int index);

    /// <summary>
    /// function to delete ServiceProvider that take ServiceProviderDto   
    /// </summary>
    /// <param name="id">departmnet id</param>
    /// <returns>ServiceProvider removed successfully </returns>
    public Task<Result> DeleteServiceProviderAsync(string id);
}
