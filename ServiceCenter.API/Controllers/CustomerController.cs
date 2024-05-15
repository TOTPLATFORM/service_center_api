using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class CustomerController(ICustomerService customerService) : BaseController
{
	private readonly ICustomerService _customerService = customerService;
	/// <summary>
	/// get all customers in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<List<CustomerResponseDto>>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CustomerResponseDto>>> GetAllCustomers()
	{
		return await _customerService.GetAllCustomersAsync();
	}

	/// <summary>
	/// get all customers in the system.
	/// </summary>
	///<param name="id">id of customer.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CustomerGetByIdResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CustomerGetByIdResponseDto>> GetCustomerById(string id)
	{
		return await _customerService.GetCustomerByIdAsync(id);
	}
	/// <summary>
	/// get  customer by id in the system.
	/// </summary>
	///<param name="id">id of customer.</param>
	///<param name="customerRequestDto">customer dto.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPut("{id}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<CustomerResponseDto>> UpdateCustomer(string id, CustomerRequestDto customerRequestDto)
	{
		return await _customerService.UpdateCustomerAsync(id, customerRequestDto);
	}
	/// <summary>
	/// search  customer by text in the system.
	/// </summary>
	///<param name="text">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("search/{text}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CustomerResponseDto>>> SerachCustomerByText(string text)
	{
		return await _customerService.SearchCustomerByTextAsync(text);
	}

	/// <summary>
	/// search  customers by branch in the system
	/// </summary>
	///<param name="branchId">branch id </param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpGet("searchByCustomers/{branchId}")]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result<List<CustomerResponseDto>>> GetCustomersByBranch(int branchId)
	{
		return await _customerService.GetCustomersByBranchAsync(branchId);
	}
	/// <summary>
	/// delete  customer by id from the system.
	/// </summary>
	///<param name="id">id</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpDelete]
	//[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> DeleteCustomerAsycn(string id)
	{
		return await _customerService.DeleteCustomerAsync(id);
	}
}
