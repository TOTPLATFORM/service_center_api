using Microsoft.AspNetCore.Authorization;
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
	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

	[HttpPost]
	[ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
	public async Task<Result> AddCustomer(CustomerRequestDto customerRequestDto)
	{
		return await _customerService.RegisterCustomerAsync(customerRequestDto);
	}
    /// <summary>
    /// retrieves all customer in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of customer to retrieve</param>
    ///<param name="index">index of customer to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager,Sales" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all customer.</returns> [HttpGet]
    [HttpGet]
	[Authorize(Roles = "Admin,Manager,Sales")]
	[ProducesResponseType(typeof(Result<PaginationResult<CustomerResponseDto>>), StatusCodes.Status200OK)]
	public async Task<Result<PaginationResult<CustomerResponseDto>>> GetAllCustomers(int itemCount, int index)
	{
		return await _customerService.GetAllCustomersAsync(itemCount, index);
	}

    /// <summary>
    /// retrieves a customer  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the customer .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager,Sales" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the customer category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
	[Authorize(Roles = "Admin,Manager,Sales")]
	[ProducesResponseType(typeof(Result<CustomerResponseDto>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
	public async Task<Result<CustomerResponseDto>> GetCustomerById(string id)
	{
		return await _customerService.GetCustomertByIdAsync(id);
	}

}

