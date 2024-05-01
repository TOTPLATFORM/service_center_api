using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IDepartmentService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add department that take departmentDto   
	/// </summary>
	/// <param name="departmentRequestDto">department request dto</param>
	/// <returns>Department added successfully </returns>
	public Task<Result> AddDepartmentAsync(DepartmentRequestDto departmentRequestDto);

	/// <summary>
	/// function to get all inventories 
	/// </summary>
	/// <returns>list all departmentResponseDto </returns>
	public Task<Result<List<DepartmentResponseDto>>> GetAllDepartmentsAsync();

	/// <summary>
	/// function to get department by id that take  department id
	/// </summary>
	/// <param name="id">department id</param>
	/// <returns>department response dto</returns>
	public Task<Result<DepartmentResponseDto>> GetDepartmentByIdAsync(int id);

	/// <summary>
	/// function to update department that take DepartmentRequestDto   
	/// </summary>
	/// <param name="id">department id</param>
	/// <param name="departmentRequestDto">department dto</param>
	/// <returns>Updated Department </returns>
	public Task<Result<DepartmentResponseDto>> UpdateDepartmentAsync(int id, DepartmentRequestDto departmentRequestDto);


	/// <summary>
	/// function to search department by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all departmentes that contain this text </returns>
	public Task<Result<List<DepartmentResponseDto>>> SearchDepartmentByTextAsync(string text);

	/// <summary>
	/// function to delete Department that take DepartmentDto   
	/// </summary>
	/// <param name="id">departmnet id</param>
	/// <returns>Department removed successfully </returns>
	public Task<Result> DeleteDepartmentAsync(int id);
}
