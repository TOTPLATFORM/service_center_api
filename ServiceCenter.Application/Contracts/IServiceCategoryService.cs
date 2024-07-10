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
/// provides an interface for serviceCategory-related services that manages serviceCategory data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IServiceCategoryService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new service category to the database.
	/// </summary>
	/// <param name="service categoryRequestDto">the service category data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the service category addition.</returns>
	public Task<Result> AddServiceCategoryAsync(ServiceCategoryRequestDto serviceCategoryRequestDto);

	/// <summary>
	/// asynchronously retrieves all service categorys in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of service categories to retrieve</param>
	///<param name="index">index of service categories to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service category response DTOs.</returns>
	public Task<Result<PaginationResult<ServiceCategoryResponseDto>>> GetAllServiceCategoryAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a service category by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the service category to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service category response DTO.</returns>
	public Task<Result<ServiceCategoryGetByIdResponseDto>> GetServiceCategoryByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing service category.
	/// </summary>
	/// <param name="id">the unique identifier of the service category to update.</param>
	/// <param name="service categoryRequestDto">the service category data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ServiceCategoryResponseDto>> UpdateServiceCategoryAsync(int id, ServiceCategoryRequestDto ServiceCategoryRequestDto);

	/// <summary>
	/// asynchronously deletes a service category from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the service category to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteServiceCategoryAsync(int id);

	/// <summary>
	/// asynchronously searches for service categorys based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within service category data.</param>
	/// <param name = "itemCount" > item count of service categories to retrieve</param>
	///<param name="index">index of service categories to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of service category response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ServiceCategoryResponseDto>>> SearchServiceCategoryByTextAsync(string text, int itemCount, int index);
}
