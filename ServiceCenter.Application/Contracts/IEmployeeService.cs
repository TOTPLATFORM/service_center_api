using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IEmployeeService : IApplicationService, IScopedService
{
	/// <summary>
	/// function to add employee that take employeeDto   
	/// </summary>
	/// <param name="employeeRequestDto">employee request dto</param>
	/// <returns>Employee added successfully </returns>
	public Task<Result> AddEmployeeAsync(EmployeeRequestDto employeeRequestDto);

	/// <summary>
	/// function to get all employees 
	/// </summary>
	/// <returns>list all employeeResponseDto </returns>
	public Task<Result<List<EmployeeResponseDto>>> GetAllEmployeesAsync();

	/// <summary>
	/// function to get employee by id that take  employee id
	/// </summary>
	/// <param name="id">employee id</param>
	/// <returns>employee response dto</returns>
	public Task<Result<EmployeeResponseDto>> GetEmployeeByIdAsync(int id);

	/// <summary>
	/// function to update employee that take EmployeeRequestDto   
	/// </summary>
	/// <param name="id">employee id</param>
	/// <param name="employeeRequestDto">employee dto</param>
	/// <returns>Updated Employee </returns>
	public Task<Result<EmployeeResponseDto>> UpdateEmployeeAsync(int id, EmployeeRequestDto employeeRequestDto);


	/// <summary>
	/// function to search employee by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all employeees that contain this text </returns>
	public Task<Result<List<EmployeeResponseDto>>> SearchEmployeeByTextAsync(string text);

	/// <summary>
	/// function to search employee by relation  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all employeees that contain this relation </returns>
	public Task<Result<List<EmployeeResponseDto>>> GetAllItemsCategoryForSpecificInventoryAsync(string text);

	/// <summary>
	/// function to delete Employee that take EmployeeDto   
	/// </summary>
	/// <param name="id">departmnet id</param>
	/// <returns>Employee removed successfully </returns>
	public Task<Result> DeleteEmployeeAsync(int id);
}
