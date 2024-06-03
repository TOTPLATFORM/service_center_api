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

//public class RatingServiceService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<RatingServiceService> logger, IUserContextService userContext) : IRatingServiceService
//{

//    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
//    private readonly IMapper _mapper = mapper;
//    private readonly ILogger<RatingServiceService> _logger = logger;
//    private readonly IUserContextService _userContext = userContext;

//    /// <inheritdoc/>
//    public async Task<Result> AddRatingServiceAsync(RatingServiceRequestDto ratingServiceRequestDto)
//    {
//        var rating = _mapper.Map<RatingService>(ratingServiceRequestDto);
//        var service = await _dbContext.Services.FirstOrDefaultAsync(S => S.Id == ratingServiceRequestDto.ServiceId);
//        var customer = await _dbContext.Customers.FirstOrDefaultAsync(C => C.Id == ratingServiceRequestDto.CustomerId);
//        if (service is null || customer is null)
//        {
//            _logger.LogInformation("customer or service not found");
//            return Result.Error("rating for service added failed to the database");
//        }
//        rating.CreatedBy = _userContext.Email;
//        rating.Service = service;
//        rating.Customer = customer;
//        _dbContext.RatingServices.Add(rating);
//        await _dbContext.SaveChangesAsync();
//        _logger.LogInformation("rating for service added successfully to the database");
//        return Result.SuccessWithMessage("rating for service added successfully");
//    }
//    /// <inheritdoc/>
//    public async Task<Result> DeleteRatingServiceAsync(int id)
//    {
//        var rating = await _dbContext.RatingServices.FirstOrDefaultAsync(R => R.Id == id);
//        if (rating is null)
//        {
//            _logger.LogWarning($"rating for service  with id {id} was not found while attempting to delete");
//            return Result.NotFound(["The rating for service  is not found"]);
//        }
//        _dbContext.RatingServices.Remove(rating);
//        await _dbContext.SaveChangesAsync();
//        _logger.LogInformation($"Successfully removed rating for service  {rating}");
//        return Result.SuccessWithMessage("rating for service  removed successfully");
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingServiceResponseDto>>> GetAllRatingServicesAsync()
//    {
//        var ratingServiceResponseDto = await _dbContext.RatingServices
//             .ProjectTo<RatingServiceResponseDto>(_mapper.ConfigurationProvider)
//             .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service . Total count: {rating for service}.", ratingServiceResponseDto.Count);

//        return Result.Success(ratingServiceResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<RatingServiceGetByIdResponseDto>> GetRatingServiceByIdAsync(int id)
//    {
//        var ratingServiceResponseDto = await _dbContext.RatingServices
//                    .ProjectTo<RatingServiceGetByIdResponseDto>(_mapper.ConfigurationProvider)
//                    .FirstOrDefaultAsync(c => c.Id == id);

//        if (ratingServiceResponseDto is null)
//        {
//            _logger.LogWarning("rating for service  Id not found,Id {id}", id);
//            return Result.NotFound(["The rating for service  is not found"]);
//        }

//        _logger.LogInformation("Fetched rating for service  details");
//        return Result.Success(ratingServiceResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingServiceResponseDto>>> GetRatingServicesByServiceAsync(int serviceId)
//    {
//        var ratingServiceResponseDto = await _dbContext.RatingServices.Where(A => A.ServiceId == serviceId)
//                                    .ProjectTo<RatingServiceResponseDto>(_mapper.ConfigurationProvider)
//                                    .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service for specific service . Total count: {rating for service}.", ratingServiceResponseDto.Count);

//        return Result.Success(ratingServiceResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingServiceResponseDto>>> GetsRatingServicesByCustomerAsync(string customerId)
//    {
//        var ratingServiceResponseDto = await _dbContext.RatingServices.Where(A => A.CustomerId == customerId)
//                            .ProjectTo<RatingServiceResponseDto>(_mapper.ConfigurationProvider)
//                            .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service for specific customer . Total count: {rating for service}.", ratingServiceResponseDto.Count);

//        return Result.Success(ratingServiceResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<List<RatingServiceResponseDto>>> SearchRatingServiceByRatingValueAsync(int ratingValue)
//    {
//        var ratingServiceResponseDto = await _dbContext.RatingServices.Where(A => A.RatingValue == ratingValue)
//                            .ProjectTo<RatingServiceResponseDto>(_mapper.ConfigurationProvider)
//                            .ToListAsync();

//        _logger.LogInformation("Fetching all rating for service for specific value . Total count: {rating for service}.", ratingServiceResponseDto.Count);

//        return Result.Success(ratingServiceResponseDto);
//    }
//    /// <inheritdoc/>
//    public async Task<Result<RatingServiceResponseDto>> UpdateRatingServiceAsync(int id, RatingServiceRequestDto ratingServiceRequestDto)
//    {
//        var rating = await _dbContext.RatingServices.FirstOrDefaultAsync(R => R.Id == id);
//        if (rating is null)
//        {
//            _logger.LogWarning("rating  Id not found,Id {id}", id);
//            return Result.NotFound(["The rating  is not found"]);
//        }
//        var service = await _dbContext.Services.FirstOrDefaultAsync(S => S.Id == ratingServiceRequestDto.ServiceId);
//        var customer = await _dbContext.Customers.FirstOrDefaultAsync(C => C.Id == ratingServiceRequestDto.CustomerId);
//        if (service is null || customer is null)
//        {
//            _logger.LogInformation("customer or service not found");
//            return Result.Error("rating for service added failed to the database");
//        }
//        rating= _mapper.Map(ratingServiceRequestDto, rating);
//        rating.ModifiedBy = _userContext.Email;
//        rating.Service = service;
//        rating.Customer = customer;
//        await _dbContext.SaveChangesAsync();
//        var updatedRatingService = _mapper.Map<RatingServiceResponseDto>(rating);

//        _logger.LogInformation("rating for service  updated successfully");
//        return Result.Success(updatedRatingService, "rating for service  updated successfully");
//    }
//}
