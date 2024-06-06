﻿using ServiceCenter.Application.DTOS;
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
    /// function to add  Offer  that take  OfferDto   
    /// </summary>
    /// <param name="OfferRequestDto">Offer  request dto</param>
    /// <returns> Offer  added successfully </returns>
    public Task<Result> AddOfferAsync(OfferRequestDto OfferRequestDto);
    /// <summary>
    /// function to get all Offer  
    /// </summary>
    /// <returns>list all Offer  response dto </returns>
    public Task<Result<PaginationResult<OfferResponseDto>>> GetAllOfferAsync(int itemCount , int index);
    /// <summary>
    /// function to get  Offer  by id that take   Offer id
    /// </summary>
    /// <param name="id"> Offer  id</param>
    /// <returns> Offer  response dto</returns>
    public Task<Result<OfferResponseDto>> GetOfferByIdAsync(int id);
    /// <summary>
    /// function to update Offer  that take OfferRequestDto   
    /// </summary>
    /// <param name="id">Offer id</param>
    /// <param name="OfferRequestDto">Offer dto</param>
    /// <returns>Updated Offer </returns>
    public Task<Result<OfferResponseDto>> UpdateOfferAsync(int id, OfferRequestDto OfferRequestDto);

	/// <summary>
	/// function to search by offer name  that take  offer name
	/// </summary>
	/// <param name="text">offer name</param>
	/// <returns>offer response dto </returns>
	public Task<Result<PaginationResult<OfferResponseDto>>> SearchOfferByTextAsync(string text,int itemCount,int index);

	/// <summary>
	/// function to delete Offer  that take Offer  id   
	/// </summary>
	/// <param name="id">Offer  id</param>
	/// <returns>Offer  removed successfully </returns>
	public Task<Result> DeleteOfferAsync(int id);

    /// <summary>
    /// asynchronously retrieves all products for spicific offer  in the system.
    /// </summary>
    /// <returns>a task that represents the asynchronous operation, which encapsulates the result containing a list of products response DTOs.</returns>

    public Task<Result<PaginationResult<ProductResponseDto>>> GetProductsByOffer(int offerId,int itemCount,int index);

}
