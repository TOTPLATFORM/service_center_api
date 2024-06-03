//using HMSWithLayers.Application.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Application.Services;
//using ServiceCenter.Core.Result;
//using ServiceCenter.Domain.Enums;

//namespace ServiceCenter.API.Controllers;

//public class OfferController(IOfferService OfferService) : BaseController
//{
//    private readonly IOfferService _OfferService = OfferService;

//    /// <summary>
//    /// action for Add new an Offer that take Offer request dto.
//    /// </summary>
//    /// <param name="OfferDto">Offer request dto.</param>
//    /// <returns>result of the Offer added successfully</returns>

//    [HttpPost]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//    public async Task<Result> AddOfferAsync(OfferRequestDto OfferDto)
//    {
//        return await _OfferService.AddOfferAsync(OfferDto);
//    }

//    /// <summary>
//    /// action for Get all  Offers based on the status that take status.
//    /// </summary>
//    /// <returns>result of list from Offer response dto.</returns>

//    [HttpGet]
//   // [Authorize(Roles = "Customer,Admin,Manager")]
//    [ProducesResponseType(typeof(Result<List<OfferResponseDto>>), StatusCodes.Status200OK)]
//    public async Task<Result<List<OfferResponseDto>>> GetAllOffersAsync()
//    {
//        return await _OfferService.GetAllOfferAsync();
//    }
//    /// <summary>
//    /// action for Get an Offer by id that take Offer id.
//    /// </summary>
//    /// <param name="id">Offer id</param>
//    /// <returns>result of Offer response dto </returns>

//    [HttpGet("{id:int}")]
//    [Authorize(Roles = "Manager,Admin")]
//    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//    public async Task<Result<OfferResponseDto>> GetOfferByIdAsync(int id)
//    {
//        return await _OfferService.GetOfferByIdAsync(id);
//    }
//    /// </summary>
//    ///<param name="id">id of Offer.</param>
//    ///<param name="OfferRequestDto">Offer dto.</param>
//    /// <remarks>
//    /// Access is limited to users with the "Admin" role.
//    /// </remarks>
//    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//    [HttpPut("{id}")]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
//    public async Task<Result<OfferResponseDto>> UpdateOffer(int id, OfferRequestDto OfferRequestDto)
//    {
//        return await _OfferService.UpdateOfferAsync(id, OfferRequestDto);
//    }

//	/// <summary>
//	/// search  offer  by offfer name in the system.
//	/// </summary>
//	///<param name="text">offer name </param>
//	/// <remarks>
//	/// Access is limited to users with the "Manager" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

//	[HttpGet("search/{text}")]
//	//[Authorize(Roles = "Admin,Manager")]
//	[ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
//	[ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//	public async Task<Result<List<OfferResponseDto>>> SearchOfferByText(string text)
//	{
//		return await _OfferService.SearchOfferByTextAsync(text);
//	}
//	/// <summary>
//	/// delete  Offer  by id from the system.
//	/// </summary>
//	///<param name="id">id</param>
//	/// <remarks>
//	/// Access is limited to users with the "Admin" role.
//	/// </remarks>
//	/// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
//	[HttpDelete("{id}")]
//    [Authorize(Roles = "Admin,Manager")]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
//    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
//    public async Task<Result> DeleteOffer(int id)
//    {
//        return await _OfferService.DeleteOfferAsync(id);
//    }

//    /// <summary>
//    /// action for Get all  products based on the status that take status.
//    /// </summary>
//    /// <returns>result of list from products response dto.</returns>

//    [HttpGet("SearchByOffer/{offerId}")]
//    //[Authorize(Roles = "Customer,Admin,Manager")]
//    [ProducesResponseType(typeof(Result<List<ProductResponseDto>>), StatusCodes.Status200OK)]
//    public async Task<Result<List<ProductResponseDto>>> GetProductsByOffer(int offerId)
//    {
//        return await _OfferService.GetProductsByOffer(offerId);
//    }
//}
