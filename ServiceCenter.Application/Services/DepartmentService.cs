//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Core.Result;
//using ServiceCenter.Domain.Entities;
//using ServiceCenter.Infrastructure.BaseContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceCenter.Application.Services;

//public class DepartmentService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<DepartmentService> logger, IUserContextService userContext) : IDepartmentService
//{
//	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
//	private readonly IMapper _mapper = mapper;
//	private readonly ILogger<IDepartmentService> _logger = logger;
//	private readonly IUserContextService _userContext = userContext;


//	///<inheritdoc/>
//	public async Task<Result> AddDepartmentAsync(DepartmentRequestDto departmentRequestDto)
//	{
//		var result = _mapper.Map<Department>(departmentRequestDto);

//		if (result is null)
//		{
//			_logger.LogError("Failed to map DepartmentRequestDto to Department. DepartmentRequestDto: {@DepartmentRequestDto}", departmentRequestDto);

//			return Result.Invalid(new List<ValidationError>
//			{
//				new ValidationError
//				{
//					ErrorMessage = "Validation Errror"
//				}
//			});
//		}
//		result.CreatedBy = _userContext.Email;

//		_dbContext.Departments.Add(result);

//		await _dbContext.SaveChangesAsync();

//		_logger.LogInformation("Department added successfully to the database");

//		return Result.SuccessWithMessage("Department added successfully");
//	}
//	///<inheritdoc/>
//	public async Task<Result<List<DepartmentResponseDto>>> GetAllDepartmentsAsync()
//	{
//		var result = await _dbContext.Departments
//				 .ProjectTo<DepartmentResponseDto>(_mapper.ConfigurationProvider)
//				 .ToListAsync();

//		_logger.LogInformation("Fetching all Departments. Total count: {Department}.", result.Count);

//		return Result.Success(result);
//	}
//	///<inheritdoc/>
//	public async Task<Result<DepartmentResponseDto>> GetDepartmentByIdAsync(int id)
//	{
//		var result = await _dbContext.Departments
//				.ProjectTo<DepartmentResponseDto>(_mapper.ConfigurationProvider)
//				.FirstOrDefaultAsync(department => department.Id == id);

//		if (result is null)
//		{
//			_logger.LogWarning("Department Id not found,Id {InevntoryId}", id);

//			return Result.NotFound(["Department not found"]);
//		}

//		_logger.LogInformation("Fetching Department");

//		return Result.Success(result);
//	}

//	///<inheritdoc/>

//	public async Task<Result<DepartmentResponseDto>> UpdateDepartmentAsync(int id, DepartmentRequestDto departmentRequestDto)
//	{
//		var result = await _dbContext.Departments.FindAsync(id);

//		if (result is null)
//		{
//			_logger.LogWarning("Department Id not found,Id {DepartmentId}", id);
//			return Result.NotFound(["Department not found"]);
//		}

//		result.ModifiedBy = _userContext.Email;

//		_mapper.Map(departmentRequestDto, result);

//		await _dbContext.SaveChangesAsync();

//		var inventory = _mapper.Map<DepartmentResponseDto>(result);

//		if (inventory is null)
//		{
//			_logger.LogError("Failed to map DepartmentRequestDto to DepartmentResponseDto. DepartmentRequestDto: {@DepartmentRequestDto}", inventory);

//			return Result.Invalid(new List<ValidationError>
//			{
//					new ValidationError
//					{
//						ErrorMessage = "Validation Errror"
//					}
//			});
//		}

//		_logger.LogInformation("Updated Department , Id {Id}", id);

//		return Result.Success(inventory);
//	}

//	///<inheritdoc/>
//	public async Task<Result<List<DepartmentResponseDto>>> SearchDepartmentByTextAsync(string text)
//	{


//		//if (string.IsNullOrWhiteSpace(text))
//		//{
//		//	_logger.LogError("Search text cannot be empty", text);

//		//	return new Result.Invalid(new List<ValidationError>
//		//	{
//		//		new ValidationError
//		//		{
//		//			ErrorMessage = "Validation Errror : Search text cannot be empty"
//		//		}
//		//	});
//		//}

//		var name = await _dbContext.Departments
//					   .ProjectTo<DepartmentResponseDto>(_mapper.ConfigurationProvider)
//					   .Where(n => n.DepartmentName.Contains(text))
//					   .ToListAsync();

//		_logger.LogInformation("Fetching search time slot by name . Total count: {time slot}.", name.Count);

//		return Result.Success(name);

//	}

//	///<inheritdoc/>
//	public async Task<Result<List<DepartmentResponseDto>>> GetAllEmployeesForSpecificDepartmentAsync(int id)
//	{
//		var employees = await _dbContext.Departments
//		   .Where(s => s.Id == id)
//		   .ProjectTo<DepartmentResponseDto>(_mapper.ConfigurationProvider)
//		   .ToListAsync();

//		_logger.LogInformation("Fetching employees. Total count: {employee}.", employees.Count);

//		return Result.Success(employees);
//	}

//	///<inheritdoc/>
//	public async Task<Result> DeleteDepartmentAsync(int id)
//	{
//		var department = await _dbContext.Departments.FindAsync(id);

//		if (department is null)
//		{
//			_logger.LogWarning("Department Invaild Id ,Id {DepartmentId}", id);

//			return Result.NotFound(["Department Invaild Id"]);
//		}

//		_dbContext.Departments.Remove(department);

//		await _dbContext.SaveChangesAsync();

//		_logger.LogInformation("Department removed successfully in the database");

//		return Result.SuccessWithMessage("Department removed successfully");
//	}

//}
