using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IServiceService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Service  that take  ServiceDto   
    /// </summary>
    /// <param name="ServiceRequestDto">Service  request dto</param>
    /// <returns> Service  added successfully </returns>
    public Task<Result> AddServiceAsync(ServiceRequestDto ServiceRequestDto);
    /// <summary>
    /// function to get all Service  
    /// </summary>
    /// <returns>list all Service  response dto </returns>
    public Task<Result<List<ServiceResponseDto>>> GetAllServiceAsync();

	/// <summary>
	/// function to get all Service that assign to package  
	/// </summary>
	/// <returns>list all Service  response dto </returns>
	public Task<Result<List<ServiceResponseDto>>> AssignServiceToPackagesAsync(int serviceId,int servicePackageId);

	/// <summary>
	/// function to get all Service that assign to package  
	/// </summary>
	/// <returns>list all Service  response dto </returns>
	public Task<Result<List<ServiceResponseDto>>> GetServicesByPackageAsync(int servicePackageId);

	/// <summary>
	/// function to get  Service  by id that take   Service id
	/// </summary>
	/// <param name="id"> Service  id</param>
	/// <returns> Service  response dto</returns>
	public Task<Result<ServiceGetByIdResponseDto>> GetServiceByIdAsync(int id);
    /// <summary>
    /// function to update Service  that take ServiceRequestDto   
    /// </summary>
    /// <param name="id">Service id</param>
    /// <param name="ServiceRequestDto">Service dto</param>
    /// <returns>Updated Service </returns>
    public Task<Result<ServiceResponseDto>> UpdateServiceAsync(int id, ServiceRequestDto ServiceRequestDto);
    /// <summary>
    /// function to delete Service  that take Service  id   
    /// </summary>
    /// <param name="id">Service  id</param>
    /// <returns>Service  removed successfully </returns>
    public Task<Result> DeleteServiceAsync(int id);
    /// <summary>
    /// function to search by Service name  that take  Service name
    /// </summary>
    /// <param name="text">Service name</param>
    /// <returns>Service response dto </returns>
    public Task<Result<List<ServiceResponseDto>>> SearchServiceByTextAsync(string text);
}