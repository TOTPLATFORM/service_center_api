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
    /// asynchronously retrieves all customers in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of customer response DTOs.</returns>
	public Task<Result<List<CustomerResponseDto>>> GetAllCustomersAsync();

    /// <summary>
    /// asynchronously retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the customer to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the customer response DTO.</returns>
	public Task<Result<CustomerGetByIdResponseDto>> GetCustomerByIdAsync(string id);

    /// <summary>
    /// asynchronously updates the data of an existing customer.
    /// </summary>
    /// <param name="id">the unique identifier of the customer to update.</param>
    /// <param name="customerRequestDto">the customer data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<CustomerResponseDto>> UpdateCustomerAsync(string id, CustomerRequestDto customerRequestDto);
    /// <summary>
    /// asynchronously searches for customers based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within customer data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of customer response DTOs that match the search criteria.</returns>
	public Task<Result<List<CustomerResponseDto>>> SearchCustomerByTextAsync(string text);

	/// <summary>
	/// asynchronously retrieves all customers By branch  by their unique identifier.
	/// </summary>
	/// <param name="branchId">the unique identifier of the branch to retrieve customers.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all customers by branch id operation.</returns>
	public Task<Result<List<CustomerResponseDto>>> GetCustomersByBranchAsync(int branchId);

    /// <summary>
    /// asynchronously deletes a customer from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the customer to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteCustomerAsync(string id);


}
