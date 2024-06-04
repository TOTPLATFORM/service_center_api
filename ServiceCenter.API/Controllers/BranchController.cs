﻿//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Application.Services;
//using ServiceCenter.Core.Result;

//namespace ServiceCenter.API.Controllers;

//public class BranchController(IBranchService branchService) : BaseController
//{
//	private readonly IBranchService _branchService = branchService;

//    /// <summary>
//    /// adds a new branch to the system.
//    /// </summary>
//    /// <param name="branchRequestDto">the data transfer object containing developer details for creation.</param>
//    /// <remarks>
//    /// access is limited to users with the "admin" role.
//    /// </remarks>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//    [HttpPost]
//	[Authorize(Roles = "Admin")]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result> AddBranch(BranchRequestDto branchRequestDto)
//	{
//		return await _branchService.AddBranchAsync(branchRequestDto);
//	}


//	/// <summary>
//	/// get all branches in the system.
//	/// </summary>
//	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//	[HttpGet]
//	[ProducesResponseType(typeof(Result<List<BranchResponseDto>>), StatusCodes.Status200OK)]
//	public async Task<Result<List<BranchResponseDto>>> GetAllBranches()
//	{
//		return await _branchService.GetAllBranchesAsync();
//	}
//    /// <summary>
//    ///get branch by id to the system.
//    /// </summary>
//    ///<param name="id">id of branch.</param>
//    /// <remarks>
//    /// access is limited to users with the "manager" role.
//    /// </remarks>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//    [HttpGet("{id}")]
//	//[Authorize(Roles = "Admin,Manager")]
//	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result<BranchResponseDto>> GetBranchById(int id)
//	{
//		return await _branchService.GetBranchByIdAsync(id);
//	}

//	/// <summary>
//	/// update  branch by id in the system.
//	/// </summary>
//	///<param name="id">id of branch.</param>
//	///<param name="branchRequestDto">branch dto.</param>
//	/// <remarks>
//	/// access is limited to users with the "manager" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpPut("{id}")]
//	[Authorize(Roles = "Admin")]
//	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result<BranchResponseDto>> UpdateBranch(int id, BranchRequestDto branchRequestDto)
//	{
//		return await _branchService.UpdateBranchAsync(id, branchRequestDto);
//	}
//	/// <summary>
//	/// search  branch by text in the system.
//	/// </summary>
//	///<param name="text">id</param>
//	/// <remarks>
//	/// Access is limited to users with the "manager ,customer" role.
//	/// </remarks>
//	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpGet("search/{text}")]
//	//[Authorize(Roles = "Admin,Manager,Customer")]
//	[ProducesResponseType(typeof(Result<BranchResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result<List<BranchResponseDto>>> SerachBranchByText(string text)
//	{
//		return await _branchService.SearchBranchByTextAsync(text);
//	}
//    /// <summary>
//    /// delete  branch by id from the system.
//    /// </summary>
//    ///<param name="id">id</param>
//    /// <remarks>
//    /// access is limited to users with the "Admin" role.
//    /// </remarks>
//    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//    [HttpDelete("{id}")]
//    [Authorize(Roles = "Admin")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//	public async Task<Result> DeleteBranchAsycn(int id)
//	{
//		return await _branchService.DeleteBranchAsync(id);
//	}
//}