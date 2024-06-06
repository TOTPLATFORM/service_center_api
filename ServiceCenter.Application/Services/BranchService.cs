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

public class BranchService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<BranchService> logger, IUserContextService userContext) : IBranchService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<BranchService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddBranchAsync(BranchRequestDto branchRequestDto)
	{

        var branch = _mapper.Map<Branch>(branchRequestDto);

		var manager = await _dbContext.Managers.FirstOrDefaultAsync(m => m.Id == branchRequestDto.ManagerId);

		var center = await _dbContext.Centers.FirstOrDefaultAsync();

        if (branch == null)
        {
            _logger.LogError("Failed to map BranchRequestDto to Branch. BranchRequestDto: {@BranchRequestDto}", branchRequestDto);

            return Result.Invalid(new List<ValidationError>
            {
               new ValidationError
               {
                ErrorMessage = "Validation Error"
               }
            });
        }
        branch.CreatedBy = _userContext.Email;

		branch.Manager = manager;
		branch.Center = center;

        _dbContext.Branches.Add(branch);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Branch added successfully to the database");

        return Result.SuccessWithMessage("Branch added successfully");
    }

	///<inheritdoc/>
	public async Task<Result<List<BranchResponseDto>>> GetAllBranchesAsync()
	{
		var result = await _dbContext.Branches
				 .ProjectTo<BranchResponseDto>(_mapper.ConfigurationProvider)
				 .ToListAsync();

		_logger.LogInformation("Fetching all Branches. Total count: {Branch}.", result.Count);

		return Result.Success(result);
	}
	///<inheritdoc/>
	public async Task<Result<BranchResponseDto>> GetBranchByIdAsync(int id)
	{
		var result = await _dbContext.Branches
				.ProjectTo<BranchResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(timeslot => timeslot.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Branch Id not found,Id {BranchId}", id);

			return Result.NotFound(["Branch not found"]);
		}

		_logger.LogInformation("Fetching Branch");

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<BranchResponseDto>> UpdateBranchAsync(int id, BranchRequestDto branchRequestDto)
	{
		var result = await _dbContext.Branches.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Branch Id not found,Id {BranchId}", id);
			return Result.NotFound(["Branch not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(branchRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var branch = _mapper.Map<BranchResponseDto>(result);

		if (branch is null)
		{
			_logger.LogError("Failed to map BranchRequestDto to BranchResponseDto. BranchRequestDto: {@BranchRequestDto}", branch);

			return Result.Invalid(new List<ValidationError>
			{
					new ValidationError
					{
						ErrorMessage = "Validation Errror"
					}
			});
		}

		_logger.LogInformation("Updated Branch , Id {Id}", id);

		return Result.Success(branch);
	}

	///<inheritdoc/>
	public async Task<Result<List<BranchResponseDto>>> SearchBranchByTextAsync(string text)
	{


		//if (string.IsNullOrWhiteSpace(text))
		//{
		//	_logger.LogError("Search text cannot be empty", text);

		//	return new Result.Invalid(new List<ValidationError>
		//	{
		//		new ValidationError
		//		{
		//			ErrorMessage = "Validation Errror : Search text cannot be empty"
		//		}
		//	});
		//}

		var Days = await _dbContext.Branches
					   .ProjectTo<BranchResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.BranchName.Contains(text)||n.PostalCode.Contains(text)||n.EmailAddress.Contains(text))
					   .ToListAsync();

		_logger.LogInformation("Fetching search branch by name . Total count: {branch}.", Days.Count);

		return Result.Success(Days);

	}

	///<inheritdoc/>
	public async Task<Result> DeleteBranchAsync(int id)
	{
		var timeSlot = await _dbContext.Branches.FindAsync(id);

		if (timeSlot is null)
		{
			_logger.LogWarning("Branch Invaild Id ,Id {BranchId}", id);

			return Result.NotFound(["Branch Invaild Id"]);
		}

		_dbContext.Branches.Remove(timeSlot);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Branch removed successfully in the database");

		return Result.SuccessWithMessage("Branch removed successfully");
	}
}