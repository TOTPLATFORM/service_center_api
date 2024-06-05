using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IManagerService: IScopedService , IApplicationService
{
	/// <summary>
	/// asynchronously adds a new manager to the database.
	/// </summary>
	/// <param name="employeeRequestDto">the manager data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the manager addition.</returns>
	public Task<Result> AddManagerAsync(ManagerRequestDto managerRequestDto);
}
