﻿using HMSWithLayers.Application.Services;
using Microsoft.AspNetCore.Mvc;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.Services;
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
    /// <returns>result of the Offer added successfully</returns>

    [HttpPost]
    //[Authorize(Roles = "Admin")]
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
    // [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<List<OfferResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<List<OfferResponseDto>>> GetAllOffersAsync()
    {
        return await _OfferService.GetAllOfferAsync();
    }
    /// <summary>
    /// action for Get an Offer by id that take Offer id.
    /// </summary>
    /// <param name="id">Offer id</param>
    /// <returns>result of Offer response dto </returns>

    [HttpGet("{id:int}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OfferResponseDto>> GetOfferByIdAsync(int id)
    {
        return await _OfferService.GetOfferByIdAsync(id);
    }
    /// </summary>
    ///<param name="id">id of Offer.</param>
    ///<param name="OfferRequestDto">Offer dto.</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result<OfferResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result<OfferResponseDto>> UpdateOffer(int id, OfferRequestDto OfferRequestDto)
    {
        return await _OfferService.UpdateOfferAsync(id, OfferRequestDto);
    }
    /// <summary>
    /// delete  Offer  by id from the system.
    /// </summary>
    ///<param name="id">id</param>
    /// <remarks>
    /// Access is limited to users with the "Admin" role.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation, which encapsulates the result of the addition process.</returns>
    [HttpDelete]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<Result> DeleteOfferAsycn(int id)
    {
        return await _OfferService.DeleteOfferAsync(id);
    }
}
