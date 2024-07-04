﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;

public class CustomerController(ICustomerService customerService) : BaseController
{
	private readonly ICustomerService _customerService = customerService;

	/// <summary>
	/// Adds a new customer to the system.
	/// </summary>
	/// <param name="customerRequestDto">The data transfer object containing customer details for creation.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[Authorize(Roles = "Admin,Customer")]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddCustomer(CustomerRequestDto customerRequestDto)
	{
		return await _customerService.RegisterCustomerAsync(customerRequestDto);
	}
	 
	

	/// <summary>
	/// get all customers in the system.
	/// </summary>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet]
	[Authorize(Roles = "Admin,Customer,Sales")]
	[ProducesResponseType(typeof(Result<PaginationResult<CustomerResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<CustomerResponseDto>>> GetAllCustomers(int itemCount, int index)
	{
		return await _customerService.GetAllCustomersAsync(itemCount, index);
	}

	/// <summary>
	/// action for update an customer status that take customer status and conatct id.
	/// </summary>
	/// <param name="id">customer id.</param>
	/// <param name="status">customer status</param>
	/// <returns>result of the customer response dto after updated successfully</returns>

	//[HttpPut("customerId/{id}/status/{status}")]
	//[Authorize(Roles = "Admin,Sales")]
	//[ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
	//[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	//public async Task<Result<CustomerResponseDto>> UpdateCustomerStatus(CustomerStatus status, string id)
	//{
	//	return await _customerService.UpdateCustomerAsync(id, status);
	//}
	/// <summary>
	/// get customer in the system.
	/// </summary>
	///<param name="id">id of customer.</param>
	/// <remarks>
	/// Access is limited to users with the "Admin" role.
	/// </remarks>
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
	[HttpGet("{id}")]
	[Authorize(Roles = "Admin")]
	[ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<CustomerResponseDto>> GetCustomerById(string id)
	{
		return await _customerService.GetCustomertByIdAsync(id);
	}

}

