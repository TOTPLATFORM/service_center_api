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
        var result = _mapper.Map<Subscription>(ContractRequestDto);
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

    ///<inheritdoc/>
    public async Task<Result<ContractResponseDto>> GetContractByIdAsync(int id)
    {
        var result = await _dbContext.Contracts
            .ProjectTo<ContractResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Contract Id not found,Id {ContractId}", id);

            return Result.NotFound(["Contract not found"]);
        }

        _logger.LogInformation("Fetching Contract");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ContractResponseDto>> UpdateContractAsync(int id, ContractRequestDto ContractRequestDto)
    {
        var result = await _dbContext.Contracts.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Contract Id not found,Id {ContractId}", id);
            return Result.NotFound(["Contract not found"]);
        }

        result.ModifiedBy = _userContext.Email;

        _mapper.Map(ContractRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var ContractResponse = _mapper.Map<ContractResponseDto>(result);
        if (ContractResponse is null)
        {
            _logger.LogError("Failed to map ContractRequestDto to ContractResponseDto. ContractRequestDto: {@ContractRequestDto}", ContractResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Contract , Id {Id}", id);

        return Result.Success(ContractResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteContractAsync(int id)
    {
        var Contract = await _dbContext.Contracts.FindAsync(id);

        if (Contract is null)
        {
            _logger.LogWarning("Contract Invaild Id ,Id {ContractId}", id);
            return Result.NotFound(["Contract Invaild Id"]);
        }

        _dbContext.Contracts.Remove(Contract);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Contract removed successfully in the database");
        return Result.SuccessWithMessage("Contract removed successfully");
    }


}
