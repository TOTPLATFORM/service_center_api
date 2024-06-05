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

		var employeeAdded = await _authService.RegisterUserWithRoleAsync(manager, managerRequestDto.Password, role);

		if (!employeeAdded.IsSuccess)
		{
			return Result.Error(employeeAdded.Errors.FirstOrDefault());
		}

		_logger.LogInformation("Manager added successfully in the database");

		return Result.SuccessWithMessage("Manager added successfully");
	}
}
