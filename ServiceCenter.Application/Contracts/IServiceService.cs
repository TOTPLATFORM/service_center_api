using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for service-related services that manages service data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IServiceService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new service to the database.
	/// </summary>
	/// <param name="serviceRequestDto">service request dto</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the service addition.</returns>
	public Task<Result> AddServiceAsync(ServiceRequestDto serviceRequestDto);

	/// <summary>
	/// asynchronously retrieves all services in the system.
	/// </summary>
	/// <param name = "serviceCount" > service count of services to retrieve</param>
	///<param name="index">index of services to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service response DTOs.</returns>
	public Task<Result<PaginationResult<ServiceResponseDto>>> GetAllServiceAsync(int serviceCount, int index);

	/// <summary>
	/// asynchronously retrieves a service by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the service to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service response DTO.</returns>
	public Task<Result<List<ServiceGetByIdResponseDto>>> AssignServiceToPackagesAsync(int serviceId, int servicePackageId);

	/// <summary>
	/// function to get all Service that assign to package  
	/// </summary>
	/// <returns>list all Service  response dto </returns>
	public Task<Result<PaginationResult<ServiceResponseDto>>> GetServicesByPackageAsync(int servicePackageId, int serviceCount, int index);

	/// <summary>
	/// asynchronously retrieves a service by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the service to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service response DTO.</returns>
	public Task<Result<ServiceGetByIdResponseDto>> GetServiceByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing service.
	/// </summary>
	/// <param name="id">the unique identifier of the service to update.</param>
	/// <param name="serviceRequestDto">the service data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ServiceResponseDto>> UpdateServiceAsync(int id, ServiceRequestDto serviceRequestDto);


	/// <summary>
	/// asynchronously deletes a service from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the service to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteServiceAsync(int id);

	/// <summary>
	/// asynchronously searches for services based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within service data.</param>
	/// <param name = "serviceCount" > service count of services to retrieve</param>
	///<param name="index">index of services to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ServiceResponseDto>>> SearchServiceByTextAsync(string text,int serviceCount,int index);

	/// <summary>
	/// asynchronously searches for services based on the categoryId.
	/// </summary>
	/// <param name="categoryId">the text to search within category data.</param>
	/// <param name = "serviceCount" > service count of services to retrieve</param>
	///<param name="index">index of services to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of services response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ServiceResponseDto>>> GetServicesByCategoryAsync(int categoryId,int serviceCount,int index);
}