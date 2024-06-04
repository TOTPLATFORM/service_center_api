using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
/// <summary>
/// provides an interface for rating service-related services that manages rating service data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IRatingServiceService:IApplicationService,IScopedService
{
    /// <summary>
    /// asynchronously adds a new rating service to the database.
    /// </summary>
    /// <param name="ratingServiceRequestDto">the rating service data transfer object containing the details necessary for creation.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the rating service addition.</returns>
    public Task<Result> AddRatingServiceAsync(RatingRequestDto ratingServiceRequestDto);
    /// <summary>
    /// asynchronously retrieves all rating service in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of rating service response DTOs.</returns>
    public Task<Result<List<RatingResponseDto>>> GetAllRatingServicesAsync();
    /// <summary>
    /// asynchronously retrieves a rating service by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service to retrieve.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the rating service response DTO.</returns>
    public Task<Result<RatingResponseDto>> GetRatingServiceByIdAsync(int id);
    /// <summary>
    /// asynchronously updates the data of an existing rating service.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service to update.</param>
    /// <param name="ratingRequestDto">the rating service data transfer object containing the updated details.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update operation.</returns>
    public Task<Result<RatingResponseDto>> UpdateRatingServiceAsync(int id, RatingRequestDto ratingServiceRequestDto);
    /// <summary>
    /// asynchronously deletes a rating service from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion operation.</returns>
    public Task<Result> DeleteRatingServiceAsync(int id);

    /// <summary>
    /// asynchronously searches for rating service based on the provided text.
    /// </summary>
    /// <param name="text">the text to search within rating service data.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of rating service response DTOs that match the search criteria.</returns>
    public Task<Result<List<RatingResponseDto>>> SearchRatingServiceByRatingValueAsync(int ratingValue);

    /// <summary>
    /// asynchronously get all a rating service by agent from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the service to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all rating service by service operation.</returns>

    public Task<Result<List<RatingResponseDto>>> GetRatingServicesByServiceAsync(int serviceId);

    /// <summary>
    /// asynchronously get all a rating service by customer from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the customer to get all.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the get all rating service by customer operation.</returns>

    public Task<Result<List<RatingResponseDto>>> GetsRatingServicesByCustomerAsync(string customerId);
}
