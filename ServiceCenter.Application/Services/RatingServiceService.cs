//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Core.Result;
//using ServiceCenter.Domain.Entities;
//using ServiceCenter.Infrastructure.BaseContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Application.Services;

//public class RatingService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<RatingService> logger, IUserContextService userContext) : IRatingServiceService
//{

//    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
//    private readonly IMapper _mapper = mapper;
//    private readonly ILogger<RatingService> _logger = logger;
//    private readonly IUserContextService _userContext = userContext;

//    /// <inheritdoc/>
//    public async Task<Result> AddRatingAsync(RatingRequestDto ratingRequestDto)
//    {
//        var rating = _mapper.Map<Rating>(ratingRequestDto);
//        var service = await _dbContext.Services.FirstOrDefaultAsync(S => S.Id == ratingRequestDto.ServiceId);
//        var customer = await _dbContext.Customers.FirstOrDefaultAsync(C => C.Id == ratingRequestDto.CustomerId);
//        if (service is null || customer is null)
//        {
//            _logger.LogInformation("customer or service not found");
//            return Result.Error("rating for service added failed to the database");
//        }
//        rating.CreatedBy = _userContext.Email;
//        rating.Service = service;
//        rating.Customer = customer;
//        _dbContext.Ratings.Add(rating);
//        await _dbContext.SaveChangesAsync();
//        _logger.LogInformation("rating for service added successfully to the database");
//        return Result.SuccessWithMessage("rating for service added successfully");
//    }
//    /// <inheritdoc/>
//    public async Task<Result> DeleteRatingAsync(int id)
//    {
//        var rating = await _dbContext.Ratings.FirstOrDefaultAsync(R => R.Id == id);
//        if (rating is null)
//        {
//            _logger.LogWarning($"rating for service  with id {id} was not found while attempting to delete");
//            return Result.NotFound(["The rating for service  is not found"]);
//        }
//        _dbContext.Ratings.Remove(rating);
//        await _dbContext.SaveChangesAsync();
//        _logger.LogInformation($"Successfully removed rating for service  {rating}");
//        return Result.SuccessWithMessage("rating for service  removed successfully");
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingResponseDto>>> GetAllRatingsAsync()
//    {
//        var ratingResponseDto = await _dbContext.Ratings
//             .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
//             .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service . Total count: {rating for service}.", ratingResponseDto.Count);

//        return Result.Success(ratingResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<RatingGetByIdResponseDto>> GetRatingByIdAsync(int id)
//    {
//        var ratingResponseDto = await _dbContext.Ratings
//                    .ProjectTo<RatingGetByIdResponseDto>(_mapper.ConfigurationProvider)
//                    .FirstOrDefaultAsync(c => c.Id == id);

//        if (ratingResponseDto is null)
//        {
//            _logger.LogWarning("rating for service  Id not found,Id {id}", id);
//            return Result.NotFound(["The rating for service  is not found"]);
//        }

//        _logger.LogInformation("Fetched rating for service  details");
//        return Result.Success(ratingResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingResponseDto>>> GetRatingsByServiceAsync(int serviceId)
//    {
//        var ratingResponseDto = await _dbContext.Ratings.Where(A => A.ServiceId == serviceId)
//                                    .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
//                                    .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service for specific service . Total count: {rating for service}.", ratingResponseDto.Count);

//        return Result.Success(ratingResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingResponseDto>>> GetsRatingsByCustomerAsync(string customerId)
//    {
//        var ratingResponseDto = await _dbContext.Ratings.Where(A => A.CustomerId == customerId)
//                            .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
//                            .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service for specific customer . Total count: {rating for service}.", ratingResponseDto.Count);

//        return Result.Success(ratingResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingResponseDto>>> SearchRatingByRatingValueAsync(int ratingValue)
//    {
//        var ratingResponseDto = await _dbContext.Ratings.Where(A => A.RatingValue == ratingValue)
//                            .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
//                            .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service for specific value . Total count: {rating for service}.", ratingResponseDto.Count);

//        return Result.Success(ratingResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<RatingResponseDto>> UpdateRatingAsync(int id, RatingRequestDto ratingRequestDto)
//    {
//        var rating = await _dbContext.Ratings.FirstOrDefaultAsync(R => R.Id == id);
//        if (rating is null)
//        {
//            _logger.LogWarning("rating  Id not found,Id {id}", id);
//            return Result.NotFound(["The rating  is not found"]);
//        }
//        var service = await _dbContext.Services.FirstOrDefaultAsync(S => S.Id == ratingRequestDto.ServiceId);
//        var customer = await _dbContext.Customers.FirstOrDefaultAsync(C => C.Id == ratingRequestDto.CustomerId);
//        if (service is null || customer is null)
//        {
//            _logger.LogInformation("customer or service not found");
//            return Result.Error("rating for service added failed to the database");
//        }
//        rating= _mapper.Map(ratingRequestDto, rating);
//        rating.ModifiedBy = _userContext.Email;
//        rating.Service = service;
//        rating.Customer = customer;
//        await _dbContext.SaveChangesAsync();
//        var updatedRating = _mapper.Map<RatingResponseDto>(rating);

//        _logger.LogInformation("rating for service  updated successfully");
//        return Result.Success(updatedRating, "rating for service  updated successfully");
//    }
//}
