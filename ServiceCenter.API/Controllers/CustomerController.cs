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
}
