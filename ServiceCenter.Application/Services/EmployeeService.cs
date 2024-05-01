using AutoMapper;
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

public class EmployeeService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<EmployeeService> logger, IUserContextService userContext , IAuthService authService) : IEmployeeService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<IEmployeeService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService;


	///<inheritdoc/>
	public async Task<Result> AddEmployeeAsync(EmployeeRequestDto employeeRequestDto)
	{
		var employee = _mapper.Map<Employee>(employeeRequestDto);

		var department = await _dbContext.Departments.FindAsync(employeeRequestDto.DepartmentId);

		if (department is null)
		{
			_logger.LogWarning("Department Invaild Id ,Id {departmentId}", employeeRequestDto.DepartmentId);

			return Result.NotFound(["Department Invaild Id"]);
		}
		employee.Department = department;

		var employeeAdded = await _authService.RegisterEmployeeAsync(employeeRequestDto);

		if (!employeeAdded.IsSuccess)
		{
			return Result.Error(employeeAdded.Errors.FirstOrDefault());
		}

		_logger.LogInformation("Employee added successfully in the database");

		return Result.SuccessWithMessage("Employee added successfully");
	}
}
