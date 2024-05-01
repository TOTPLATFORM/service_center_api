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
public class ContractService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ContractService> logger, IUserContextService userContext) : IContractService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ContractService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddContractAsync(ContractRequestDto ContractRequestDto)
    {
        var result = _mapper.Map<Contract>(ContractRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ContractRequestDto to Contract. ContractRequestDto: {@ContractRequestDto}", ContractRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Contracts.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Contract added successfully to the database");
        return Result.SuccessWithMessage("Contract added successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<List<ContractResponseDto>>> GetAllContractAsync()
    {
        var result = await _dbContext.Contracts
             .ProjectTo<ContractResponseDto>(_mapper.ConfigurationProvider)
             .ToListAsync();

        _logger.LogInformation("Fetching all  Contract. Total count: { Contract}.", result.Count);

        return Result.Success(result);
    }



}
