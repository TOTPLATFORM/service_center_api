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
/// provides an interface for branch-related services that manages branch data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IBranchService : IApplicationService, IScopedService
{
    /// <summary>
    /// asynchronously adds a new branch to the database.
    /// </summary>
    /// <param name="branchRequestDto">the branch data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the branch addition.</returns>
	public Task<Result> AddBranchAsync(BranchRequestDto branchRequestDto);

    /// <summary>
    /// asynchronously retrieves all branchs in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of branch response DTOs.</returns>
	public Task<Result<PaginationResult<BranchResponseDto>>> GetAllBranchesAsync(int itemCount, int index);

    /// <summary>
    /// asynchronously retrieves a branch by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the branch to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the branch response DTO.</returns>
	public Task<Result<BranchResponseDto>> GetBranchByIdAsync(int id);

    /// <summary>
    /// asynchronously updates the data of an existing branch.
    /// </summary>
    /// <param name="id">the unique identifier of the branch to update.</param>
    /// <param name="branchRequestDto">the branch data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<BranchResponseDto>> UpdateBranchAsync(int id, BranchRequestDto branchRequestDto);


    /// <summary>
    /// asynchronously searches for branchs based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within branch data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of branch response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<BranchResponseDto>>> SearchBranchByTextAsync(string text, int itemCount, int index);

    /// <summary>
    /// asynchronously deletes a branch from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the branch to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteBranchAsync(int id);
}