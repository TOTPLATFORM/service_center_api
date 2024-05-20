using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class OfferService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<OfferService> logger, IUserContextService userContext) : IOfferService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<OfferService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddOfferAsync(OfferRequestDto OfferRequestDto)
    {
        var result = _mapper.Map<Offer>(OfferRequestDto);
		if (result is null)
		{
			_logger.LogError("Failed to map DepartmentRequestDto to Department. DepartmentRequestDto: {@DepartmentRequestDto}", OfferRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

        _dbContext.Offers.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Offer added successfully to the database");
        return Result.SuccessWithMessage("Offer added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<List<OfferResponseDto>>> GetAllOfferAsync()
    {
        var result = await _dbContext.Offers
             .ProjectTo<OfferResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Offer. Total count: { Offer}.", result.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<OfferResponseDto>> GetOfferByIdAsync(int id)
    {
        var result = await _dbContext.Offers
            .ProjectTo<OfferResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Offer Id not found,Id {OfferId}", id);

            return Result.NotFound(["Offer not found"]);
        }

        _logger.LogInformation("Fetching Offer");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<OfferResponseDto>> UpdateOfferAsync(int id, OfferRequestDto OfferRequestDto)
    {
        var result = await _dbContext.Offers.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Offer Id not found,Id {OfferId}", id);
            return Result.NotFound(["Offer not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(OfferRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var OfferResponse = _mapper.Map<OfferResponseDto>(result);
        if (OfferResponse is null)
        {
            _logger.LogError("Failed to map OfferRequestDto to OfferResponseDto. OfferRequestDto: {@OfferRequestDto}", OfferResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Offer , Id {Id}", id);

        return Result.Success(OfferResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteOfferAsync(int id)
    {
        var Offer = await _dbContext.Offers.FindAsync(id);

        if (Offer is null)
        {
            _logger.LogWarning("Offer Invaild Id ,Id {OfferId}", id);
            return Result.NotFound(["Offer Invaild Id"]);
        }

        _dbContext.Offers.Remove(Offer);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Offer removed successfully in the database");
        return Result.SuccessWithMessage("Offer removed successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<List<ProductResponseDto>>> GetProductsByOffer(int productId)
    {
        var products = await _dbContext.Offers
            .Where(O => O.ProductId == productId)
            .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        _logger.LogInformation("Fetching all  products. Total count: { product}.", products.Count);
        return Result.Success(products);
    }

}
