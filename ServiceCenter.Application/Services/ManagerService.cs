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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ManagerService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ManagerService> logger, IUserContextService userContext, IAuthService authService) : IManagerService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<IManagerService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService = authService;

	///<inheritdoc/>
	public async Task<Result> AddManagerAsync(ManagerRequestDto managerRequestDto)
	{
		var role = "Manager";
		var manager = _mapper.Map<Manager>(managerRequestDto);

		var department = await _dbContext.Departments.FindAsync(managerRequestDto.DepartmentId);

		if (department is null)
		{
			_logger.LogWarning("Department Invaild Id ,Id {departmentId}", managerRequestDto.DepartmentId);

			return Result.NotFound(["Department Invaild Id"]);
		}
		manager.Department = department;

		var managerAdded = await _authService.RegisterUserWithRoleAsync(manager, managerRequestDto.Password, role);

		if (!managerAdded.IsSuccess)
		{
			return Result.Error(managerAdded.Errors.FirstOrDefault());
		}

		_logger.LogInformation("Manager added successfully in the database");

		return Result.SuccessWithMessage("Manager added successfully");
	}


	///<inheritdoc/>
	public async Task<Result<PaginationResult<ManagerResponseDto>>> GetAllManagersAsync(int itemCount, int index)
	{
		var result = await _dbContext.Managers
				 .ProjectTo<ManagerResponseDto>(_mapper.ConfigurationProvider)
				 .GetAllWithPagination(itemCount, index);

		_logger.LogInformation("Fetching all Managers. Total count: {Manager}.", result.Data.Count);

		return Result.Success(result);
	}
	///<inheritdoc/>
	public async Task<Result<ManagerGetByIdResponseDto>> GetMangertByIdAsync(string id)
	{
		var result = await _dbContext.Managers
				.ProjectTo<ManagerGetByIdResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(manager => manager.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Manager Id not found,Id {ManagerId}", id);

			return Result.NotFound(["Manager not found"]);
		}

		_logger.LogInformation("Fetching Manager");

		return Result.Success(result);
	}
	
	///<inheritdoc/>
	public async Task<Result<PaginationResult<ManagerResponseDto>>> SearchManagerByTextAsync(string text,int itemCount,int index)
	{

		    var result = await _dbContext.Managers
				.ProjectTo<ManagerResponseDto>(_mapper.ConfigurationProvider)
				.Where(n => n.ManagerFirstName.Contains(text) || n.ManagerLastName.Contains(text))
				.GetAllWithPagination(itemCount,index);

		_logger.LogInformation("Fetching search manager by name . Total count: {managers}.", result.Data.Count);

		return Result.Success(result);

	}
	///<inheritdoc/>
	public async Task<Result<ManagerGetByIdResponseDto>> UpdateManagerAsync(string id, ManagerRequestDto managerRequestDto)
	{
		var manager = await _dbContext.Managers.FindAsync(id);

		if (manager is null)
		{
			_logger.LogWarning("manager  Id not found,Id {id}", id);

			return Result.NotFound(["The manager  is not found"]);
		}
		managerRequestDto.UserName = manager.UserName;

		_mapper.Map(managerRequestDto, manager);

		var result = await _authService.UpdateUserAsync(manager);

		if (!result.IsSuccess)
		{
			return Result.Error(result.Errors.FirstOrDefault());
		}

		var updatedManager = _mapper.Map<ManagerGetByIdResponseDto>(manager);

		_logger.LogInformation("mananger  updated successfully");
		return Result.Success(updatedManager, "manager  updated successfully");
	}
}
