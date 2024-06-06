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

public class SalesService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<SalesService> logger, IUserContextService userContext, IAuthService authService) : ISalesService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<ISalesService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService = authService;


	///<inheritdoc/>
	public async Task<Result> AddSalesAsync(SalesRequestDto salesRequestDto)
	{
		var role = "Sales";
		var sales = _mapper.Map<Sales>(salesRequestDto);

		var department = await _dbContext.Departments.FindAsync(salesRequestDto.DepartmentId);

		if (department is null)
		{
			_logger.LogWarning("Department Invaild Id ,Id {departmentId}", salesRequestDto.DepartmentId);

			return Result.NotFound(["Department Invaild Id"]);
		}
		sales.Department = department;

		var salesAdded = await _authService.RegisterUserWithRoleAsync(sales, salesRequestDto.Password, role);

		if (!salesAdded.IsSuccess)
		{
			return Result.Error(salesAdded.Errors.FirstOrDefault());
		}

		_logger.LogInformation("Sales added successfully in the database");

		return Result.SuccessWithMessage("Sales added successfully");
	}

	public async Task<Result<PaginationResult<SalesResponseDto>>> GetAllSalesAsync(int itemCount, int index)
	{
		var saless = await _dbContext.Users.OfType<Sales>()
				  .ProjectTo<SalesResponseDto>(_mapper.ConfigurationProvider)
				  .GetAllWithPagination(itemCount,index);
		_logger.LogInformation("Fetching all sales. Total count: {sales}.", saless.Data.Count);
		return Result.Success(saless);
	}

	///<inheritdoc/>
	public async Task<Result<SalesResponseDto>> GetSalesByIdAsync(string Id)
	{
		var sales = await _dbContext.Sales
			.ProjectTo<SalesResponseDto>(_mapper.ConfigurationProvider)
			.FirstOrDefaultAsync(d => d.Id == Id);

		if (sales is null)
		{
			_logger.LogWarning("sales Id not found,Id {salesId}", Id);
			return Result.NotFound(["sales not found"]);
		}
		_logger.LogInformation("Fetching sales");

		return Result.Success(sales);
	}

	///<inheritdoc/>

	public async Task<Result<SalesResponseDto>> UpdateSalesAsync(string id, SalesRequestDto salesRequestDto)
	{
		var sales = await _dbContext.Sales.FindAsync(id);

		if (sales is null)
		{
			_logger.LogWarning("sales Id not found,Id {salesId}", id);

			return Result.NotFound(["sales not found"]);
		}

		_mapper.Map(salesRequestDto, sales);

		await _dbContext.SaveChangesAsync();

		var salesResponse = _mapper.Map<SalesResponseDto>(sales);

		if (salesResponse is null)
		{
			_logger.LogError("Failed to map salesRequestDto to salesResponseDto. salesRequestDto: {@salesRequestDto}", salesRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}

		_logger.LogInformation("Updated sales , Id {Id}", id);

		return Result.Success(salesResponse);
	}

	///<inheritdoc/>

	public async Task<Result<PaginationResult<SalesResponseDto>>> SearchSalesByTextAsync(string text, int itemCount, int index)
	{

		var sales = await _dbContext.Sales
					   .ProjectTo<SalesResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.SalesFirstName.Contains(text) )
					   .GetAllWithPagination(itemCount,index);

		_logger.LogInformation("Fetching search branch by name . Total count: {branch}.", sales.Data.Count);

		return Result.Success(sales);
	}

	///<inheritdoc/>

	public async Task<Result> DeleteSalesAsync(string id)
	{
		var sales = await _dbContext.Sales.FindAsync(id);

		if (sales is null)
		{
			_logger.LogWarning("sales Invaild Id ,Id {salesId}", id);

			return Result.NotFound(["sales Invaild Id"]);
		}

		_dbContext.Sales.Remove(sales);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("sales remove successfully in the database");

		return Result.SuccessWithMessage("sales remove successfully ");
	}
}


