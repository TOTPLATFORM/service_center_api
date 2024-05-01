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
}
