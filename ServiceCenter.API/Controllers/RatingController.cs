using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class RatingController(IRatingService ratingServiceService) : BaseController
{
    private readonly IRatingService _RatingService = ratingServiceService;

    /// <summary>
    /// action for add Rating  action that take  Rating dto   
    /// </summary>
    /// <param name="RatingDto">Rating  dto</param>
    /// <remarks>
    /// Access is limited to users with the "Customer" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddRating(RatingRequestDto RatingDto)
    {
        return await _RatingService.AddRatingAsync(RatingDto);
    }
    /// <summary>
    /// retrieves all rating in the system.
    /// </summary>
    /// <param name = "itemCount" > item count of rating to retrieve</param>
    ///<param name="index">index of rating to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating.</returns> [HttpGet]
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetAllRating(int itemCount, int index)
    {
        return await _RatingService.GetAllRatingAsync(itemCount, index);
    }
    /// <summary>
    /// retrieves a rating  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the rating category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<RatingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<RatingResponseDto>> GetRatingById(int id)
    {
        return await _RatingService.GetRatingByIdAsync(id);
    }
    /// <summary>
    /// Updates an existing rating  by its ID.
    /// </summary>
    ///<param name="id">id of rating.</param>
    ///<param name="ratingValue">rating value dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Customer" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Customer")]
    [ProducesResponseType(typeof(Result<RatingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<RatingResponseDto>> UpdateRatingValue(int id, int ratingValue)
    {
        return await _RatingService.UpdateRatingValueAsync(id, ratingValue);
    }

    /// <summary>
    /// deletes a rating from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Customer,Manager" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the rating to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteRatingAsycn(int id)
    {
        return await _RatingService.DeleteRatingAsync(id);
    }
  
    /// <summary>
    /// retrieves ratings by their customer unique identifier.
    /// </summary>
    ///<param name="customerId">the unique identifier of the customer</param>  
    /// <param name = "itemCount" > item count of rating to retrieve</param>
    ///<param name="index">index of rating to retrieve</param>
    /// <remarks>
    /// access is limited to users with the "Manager,Admin,Customer" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the customer's ratings.</returns>

    [HttpGet("searchByCustomer/{customerId}")]
    [Authorize(Roles = "Admin,Manager,Customer")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificCustomer(string customerId, int itemCount, int index)
    {
        return await _RatingService.GetRatingsForSpecificCustomerAsync(customerId, itemCount, index);
    }
    /// <summary>
    /// retrieves ratings by their product unique identifier.
    /// </summary>
    ///<param name="productId">the unique identifier of the product</param>  
    /// <param name = "itemCount" > item count of rating to retrieve</param>
    ///<param name="index">index of rating to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the product's ratings.</returns>

    [HttpGet("searchByProduct/{productId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificProduct(int productId, int itemCount, int index)
    {
        return await _RatingService.GetRatingsForSpecificProductAsync(productId, itemCount, index);
    }

    /// <summary>
    /// retrieves ratings by their service unique identifier.
    /// </summary>
    ///<param name="serviceId">the unique identifier of the service</param>  
    /// <param name = "itemCount" > item count of rating to retrieve</param>
    ///<param name="index">index of rating to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the service's ratings.</returns>

    [HttpGet("searchByService/{serviceId}")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificService(int serviceId, int itemCount, int index)
    {
        return await _RatingService.GetRatingsForSpecificServiceAsync(serviceId, itemCount, index);
    }
}
