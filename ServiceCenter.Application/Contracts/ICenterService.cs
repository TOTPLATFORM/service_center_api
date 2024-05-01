using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ICenterService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add center that take center dto  
	/// </summary>
	/// <param name="CenterRequestDto">centerRequestDto</param>
	/// <returns>Center added successfully </returns>
	public Task<Result> AddCenterAsync(CenterRequestDto centerRequestDto);

	/// <summary>
	/// function to get all centers 
	/// </summary>
	/// <returns>list all centerResponseDto </returns>
	public Task<Result<List<CenterResponseDto>>> GetAllCentersAsync();

	/// <summary>
	/// function to get center by id that take  center id
	/// </summary>
	/// <param name="id">center id</param>
	/// <returns>center response dto</returns>
	public Task<Result<CenterResponseDto>> GetCenterByIdAsync(int id);

	/// <summary>
	/// function to update center that take CenterRequestDto   
	/// </summary>
	/// <param name="id">center id</param>
	/// <param name="CenterRequestDto">center dto</param>
	/// <returns>Updated Center </returns>
	public Task<Result<CenterResponseDto>> UpdateCenterAsync(int id, CenterRequestDto CenterRequestDto);


	/// <summary>
	/// function to search inventory by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all centers that contain this text </returns>
	public Task<Result<List<CenterResponseDto>>> SearchCenterByTextAsync(string text);



	/// <summary>
	/// function to delete Center that take id   
	/// </summary>
	/// <param name="id">time slot id</param>
	/// <returns>Center removed successfully </returns>
	public Task<Result> DeleteCenterAsync(int id);
}
