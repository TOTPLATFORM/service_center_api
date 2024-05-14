using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface ICustomerService : IApplicationService, IScopedService
{

	/// <summary>
	/// function to get all customers 
	/// </summary>
	/// <returns>list all customerResponseDto </returns>
	public Task<Result<List<CustomerResponseDto>>> GetAllCustomersAsync();
}
