using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IManagerService: IScopedService , IApplicationService
{
    /// <summary>
    /// asynchronously adds a new manager to the database.
    /// </summary>
    /// <param name="managerRequestDto">the manager data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the manager addition.</returns>
    public Task<Result> AddManagerAsync(ManagerRequestDto managerRequestDto);


	/// <summary>
	/// asynchronously retrieves all managers in the system.
	/// </summary>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of manager response DTOs.</returns>
	public Task<Result<PaginationResult<ManagerResponseDto>>> GetAllManagersAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a manager by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the manger to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the manger response DTO.</returns>
	public Task<Result<ManagerGetByIdResponseDto>> GetMangertByIdAsync(string id);

	/// <summary>
	/// asynchronously updates the data of an existing manager.
	/// </summary>
	/// <param name="id">the unique identifier of the manager to update.</param>
	/// <param name="agentRequestDto">the manager data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<ManagerGetByIdResponseDto>> UpdateManagerAsync(string id, ManagerRequestDto managerRequestDto);

	/// <summary>
	/// asynchronously searches for managers based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within manager data.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of manager response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<ManagerResponseDto>>> SearchManagerByTextAsync(string text, int itemCount, int index);
}
