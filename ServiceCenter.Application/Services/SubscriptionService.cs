using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Subscriptions;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Core.Entities;
using ServiceCenter.Application.ExtensionForServices;

namespace ServiceCenter.Application.Services;
public class SubscriptionService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<SubscriptionService> logger, IUserContextService userContext) : ISubscriptionService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<SubscriptionService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddSubscriptionAsync(SubscriptionRequestDto SubscriptionRequestDto)
    {
        var contact = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == SubscriptionRequestDto.CustomerId);
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


        var result = _mapper.Map<Subscription>(SubscriptionRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map SubscriptionRequestDto to Subscription. SubscriptionRequestDto: {@SubscriptionRequestDto}", SubscriptionRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;
        result.Customer = contact;
        _dbContext.Subscriptions.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Subscription added successfully to the database");
        return Result.SuccessWithMessage("Subscription added successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<SubscriptionResponseDto>>> GetAllSubscriptionAsync(int itemCount, int index)
    {
        var result = await _dbContext.Subscriptions
             .AsNoTracking()
             
             .ProjectTo<SubscriptionResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching all  Subscription. Total count: { Subscription}.", result.Data.Count);

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<SubscriptionResponseDto>> GetSubscriptionByIdAsync(int id)
    {
        var result = await _dbContext.Subscriptions
			.AsNoTracking()
			.ProjectTo<SubscriptionResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);
			 

        if (result is null)
        {
            _logger.LogWarning("Subscription Id not found,Id {SubscriptionId}", id);

            return Result.NotFound(["Subscription not found"]);
        }

        _logger.LogInformation("Fetching Subscription");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<SubscriptionResponseDto>> UpdateSubscriptionAsync(int id, SubscriptionRequestDto SubscriptionRequestDto)
    {
        var result = await _dbContext.Subscriptions.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Subscription Id not found,Id {SubscriptionId}", id);
            return Result.NotFound(["Subscription not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(SubscriptionRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var SubscriptionResponse = _mapper.Map<SubscriptionResponseDto>(result);
        if (SubscriptionResponse is null)
        {
            _logger.LogError("Failed to map SubscriptionRequestDto to SubscriptionResponseDto. SubscriptionRequestDto: {@SubscriptionRequestDto}", SubscriptionResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Subscription , Id {Id}", id);

        return Result.Success(SubscriptionResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteSubscriptionAsync(int id)
    {
        var Subscription = await _dbContext.Subscriptions.FindAsync(id);

        if (Subscription is null)
        {
            _logger.LogWarning("Subscription Invaild Id ,Id {SubscriptionId}", id);
            return Result.NotFound(["Subscription Invaild Id"]);
        }

        _dbContext.Subscriptions.Remove(Subscription);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Subscription removed successfully in the database");
        return Result.SuccessWithMessage("Subscription removed successfully");
    }

    public async Task<Result<PaginationResult<SubscriptionResponseDto>>> GetSubscriptionsForSpecificCustomerAsync(string customerId, int itemCount, int index)
    {
        var subscriptions= await _dbContext.Subscriptions
              .Where(s => s.Customer.Id == customerId)
              .ProjectTo<SubscriptionResponseDto>(_mapper.ConfigurationProvider)
              .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching products. Total count: {products}.", subscriptions.Data.Count);
        return Result.Success(subscriptions);
    }
}
