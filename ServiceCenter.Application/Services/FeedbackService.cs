using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class FeedbackService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<FeedbackService> logger, IUserContextService userContext) : IFeedbackService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<FeedbackService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddFeedbackAsync(FeedbackRequestDto feedbackRequestDto)
    {
        var result = _mapper.Map<Feedback>(feedbackRequestDto);
        result.Product = null;
        result.Service = null;
        var contact = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == feedbackRequestDto.CustomerId);
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


        var product = await _dbContext.Products.FirstOrDefaultAsync(o => o.Id == feedbackRequestDto.ProductId);

        var service = await _dbContext.Services.FirstOrDefaultAsync(o => o.Id == feedbackRequestDto.ServiceId);

        if (product == null && service == null)
            {
                _logger.LogError("Product with Id {ProductId} not found.", feedbackRequestDto.ProductId);

                return Result.Invalid(new List<ValidationError>
            {
                 new ValidationError { ErrorMessage = "Product not found" }
            });
            }
            result.Product = product;
            result.Service = service;        
      
        if (result is null)
        {
            _logger.LogError("Failed to map FeedbackRequestDto to Feedback. FeedbackRequestDto: {@FeedbackRequestDto}", feedbackRequestDto);
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

        _dbContext.Feedbacks.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Feedback added successfully to the database");
        return Result.SuccessWithMessage("Feedback added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetAllFeedbackAsync(int itemCount, int index)
    {
        var result = await _dbContext.Feedbacks
             .ProjectTo<FeedbackResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  Feedback. Total count: { Feedback}.", result.Data.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<FeedbackResponseDto>> GetFeedbackByIdAsync(int id)
    {
        var result = await _dbContext.Feedbacks
            .ProjectTo<FeedbackResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Feedback Id not found,Id {FeedbackId}", id);

            return Result.NotFound(["Feedback not found"]);
        }

        _logger.LogInformation("Fetching Feedback");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<FeedbackResponseDto>> UpdateFeedbackDescAsync(int id,  String feedbackDesc)
    {
        var result = await _dbContext.Feedbacks.FindAsync(id);
       
        if (result is null)
        {
            _logger.LogWarning("Feedback Id not found,Id {FeedbackId}", id);
            return Result.NotFound(["Feedback not found"]);
        }
        result.FeedbackDescription = feedbackDesc;
        result.ModifiedBy = _userContext.Email;
        await _dbContext.SaveChangesAsync();
  
           
        var FeedbackResponse = _mapper.Map<FeedbackResponseDto>(result);
        if (FeedbackResponse is null)
        {
            _logger.LogError("Failed to map FeedbackRequestDto to FeedbackResponseDto. FeedbackRequestDto: {@FeedbackRequestDto}", FeedbackResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Feedback , Id {Id}", id);

        return Result.Success(FeedbackResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteFeedbackAsync(int id)
    {
        var Feedback = await _dbContext.Feedbacks.FindAsync(id);

        if (Feedback is null)
        {
            _logger.LogWarning("Feedback Invaild Id ,Id {FeedbackId}", id);
            return Result.NotFound(["Feedback Invaild Id"]);
        }

        _dbContext.Feedbacks.Remove(Feedback);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Feedback removed successfully in the database");
        return Result.SuccessWithMessage("Feedback removed successfully");
    }
    ///<inheritdoc/>
 
    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificCustomerAsync(string customerId, int itemCount, int index)
    {
        var Feedbacks = await _dbContext.Feedbacks
              .Where(s => s.Customer.Id == customerId)
              .ProjectTo<FeedbackResponseDto>(_mapper.ConfigurationProvider)
              .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Feedbacks. Total count: {Feedbacks}.", Feedbacks.Data.Count);
        return Result.Success(Feedbacks);
    }


    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificProductAsync(int productId, int itemCount, int index)
    {
        var Feedbacks = await _dbContext.Feedbacks
         .Where(s => s.Product.Id == productId)
         .ProjectTo<FeedbackResponseDto>(_mapper.ConfigurationProvider)
         .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Feedbacks. Total count: {Feedbacks}.", Feedbacks.Data.Count);
        return Result.Success(Feedbacks);
    }


    public async Task<Result<PaginationResult<FeedbackResponseDto>>> GetFeedbacksForSpecificServiceAsync(int serviceId, int itemCount, int index)
    {
        var Feedbacks = await _dbContext.Feedbacks
               .Where(s => s.Service.Id == serviceId)
               .ProjectTo<FeedbackResponseDto>(_mapper.ConfigurationProvider)
                .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Feedbacks. Total count: {Feedbacks}.", Feedbacks.Data.Count);
        return Result.Success(Feedbacks);
    }
}