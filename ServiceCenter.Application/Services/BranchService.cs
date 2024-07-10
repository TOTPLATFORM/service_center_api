﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

		var center = await _dbContext.Centers.FirstOrDefaultAsync();
       
        branch.CreatedBy = _userContext.Email;

		branch.Center = center;

        _dbContext.Branches.Add(branch);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Branch added successfully to the database");

        return Result.SuccessWithMessage("Branch added successfully");
    }

	///<inheritdoc/>
	public async Task<Result<PaginationResult<BranchResponseDto>>> GetAllBranchesAsync(int itemCount, int index)
	{
		var result = await _dbContext.Branches
				 .ProjectTo<BranchResponseDto>(_mapper.ConfigurationProvider)
				 .GetAllWithPagination( itemCount,  index);

		_logger.LogInformation("Fetching all Branches. Total count: {Branch}.", result.Data.Count);

		return Result.Success(result);
	}
	///<inheritdoc/>
	public async Task<Result<BranchGetByIdResponseDto>> GetBranchByIdAsync(int id)
	{
		var result = await _dbContext.Branches
				.ProjectTo<BranchGetByIdResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(b => b.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Branch Id not found,Id {BranchId}", id);

			return Result.NotFound(["Branch not found"]);
		}

		_logger.LogInformation("Fetching Branch");

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<BranchGetByIdResponseDto>> UpdateBranchAsync(int id, BranchRequestDto branchRequestDto)
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

		var branch = _mapper.Map<BranchGetByIdResponseDto>(result);

		_logger.LogInformation("Updated Branch , Id {Id}", id);

		return Result.Success(branch);
	}

	///<inheritdoc/>
	public async Task<Result<PaginationResult<BranchResponseDto>>> SearchBranchByTextAsync(string text, int itemCount, int index)
	{

		var branches = await _dbContext.Branches
					   .ProjectTo<BranchResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.BranchName.Contains(text)||n.PostalCode.Contains(text)||n.EmailAddress.Contains(text))
					   .GetAllWithPagination( itemCount,  index);

		_logger.LogInformation("Fetching search branch by name . Total count: {branch}.", branches.Data.Count);

		return Result.Success(branches);

	}

	///<inheritdoc/>
	public async Task<Result> DeleteBranchAsync(int id)
	{
		var branch = await _dbContext.Branches.FindAsync(id);

		if (branch is null)
		{
			_logger.LogWarning("Branch Invaild Id ,Id {BranchId}", id);

			return Result.NotFound(["Branch Invaild Id"]);
		}

		_dbContext.Branches.Remove(branch);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Branch removed successfully in the database");

		return Result.SuccessWithMessage("Branch removed successfully");
	}
}