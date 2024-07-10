﻿using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for customer-related services that manages customer data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface ICustomerService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new customer to the database.
	/// </summary>
	/// <param name="customerRequestDto">the customer data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the customer addition.</returns>
	public Task<Result> RegisterCustomerAsync(CustomerRequestDto customerRequestDto);


	/// <summary>
	/// asynchronously retrieves all customers in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of customers to retrieve</param>
	///<param name="index">index of customers to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of customer response DTOs.</returns>
	public Task<Result<PaginationResult<CustomerResponseDto>>> GetAllCustomersAsync(int itemCount, int index);
	/// <summary>
	/// asynchronously retrieves a customer by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the customer to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the customer response DTO.</returns>
	public Task<Result<CustomerResponseDto>> GetCustomertByIdAsync(string id);

}
