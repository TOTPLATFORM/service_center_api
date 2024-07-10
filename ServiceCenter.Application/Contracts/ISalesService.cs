using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

/// <summary>
/// provides an interface for sales-related services that manages sales data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface ISalesService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new sales to the database.
	/// </summary>
	/// <param name="salesRequestDto">the sales data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the sales addition.</returns>
	public Task<Result> AddSalesAsync(SalesRequestDto salesRequestDto);
	/// <summary>
	/// asynchronously retrieves all saless in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of sales to retrieve</param>
	///<param name="index">index of sales to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of sales response DTOs.</returns>
	public Task<Result<PaginationResult<SalesResponseDto>>> GetAllSalesAsync(int itemCount, int index);

	/// <summary>
	/// asynchronously retrieves a sales by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the sales to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the sales response DTO.</returns>
	public Task<Result<SalesGetByIdResponseDto>> GetSalesByIdAsync(string id);

	/// <summary>
	/// asynchronously updates the data of an existing sales.
	/// </summary>
	/// <param name="id">the unique identifier of the sales to update.</param>
	/// <param name="salesRequestDto">the sales data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<SalesGetByIdResponseDto>> UpdateSalesAsync(string id, SalesRequestDto salesRequestDto);

	/// <summary>
	/// asynchronously searches for saless based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within sales data.</param>
	/// <param name = "itemCount" > item count of sales to retrieve</param>
	///<param name="index">index of sales to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of sales response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<SalesResponseDto>>> SearchSalesByTextAsync(string text, int itemCount, int index);

    
}
