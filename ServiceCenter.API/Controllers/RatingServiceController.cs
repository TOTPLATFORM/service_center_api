using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Result;

namespace ServiceCenter.API.Controllers;

public class RatingServiceController(IRatingServiceService ratingServiceService) : BaseController
{
    private readonly IRatingServiceService _ratingServiceService = ratingServiceService;

    /// <summary>
    /// adds a new rating service to the system.
    /// </summary>
    /// <param name="ratingServiceRequestDto">the data transfer object containing rating service details for creation.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<Result> AddRatingService(RatingServiceRequestDto ratingServiceRequestDto)
    {
        return await _ratingServiceService.AddRatingServiceAsync(ratingServiceRequestDto);
    }


    /// <summary>
    /// retrieves all rating service for spicific customer in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service for spicific customer.</returns>
    [HttpGet("SearchByCustomer/{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<RatingServiceResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<RatingServiceResponseDto>>> GetsRatingServicesByCustomer(string id)
    {
        return await _ratingServiceService.GetsRatingServicesByCustomerAsync(id);
    }
    /// <summary>
    /// retrieves all rating service for spicific service in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service for spicific service.</returns>
    [HttpGet("SearchByService/{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<RatingServiceResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<RatingServiceResponseDto>>> GetRatingServicesByService(int id)
    {
        return await _ratingServiceService.GetRatingServicesByServiceAsync(id);
    }
    /// <summary>
    /// retrieves all rating service in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of all rating service.</returns>
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<RatingServiceResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<List<RatingServiceResponseDto>>> GetAllRatingService()
    {
        return await _ratingServiceService.GetAllRatingServicesAsync();
    }

    /// <summary>
    /// retrieves a rating service  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service .</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the rating service details.</returns>
    [HttpGet("{id:int}")]
    //[Authorize(Roles = "Admin,Agent,Client")]
    [ProducesResponseType(typeof(Result<RatingServiceGetByIdResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<RatingServiceGetByIdResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<Result<RatingServiceGetByIdResponseDto>> GetRatingServiceById(int id)
    {
        return await _ratingServiceService.GetRatingServiceByIdAsync(id);
    }

    /// <summary>
    /// updates an existing rating service's information.
    /// </summary>
    /// <param name="id">the unique identifier of the rating service  to update.</param>
    /// <param name="ratingServiceRequestDto">the data transfer object containing updated details for the rating service.</param>
    /// <remarks>
    /// access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>
    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<RatingServiceResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<RatingServiceResponseDto>> UpdateRatingService(int id, RatingServiceRequestDto ratingServiceRequestDto)
    {
        return await _ratingServiceService.UpdateRatingServiceAsync(id, ratingServiceRequestDto);
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
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteRatingService(int id)
    {
        return await _ratingServiceService.DeleteRatingServiceAsync(id);
    }
}
