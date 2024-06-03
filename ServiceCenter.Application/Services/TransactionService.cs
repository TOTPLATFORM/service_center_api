using AutoMapper;
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

public class TransactionService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<TransactionService> logger, IUserContextService userContext) : ITransactionService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<TransactionService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;


    ///<inheritdoc/>
    public async Task<Result> AddTransactionAsync(TransactionRequestDto transactionRequestDto)
    {
        var result = _mapper.Map<Transaction>(transactionRequestDto);

        if (result is null)
        {
            _logger.LogError("Failed to map TransactionRequestDto to Transaction. TransactionRequestDto: {@TransactionRequestDto}", transactionRequestDto);

            return Result.Invalid(new List<ValidationError>
        {
            new ValidationError
            {
                ErrorMessage = "Validation Errror"
            }
        });
        }
        result.CreatedBy = _userContext.Email;
        _dbContext.Transactions.Add(result);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Transaction added successfully to the database");

        return Result.SuccessWithMessage("Transaction added successfully");
    }

}
