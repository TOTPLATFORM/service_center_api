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
/// provides an interface for warehouse manager-related services that manages warehouse manager data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IWareHouseManagerService : IApplicationService , IScopedService
{
    /// <summary>
    /// asynchronously adds a new warehouse manager to the database.
    /// </summary>
    /// <param name="wareHouseManagerRequestDto">the warehouse manager data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the warehouse manager addition.</returns>
    public Task<Result> AddWareHouseManagerServiceAsync(WareHouseManagerRequestDto wareHouseManagerRequestDto);
	/// <summary>
	/// asynchronously retrieves all warehouse manager in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of warehouse manager manager to retrieve</param>
	///<param name="index">index of warehouse manager manager to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of warehouse manager response DTOs.</returns>
	public Task<Result<PaginationResult<WareHouseManagerResponseDto>>> GetAllWareHouseManagerServicesAsync(int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves a warehouse manager manager by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse manager manager to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the warehouse manager response DTO.</returns>
    public Task<Result<WareHouseManagerGetByIdResponseDto>> GetWareHouseManagerServiceByIdAsync(string id);
    /// <summary>
    /// asynchronously updates the data of an existing warehouse manager manager.
    /// </summary>
    /// <param name="id">the unique identifier of the warehouse manager manager to update.</param>
    /// <param name="wareHouseManagerRequestDto">the warehouse manager data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<WareHouseManagerGetByIdResponseDto>> UpdateWareHouseManagerServiceAsync(string id, WareHouseManagerRequestDto wareHouseManagerRequestDto);


	/// <summary>
	/// asynchronously searches for warehouse manager based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within warehouse manager manager data.</param>
	/// <param name = "itemCount" > item count of warehouse manager manager to retrieve</param>
	///<param name="index">index of warehouse manager manager to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of warehouse manager response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<WareHouseManagerResponseDto>>> SearchWareHouseManagerByTextAsync(string text, int itemCount, int index);
}
