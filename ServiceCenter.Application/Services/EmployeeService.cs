﻿using AutoMapper;
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

public class EmployeeService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<EmployeeService> logger, IUserContextService userContext , IAuthService authService) : IEmployeeService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<IEmployeeService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService = authService;


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

	public async Task<Result<List<EmployeeResponseDto>>> GetAllEmployeesAsync()
	{
		var employees = await _dbContext.Users.OfType<Employee>()
				  .ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider)
				  .ToListAsync();
		_logger.LogInformation("Fetching all employee. Total count: {employee}.", employees.Count);
		return Result.Success(employees);
	}

	///<inheritdoc/>

	public async Task<Result<EmployeeResponseDto>> GetEmployeeByIdAsync(string id)
	{
		var employee = await _dbContext.Employees
					.ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider)
					.FirstOrDefaultAsync(d => d.Id == id);

		if (employee is null)
		{
			_logger.LogWarning("employee Id not found,Id {employeeId}", id);

			return Result.NotFound(["employee not found"]);
		}
		_logger.LogInformation("Fetching employee");

		return Result.Success(employee);
	}

	///<inheritdoc/>

	public async Task<Result<EmployeeResponseDto>> UpdateEmployeeAsync(string id, EmployeeRequestDto employeeRequestDto)
	{
		var employee = await _dbContext.Employees.FindAsync(id);

		if (employee is null)
		{
			_logger.LogWarning("employee Id not found,Id {employeeId}", id);

			return Result.NotFound(["employee not found"]);
		}

		_mapper.Map(employeeRequestDto, employee);

		await _dbContext.SaveChangesAsync();

		var employeeResponse = _mapper.Map<EmployeeResponseDto>(employee);

		if (employeeResponse is null)
		{
			_logger.LogError("Failed to map employeeRequestDto to employeeResponseDto. employeeRequestDto: {@employeeRequestDto}", employeeRequestDto);
			return Result.Invalid(new List<ValidationError>
				{
					new ValidationError
					{
						ErrorMessage = "Validation Errror"
					}
				});
		}

		_logger.LogInformation("Updated employee , Id {Id}", id);

		return Result.Success(employeeResponse);
	}

	///<inheritdoc/>

	public async Task<Result<List<EmployeeResponseDto>>> SearchEmployeeByTextAsync(string text)
	{
		var names = await _dbContext.Employees
			.ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider)
			.Where(n => n.EmployeeFirstName.Contains(text))
			.ToListAsync();

		_logger.LogInformation("Fetching search Employee by name . Total count: {Employee}.", names.Count);

		return Result.Success(names);
	}

	///<inheritdoc/>

	public async Task<Result> DeleteEmployeeAsync(int id)
	{
		var employee = await _dbContext.Employees.FindAsync(id);

		if (employee is null)
		{
			_logger.LogWarning("employee Invaild Id ,Id {employeeId}", id);

			return Result.NotFound(["employee Invaild Id"]);
		}

		_dbContext.Employees.Remove(employee);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("employee remove successfully in the database");

		return Result.SuccessWithMessage("employee remove successfully ");
	}
}
