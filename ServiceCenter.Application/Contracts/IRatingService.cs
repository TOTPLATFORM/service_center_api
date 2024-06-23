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
/// provides an interface for rating related services that manages rating data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IRatingService:IApplicationService,IScopedService
{
    /// <summary>
    /// asynchronously adds a new rating to the database.
    /// </summary>
    /// <param name="ratingRequestDto">the rating data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the rating addition.</returns>
    public Task<Result> AddRatingAsync(RatingRequestDto ratingRequestDto);
    /// <summary>
    /// asynchronously retrieves all ratings in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of rating response DTOs.</returns>
    public Task<Result<PaginationResult<RatingResponseDto>>> GetAllRatingAsync(int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves a rating by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the rating response DTO.</returns>
    public Task<Result<RatingResponseDto>> GetRatingByIdAsync(int id);
    /// <summary>
    /// asynchronously updates the rating value  of an existing rating.
    /// </summary>
    /// <param name="id">the unique identifier of the rating to update.</param>
    /// <param name="ratingValue">the rating data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public  Task<Result<RatingResponseDto>> UpdateRatingValueAsync(int id, int ratingValue);
    /// <summary>
    /// asynchronously deletes a rating from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteRatingAsync(int id);
    /// <summary>
    /// asynchronously retrieves ratings by customer unique identifier.
    /// </summary>
    /// <param name="customerId">the unique identifier of the customer to retrieve its ratings.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all ratings by customer id operation.</returns>
    public Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificCustomerAsync(string customerId, int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves ratings by product unique identifier.
    /// </summary>
    /// <param name="productId">the unique identifier of the product to retrieve its ratings.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all ratings by product id operation.</returns>
    public Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificProductAsync(int productId, int itemCount, int index);
    /// <summary>
    /// asynchronously retrieves ratings by service unique identifier.
    /// </summary>
    /// <param name="serviceId">the unique identifier of the service to retrieve its ratings.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all ratings by service id operation.</returns>
    public Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificServiceAsync(int serviceId, int itemCount, int index);
}
