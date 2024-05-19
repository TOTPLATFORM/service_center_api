using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class BranchController(IBranchService branchService) : BaseController
{
	private readonly IBranchService _branchService = branchService;

	/// <summary>
	/// Adds a new branch to the system.
	/// </summary>
	/// <param name="branchRequestDto">The data transfer object containing developer details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddBranch(BranchRequestDto branchRequestDto)
	{
		return await _branchService.AddBranchAsync(branchRequestDto);
	}


	/// <summary>
	/// get all branches in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<BranchResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<BranchResponseDto>>> GetAllBranches()
	{
		return await _branchService.GetAllBranchesAsync();
	}
	/// <summary>
	/// get all branches in the system.
	/// </summary>
	///<param name="id">id of branch.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<BranchResponseDto>> GetBranchById(int id)
	{
		return await _branchService.GetBranchByIdAsync(id);
	}

	/// <summary>
	/// update  branch by id in the system.
	/// </summary>
	///<param name="id">id of branch.</param>
	///<param name="branchRequestDto">branch dto.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPut("{id}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<BranchResponseDto>> UpdateBranch(int id, BranchRequestDto branchRequestDto)
	{
		return await _branchService.UpdateBranchAsync(id, branchRequestDto);
	}
	/// <summary>
	/// search  branch by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search/{text}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<BranchResponseDto>>> SerachBranchByText(string text)
	{
		return await _branchService.SearchBranchByTextAsync(text);
	}
	/// <summary>
	/// delete  branch by id from the system.
	/// </summary>
	///<param name="id">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpDelete]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteBranchAsycn(int id)
	{
		return await _branchService.DeleteBranchAsync(id);
	}
}