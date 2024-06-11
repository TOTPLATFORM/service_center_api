using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;

namespace ServiceCenter.Application.Services;

public class RatingService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<RatingService> logger, IUserContextService userContext) : IRatingService
{

    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<RatingService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    /// <inheritdoc/>
    public async Task<Result> AddRatingAsync(RatingRequestDto ratingRequestDto)
    {
        var rating = _mapper.Map<Rating>(ratingRequestDto);
        rating.Products = null;
        rating.Services = null;
        if (ratingRequestDto.ProductId > 1)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(o => o.Id == ratingRequestDto.ProductId);
          
            product.Ratings.Add(rating);
        }
        if (ratingRequestDto.ServiceId > 1)
        {
            var service = await _dbContext.Services.FirstOrDefaultAsync(o => o.Id == ratingRequestDto.ServiceId);
            service.Ratings.Add(rating);
        }     
        
        if ( ratingRequestDto.ContactId==null)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(C => C.Id == ratingRequestDto.ContactId);
            _logger.LogInformation("contact or service not found");
            return Result.Error("rating for service added failed to the database");
            contact.Ratings.Add(rating);
        }
        rating.CreatedBy = _userContext.Email;
       
        _dbContext.Ratings.Add(rating);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("rating for service added successfully to the database");
        return Result.SuccessWithMessage("rating for service added successfully");
    }
    /// <inheritdoc/>
    public async Task<Result> DeleteRatingAsync(int id)
    {
        var rating = await _dbContext.Ratings.FirstOrDefaultAsync(R => R.Id == id);
        if (rating is null)
        {
            _logger.LogWarning($"rating for service  with id {id} was not found while attempting to delete");
            return Result.NotFound(["The rating for service  is not found"]);
        }
        _dbContext.Ratings.Remove(rating);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Successfully removed rating for service  {rating}");
        return Result.SuccessWithMessage("rating for service  removed successfully");
    }
    /// <inheritdoc/>
    public async Task<Result<List<RatingResponseDto>>> GetAllRatingsAsync()
    {
        var ratingResponseDto = await _dbContext.Ratings
             .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all rating for service . Total count: {rating for service}.", ratingResponseDto.Count);

        return Result.Success(ratingResponseDto);
    }
    /// <inheritdoc/>
    public async Task<Result<RatingResponseDto>> GetRatingByIdAsync(int id)
    {
        var ratingResponseDto = await _dbContext.Ratings
                    .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(c => c.Id == id);

        if (ratingResponseDto is null)
        {
            _logger.LogWarning("rating for service  Id not found,Id {id}", id);
            return Result.NotFound(["The rating for service  is not found"]);
        }

        _logger.LogInformation("Fetched rating for service  details");
        return Result.Success(ratingResponseDto);
    }

    public Task<Result<List<RatingResponseDto>>> GetRatingsByServiceAsync(int serviceId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    //public async Task<Result<List<RatingResponseDto>>> GetRatingsByServiceAsync(int serviceId)
    //{
    //    var ratingResponseDto = await _dbContext.Ratings.Where(A => A.ServiceId. == serviceId)
    //                                .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
    //                                .ToListAsync();

    //    _logger.LogInformation("Fetching all rating for service for specific service . Total count: {rating for service}.", ratingResponseDto.Count);

    //    return Result.Success(ratingResponseDto);
    //}
    /// <inheritdoc/>
    //public async Task<Result<List<RatingResponseDto>>> GetsRatingsByContactAsync(string contactId)
    //{
    //    var ratingResponseDto = await _dbContext.Ratings.Where(A => A.ContactId == contactId)
    //                        .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
    //                        .ToListAsync();

    //    _logger.LogInformation("Fetching all rating for service for specific contact . Total count: {rating for service}.", ratingResponseDto.Count);

    //    return Result.Success(ratingResponseDto);
    //}

    public Task<Result<List<RatingResponseDto>>> GetsRatingsByCustomerAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task<Result<List<RatingResponseDto>>> SearchRatingByRatingValueAsync(int ratingValue)
    {
        var ratingResponseDto = await _dbContext.Ratings.Where(A => A.RatingValue == ratingValue)
                            .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

        _logger.LogInformation("Fetching all rating for service for specific value . Total count: {rating for service}.", ratingResponseDto.Count);

        return Result.Success(ratingResponseDto);
    }
    /// <inheritdoc/>
    public async Task<Result<RatingResponseDto>> UpdateRatingAsync(int id, RatingRequestDto ratingRequestDto)
    {
        var rating = _mapper.Map<Rating>(ratingRequestDto);
        rating.Products = null;
        rating.Services = null;
        if (ratingRequestDto.ProductId > 1)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(o => o.Id == ratingRequestDto.ProductId);

            product.Ratings.Add(rating);
        }
        if (ratingRequestDto.ServiceId > 1)
        {
            var service = await _dbContext.Services.FirstOrDefaultAsync(o => o.Id == ratingRequestDto.ServiceId);
            service.Ratings.Add(rating);
        }

        if (ratingRequestDto.ContactId == null)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(C => C.Id == ratingRequestDto.ContactId);
            _logger.LogInformation("contact or service not found");
            return Result.Error("rating for service added failed to the database");
            contact.Ratings.Add(rating);
        }
        rating.ModifiedBy = _userContext.Email;
        _mapper.Map(rating,ratingRequestDto);    
       await _dbContext.SaveChangesAsync();
        var updatedRating = _mapper.Map<RatingResponseDto>(rating);
        if (updatedRating is null)
        {
            _logger.LogError("Failed to map OfferRequestDto to OfferResponseDto. OfferRequestDto: {@OfferRequestDto}", updatedRating);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }


        _logger.LogInformation("rating   updated successfully");
        return Result.Success(updatedRating, "rating for service  updated successfully");      
    }
}
