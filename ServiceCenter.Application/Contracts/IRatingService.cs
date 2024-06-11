using ServiceCenter.Application.DTOS;
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
    /// <param name="ratingServiceRequestDto">the rating data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the rating addition.</returns>
    public Task<Result> AddRatingAsync(RatingRequestDto ratingServiceRequestDto);
    /// <summary>
    /// asynchronously retrieves all rating in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of rating response DTOs.</returns>
    public Task<Result<List<RatingResponseDto>>> GetAllRatingsAsync();
    /// <summary>
    /// asynchronously retrieves a rating by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the rating response DTO.</returns>
    public Task<Result<RatingResponseDto>> GetRatingByIdAsync(int id);
    /// <summary>
    /// asynchronously updates the data of an existing rating service.
    /// </summary>
    /// <param name="id">the unique identifier of the rating to update.</param>
    /// <param name="ratingRequestDto">the rating data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<RatingResponseDto>> UpdateRatingAsync(int id, RatingRequestDto ratingServiceRequestDto);
    /// <summary>
    /// asynchronously deletes a rating from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteRatingAsync(int id);

    /// <summary>
    /// asynchronously searches for rating based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within rating data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of rating response DTOs that match the search criteria.</returns>
    public Task<Result<List<RatingResponseDto>>> SearchRatingByRatingValueAsync(int ratingValue);

    /// <summary>
    /// asynchronously get all a rating by agent from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the service to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all rating by service operation.</returns>

    public Task<Result<List<RatingResponseDto>>> GetRatingsByServiceAsync(int serviceId);

    /// <summary>
    /// asynchronously get all a rating by customer from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the customer to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all rating by customer operation.</returns>

    public Task<Result<List<RatingResponseDto>>> GetsRatingsByCustomerAsync(string customerId);
}
