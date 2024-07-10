using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class BranchController(IBranchService branchService) : BaseController
{
	private readonly IBranchService _branchService = branchService;

	/// <summary>
	/// adds a new branch to the system.
	/// </summary>
	/// <param name="branchRequestDto">the data transfer object containing developer details for creation.</param>
	/// <remarks>
	/// access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddBranch(BranchRequestDto branchRequestDto)
	{
		return await _branchService.AddBranchAsync(branchRequestDto);
	}


    /// <summary>
    /// retrieves all branch in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of branch to retrieve</param>
    ///<param name="index">index of branch to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all branch.</returns> [HttpGet]
    [HttpGet]
	[ProducesResponseType(typeof(Result<PaginationResult<BranchResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<BranchResponseDto>>> GetAllBranches(int itemCount,int index)
	{
		return await _branchService.GetAllBranchesAsync(itemCount,index);
	}
    /// <summary>
    /// retrieves a branch  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the branch .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the branch category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
	[ProducesResponseType(typeof(Result<BranchGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<BranchGetByIdResponseDto>> GetBranchById(int id)
	{
		return await _branchService.GetBranchByIdAsync(id);
	}

	/// <summary>
	/// update  branch by id in the system.
	/// </summary>
	///<param name="id">id of branch.</param>
	///<param name="branchRequestDto">branch dto.</param>
	/// <remarks>
	/// access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

	[HttpPut("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<BranchGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<BranchGetByIdResponseDto>> UpdateBranch(int id, BranchRequestDto branchRequestDto)
	{
		return await _branchService.UpdateBranchAsync(id, branchRequestDto);
	}
    /// <summary>
    /// searches branch  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of branches to retrieve</param>
    ///<param name="index">index of branches to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of branchs  that match the search criteria.</returns>
    [HttpGet("search/{text}")]
	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<PaginationResult<BranchResponseDto>>> SerachBranchByText(string text,int itemCount,int index)
	{
		return await _branchService.SearchBranchByTextAsync(text,itemCount,index);
	}

    /// <summary>
    /// deletes a branch from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the branch to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteBranchAsycn(int id)
	{
		return await _branchService.DeleteBranchAsync(id);
	}
}