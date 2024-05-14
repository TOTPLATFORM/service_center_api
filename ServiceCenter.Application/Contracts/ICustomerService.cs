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

	/// <summary>
	/// function to get customer by id that take  customer id
	/// </summary>
	/// <param name="id">customer id</param>
	/// <returns>customer response dto</returns>
	public Task<Result<CustomerGetByIdResponseDto>> GetCustomerByIdAsync(string id);
	
	/// <summary>
	/// function to update customer that take CustomerRequestDto   
	/// </summary>
	/// <param name="id">customer id</param>
	/// <param name="customerRequestDto">customer dto</param>
	/// <returns>Updated Customer </returns>
	public Task<Result<CustomerResponseDto>> UpdateCustomerAsync(string id, CustomerRequestDto customerRequestDto);
	/// <summary>
	/// function to search customer by text  that take text   
	/// </summary>
	/// <param name="text">text</param>
	/// <returns>all customeres that contain this text </returns>
	public Task<Result<List<CustomerResponseDto>>> SearchCustomerByTextAsync(string text);

	/// <summary>
	/// function to delete Customer that take CustomerDto   
	/// </summary>
	/// <param name="id">customer id</param>
	/// <returns>Customer removed successfully </returns>
	public Task<Result> DeleteCustomerAsync(string id);


}
