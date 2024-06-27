using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
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
        var result = _mapper.Map<Rating>(ratingRequestDto);
        result.Product = null;
        result.Service = null;
        var contact = await _dbContext.Contacts.FirstOrDefaultAsync(m => m.Id == ratingRequestDto.ContactId);
        if (contact == null)
        {
            _logger.LogError("No contact found in the database.");
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                     ErrorMessage = "No contact found in the database."
                }

            });
        }


        var product = await _dbContext.Products.FirstOrDefaultAsync(o => o.Id == ratingRequestDto.ProductId);

            if (product == null)
            {
                _logger.LogError("Product with Id {ProductId} not found.", ratingRequestDto.ProductId);

                return Result.Invalid(new List<ValidationError>
            {
                 new ValidationError { ErrorMessage = "Product not found" }
            });
            }
            result.Product = product;
            

        
    
            var service = await _dbContext.Services.FirstOrDefaultAsync(o => o.Id == ratingRequestDto.ServiceId);
                     
            if (service == null)
            {
                _logger.LogError("Service  with Id {ServiceId} not found.", ratingRequestDto.ServiceId);

                return Result.Invalid(new List<ValidationError>
            {
                 new ValidationError { ErrorMessage = "Service  not found" }
            });
            }
            result.Service = service;

        
       
        if (result is null)
        {
            _logger.LogError("Failed to map RatingRequestDto to Rating. RatingRequestDto: {@RatingRequestDto}", ratingRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;
        result.Contact = contact;

        _dbContext.Ratings.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Rating added successfully to the database");
        return Result.SuccessWithMessage("Rating added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<RatingResponseDto>>> GetAllRatingAsync(int itemCount, int index)
    {
        var result = await _dbContext.Ratings
             .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching all  Rating. Total count: { Rating}.", result.Data.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<RatingResponseDto>> GetRatingByIdAsync(int id)
    {
        var result = await _dbContext.Ratings
            .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Rating Id not found,Id {RatingId}", id);

            return Result.NotFound(["Rating not found"]);
        }

        _logger.LogInformation("Fetching Rating");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<RatingResponseDto>> UpdateRatingValueAsync(int id, int ratingValue)
    {
        var result = await _dbContext.Ratings.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Rating Id not found,Id {RatingId}", id);
            return Result.NotFound(["Rating not found"]);
        }
        result.RatingValue = ratingValue;
        result.ModifiedBy = _userContext.Email;
        await _dbContext.SaveChangesAsync();


        var RatingResponse = _mapper.Map<RatingResponseDto>(result);
        if (RatingResponse is null)
        {
            _logger.LogError("Failed to map RatingRequestDto to RatingResponseDto. RatingRequestDto: {@RatingRequestDto}", RatingResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Rating , Id {Id}", id);

        return Result.Success(RatingResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteRatingAsync(int id)
    {
        var Rating = await _dbContext.Ratings.FindAsync(id);

        if (Rating is null)
        {
            _logger.LogWarning("Rating Invaild Id ,Id {RatingId}", id);
            return Result.NotFound(["Rating Invaild Id"]);
        }

        _dbContext.Ratings.Remove(Rating);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Rating removed successfully in the database");
        return Result.SuccessWithMessage("Rating removed successfully");
    }
    ///<inheritdoc/>

    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificCustomerAsync(string customerId, int itemCount, int index)
    {
        var Ratings = await _dbContext.Ratings
              .Where(s => s.Contact.Id == customerId)
              .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
              .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Ratings. Total count: {Ratings}.", Ratings.Data.Count);
        return Result.Success(Ratings);
    }


    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificProductAsync(int ProductId, int itemCount, int index)
    {
        var Ratings = await _dbContext.Ratings
         .Where(s => s.Product.Id == ProductId)
         .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
         .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Ratings. Total count: {Ratings}.", Ratings.Data.Count);
        return Result.Success(Ratings);
    }


    public async Task<Result<PaginationResult<RatingResponseDto>>> GetRatingsForSpecificServiceAsync(int serviceId, int itemCount, int index)
    {
        var Ratings = await _dbContext.Ratings
               .Where(s => s.Service.Id == serviceId)
               .ProjectTo<RatingResponseDto>(_mapper.ConfigurationProvider)
                .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Ratings. Total count: {Ratings}.", Ratings.Data.Count);
        return Result.Success(Ratings);
    }
}
