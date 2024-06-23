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
    private readonly IRatingService _ratingServiceService = ratingServiceService;

    /// <summary>
    /// adds a new rating service to the system.
    /// </summary>
    /// <param name="ratingRequestDto">the data transfer object containing rating service details for creation.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<Result> AddRating(RatingRequestDto ratingRequestDto)
    {
        return await _ratingServiceService.AddRatingAsync(ratingRequestDto);
    }


    /// <summary>
    /// retrieves all rating service for spicific customer in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service for spicific customer.</returns>
    //[HttpGet("SearchByCustomer/{id}")]
    //[Authorize(Roles = "Admin,Customer")]
    //[ProducesResponseType(typeof(Result<List<RatingResponseDto>>), StatusCodes.Status200OK)]
    //public async Task<Result<List<RatingResponseDto>>> GetsRatingsByCustomer(string id)
    //{
    //    return await _ratingServiceService.GetsRatingsByCustomerAsync(id);
    //}
    /// <summary>
    /// retrieves all rating service for spicific service in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service for spicific service.</returns>
    [HttpGet("SearchByService/{id}")]
    [Authorize(Roles = "Admin,Customer,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsByService(int id, int itemCount, int index)
    {
        return await _ratingServiceService.GetRatingsByServiceAsync(id,itemCount,index);
    }
    /// <summary>
    /// retrieves all rating service in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetAllRating( int itemCount, int index)
    {
        return await _ratingServiceService.GetAllRatingsAsync( itemCount,  index);
    }

    /// <summary>
    /// retrieves a rating service  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the rating service details.</returns>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Customer,Manager")]
    [ProducesResponseType(typeof(Result<RatingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<RatingResponseDto>), StatusCodes.Status404NotFound)]
    public async Task<Result<RatingResponseDto>> GetRatingById(int id)
    {
        return await _ratingServiceService.GetRatingByIdAsync(id);
    }

    /// <summary>
    /// updates an existing rating service's information.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service  to update.</param>
    /// <param name="ratingRequestDto">the data transfer object containing updated details for the rating service.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result<RatingResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<RatingResponseDto>> UpdateRating(int id, RatingRequestDto ratingRequestDto)
    {
        return await _ratingServiceService.UpdateRatingAsync(id, ratingRequestDto);
    }

    /// <summary>
    /// deletes a rating service  from the system by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service  to delete.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteRating(int id)
    {
        return await _ratingServiceService.DeleteRatingAsync(id);
    }
    /// <summary>
    /// retrieves all rating   in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service for spicific customer.</returns>
    [HttpGet("SearchByRatingValue/{id}")]
    [Authorize(Roles = "Admin,Customer")]
    [ProducesResponseType(typeof(Result<PaginationResult<RatingResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetsRatingsByRatingValue(int ratingValue, int itemCount, int index)
    {
        return await _ratingServiceService.SearchRatingByRatingValueAsync(ratingValue, itemCount,index);
    }
}
