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
/// provides an interface for service provider-related services that manages service provider data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IServiceProviderService:IApplicationService,IScopedService
{
	/// <summary>
	/// asynchronously adds a new service provider to the database.
	/// </summary>
	/// <param name="service providerRequestDto">the service provider data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the service provider addition.</returns>
	public Task<Result> AddServiceProviderAsync(ServiceProviderRequestDto serviceproviderRequestDto);

	/// <summary>
	/// asynchronously retrieves all service providers in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of service provideres to retrieve</param>
	///<param name="index">index of service provideres to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service provider response DTOs.</returns>
	public Task<Result<PaginationResult<ServiceProviderResponseDto>>> GetAllServiceProviderAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a service provider by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the service provider to update.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service provider response DTO.</returns>
	public Task<Result<ServiceProviderGetByIdResponseDto>> GetServiceProviderByIdAsync(string id);

	/// <summary>
	/// asynchronously updates the data of an existing service provider.
	/// </summary>
	/// <param name="id">the unique identifier of the service provider to update.</param>
	/// <param name="service providerRequestDto">the service provider data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ServiceProviderGetByIdResponseDto>> UpdateServiceProviderAsync(string id, ServiceProviderRequestDto serviceproviderRequestDto);


	/// <summary>
	/// asynchronously searches for service providers based on the provided text.
	/// </summary>
	/// <param name="text">text</param>
	/// <param name = "itemCount" > item count of service provideres to retrieve</param>
	///<param name="index">index of service provideres to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service provider response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ServiceProviderResponseDto>>> SearchServiceProviderByTextAsync(string text, int itemCount, int index);

   
}
