using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ICampaginService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add campagin that take campaginDto   
	/// </summary>
	/// <param name="campaginRequestDto">time slot request dto</param>
	/// <returns>Campagin added successfully </returns>
	public Task<Result> AddCampaginAsync(CampaginRequestDto campaginRequestDto);

	/// <summary>
	/// function to get all campagins 
	/// </summary>
	/// <returns>list all campaginResponseDto </returns>
	public Task<Result<List<CampaginResponseDto>>> GetAllCampaginsAsync();

	/// <summary>
	/// function to get campagin by id that take  campagin id
	/// </summary>
	/// <param name="id">campagin id</param>
	/// <returns>campagin response dto</returns>
	public Task<Result<CampaginGetByIdResposeDto>> GetCampaginByIdAsync(int id);

	/// <summary>
	/// function to update campagin that take CampaginRequestDto   
	/// </summary>
	/// <param name="id">campagin id</param>
	/// <param name="CampaginRequestDto">campagin dto</param>
	/// <returns>Updated Campagin </returns>
	public Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginAsync(int id, CampaginRequestDto CampaginRequestDto);


	/// <summary>
	/// function to search campagin by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all campagines that contain this text </returns>
	public Task<Result<List<CampaginResponseDto>>> SearchCampaginByTextAsync(string text);

	/// <summary>
	/// function to udate campagin by status  that take campaginStatus   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>Updated campagin</returns>
	public Task<Result<CampaginGetByIdResposeDto>> UpdateCampaginStatusAsync(int id, CampaginStatus status);

	/// <summary>
	/// function to delete Campagin that take CampaginDto   
	/// </summary>
	/// <param name="id">time slot id</param>
	/// <returns>Campagin removed successfully </returns>
	public Task<Result> DeleteCampaginAsync(int id);
}
