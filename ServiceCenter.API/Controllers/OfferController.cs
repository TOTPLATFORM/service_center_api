using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;

namespace ServiceCenter.API.Controllers;

public class OfferController(IOfferService OfferService) : BaseController
{
    private readonly IOfferService _OfferService = OfferService;

    /// <summary>
    /// action for Add new an Offer that take Offer request dto.
    /// </summary>
    /// <param name="OfferDto">Offer request dto.</param>
    ///  <remarks>
	/// Access is limited to users with the "Admin,Manager" role.
	/// </remarks>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> AddOfferAsync(OfferRequestDto OfferDto)
    {
        return await _OfferService.AddOfferAsync(OfferDto);
    }

    /// <summary>
    /// action for Get all  Offers based on the status that take status.
    /// </summary>
    /// <returns>result of list from Offer response dto.</returns>

    [HttpGet]
    [ProducesResponseType(typeof(Result<List<OfferResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<OfferResponseDto>>> GetAllOffersAsync(int itemCount,int index)
    {
        return await _OfferService.GetAllOfferAsync(itemCount,index);
    }
    /// <summary>
    /// retrieves a offer  by their unique identifier.
    /// </summary>
    /// <param name="id">the unique identifier of the offer .</param>
    /// <remarks>
    /// Access is limited to users with the "Admin,Manager" role.
    /// </remarks> 
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the offer category details.</returns>[HttpGet("{id}")]
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<OfferResponseDto>> GetOfferByIdAsync(int id)
    {
        return await _OfferService.GetOfferByIdAsync(id);
    }
    /// <summary>
    /// updates an existing offer  by id.
    /// </summary>
    ///<param name="id">id of Offer.</param>
    ///<param name="OfferRequestDto">Offer dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the update process.</returns>

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OfferResponseDto>> UpdateOffer(int id, OfferRequestDto OfferRequestDto)
    {
        return await _OfferService.UpdateOfferAsync(id, OfferRequestDto);
    }
    /// <summary>
    /// searches offer  based on a query text.
    /// </summary>
    /// <param name="text">the search query text.</param>
    /// <param name = "itemCount" > item count of offers to retrieve</param>
    ///<param name="index">index of offers to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of offer  that match the search criteria.</returns>

    [HttpGet("search")]
    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result<PaginationResult<OfferResponseDto>>> SearchOfferByText(string text,int itemCount,int index)
    {
        return await _OfferService.SearchOfferByTextAsync(text,itemCount,index);
    }

    /// <summary>
    /// deletes a offer from the system by their unique identifier.
    /// </summary>
    /// <remarks>
    /// access is limited to users with the "Admin,Manager" role.
    /// </remarks>
    /// <param name="id">the unique identifier of the offer to delete.</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result of the deletion process.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<Result> DeleteOffer(int id)
    {
        return await _OfferService.DeleteOfferAsync(id);
    }

    /// <summary>
    /// retrieves products by their offer unique identifier.
    /// </summary>
    ///<param name="offerId">the unique identifier of the offer</param>  
    /// <param name = "itemCount" > item count of offer to retrieve</param>
    ///<param name="index">index of offer to retrieve</param>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing the offer's products.</returns>


    [HttpGet("SearchByOffer")]
    [ProducesResponseType(typeof(Result<List<ProductGetByIdResponseDto>>), StatusCodes.Status200OK)]
    public async Task<Result<PaginationResult<ProductGetByIdResponseDto>>> GetProductsByOffer(int offerId,int itemCount,int index)
    {
        return await _OfferService.GetProductsByOffer(offerId,itemCount,index);
    }
}
