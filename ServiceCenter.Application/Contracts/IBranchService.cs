using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IBranchService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add branch that take branchDto   
	/// </summary>
	/// <param name="branchRequestDto">time slot request dto</param>
	/// <returns>Branch added successfully </returns>
	public Task<Result> AddBranchAsync(BranchRequestDto branchRequestDto);

	/// <summary>
	/// function to get all inventories 
	/// </summary>
	/// <returns>list all branchResponseDto </returns>
	public Task<Result<List<BranchResponseDto>>> GetAllBranchesAsync();

	/// <summary>
	/// function to get branch by id that take  branch id
	/// </summary>
	/// <param name="id">branch id</param>
	/// <returns>branch response dto</returns>
	public Task<Result<BranchResponseDto>> GetBranchByIdAsync(int id);

	/// <summary>
	/// function to update branch that take BranchRequestDto   
	/// </summary>
	/// <param name="id">branch id</param>
	/// <param name="BranchRequestDto">branch dto</param>
	/// <returns>Updated Branch </returns>
	public Task<Result<BranchResponseDto>> UpdateBranchAsync(int id, BranchRequestDto BranchRequestDto);


	/// <summary>
	/// function to search branch by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all branches that contain this text </returns>
	public Task<Result<List<BranchResponseDto>>> SearchBranchByTextAsync(string text);

	/// <summary>
	/// function to delete Branch that take BranchDto   
	/// </summary>
	/// <param name="id">time slot id</param>
	/// <returns>Branch removed successfully </returns>
	public Task<Result> DeleteBranchAsync(int id);
}