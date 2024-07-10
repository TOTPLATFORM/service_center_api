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
/// provides an interface for offer-related services that manages offer data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IOfferService : IApplicationService, IScopedService
{
	/// <summary>
	/// asynchronously adds a new offer to the database.
	/// </summary>
	/// <param name="offerRequestDto">the offer data transfer object containing the details necessary for creation.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the offer addition.</returns>
	public Task<Result> AddOfferAsync(OfferRequestDto offerRequestDto);

	/// <summary>
	/// asynchronously retrieves all offers in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of offeres to retrieve</param>
	///<param name="index">index of offeres to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of offer response DTOs.</returns>
	public Task<Result<PaginationResult<OfferResponseDto>>> GetAllOfferAsync(int itemCount , int index);

	/// <summary>
	/// asynchronously retrieves a offer by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the offer to retrieve.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the offer response DTO.</returns>
	public Task<Result<OfferResponseDto>> GetOfferByIdAsync(int id);

	/// <summary>
	/// asynchronously updates the data of an existing offer.
	/// </summary>
	/// <param name="id">the unique identifier of the offer to update.</param>
	/// <param name="offerRequestDto">the offer data transfer object containing the updated details.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
	public Task<Result<OfferResponseDto>> UpdateOfferAsync(int id, OfferRequestDto offerRequestDto);

	/// <summary>
	/// asynchronously searches for offers based on the provided text.
	/// </summary>
	/// <param name="text">the text to search within offer data.</param>
	/// <param name = "itemCount" > item count of offeres to retrieve</param>
	///<param name="index">index of offeres to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of offer response DTOs that match the search criteria.</returns>
	public Task<Result<PaginationResult<OfferResponseDto>>> SearchOfferByTextAsync(string text,int itemCount,int index);

	/// <summary>
	/// asynchronously deletes a offer from the system by their unique identifier.
	/// </summary>
	/// <param name="id">the unique identifier of the offer to delete.</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
	public Task<Result> DeleteOfferAsync(int id);

	/// <summary>
	/// asynchronously retrieves all products in the system.
	/// </summary>
	/// <param name = "itemCount" > item count of offeres to retrieve</param>
	///<param name="index">index of offeres to retrieve</param>
	/// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of offer response DTOs.</returns>

	public Task<Result<PaginationResult<ProductGetByIdResponseDto>>> GetProductsByOffer(int offerId,int itemCount,int index);

}
