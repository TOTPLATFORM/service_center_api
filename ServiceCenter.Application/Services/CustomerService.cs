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

public class CustomerService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CustomerService> logger, IUserContextService userContext, IAuthService authService) : ICustomerService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<ICustomerService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService = authService;

	///<inheritdoc/>
	public async Task<Result<List<CustomerResponseDto>>> GetAllCustomersAsync()
	{
		var customers = await _dbContext.Users.OfType<Customer>()
				  .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
				  .ToListAsync();

		_logger.LogInformation("Fetching all customer. Total count: {customer}.", customers.Count);

		return Result.Success(customers);
	}

	///<inheritdoc/>
	public async Task<Result<CustomerGetByIdResponseDto>> GetCustomerByIdAsync(string Id)
	{
		var customer = await _dbContext.Customers
			.ProjectTo<CustomerGetByIdResponseDto>(_mapper.ConfigurationProvider)
			.FirstOrDefaultAsync(d => d.Id == Id);

		if (customer is null)
		{
			_logger.LogWarning("customer Id not found,Id {customerId}", Id);

			return Result.NotFound(["customer not found"]);
		}

		_logger.LogInformation("Fetching customer");

		return Result.Success(customer);
	}

	///<inheritdoc/>

	public async Task<Result<CustomerResponseDto>> UpdateCustomerAsync(string id, CustomerRequestDto customerRequestDto)
	{
		var customer = await _dbContext.Customers.FindAsync(id);

		if (customer is null)
		{
			_logger.LogWarning("customer Id not found,Id {customerId}", id);

			return Result.NotFound(["customer not found"]);
		}

		_mapper.Map(customerRequestDto, customer);

		await _dbContext.SaveChangesAsync();

		var customerResponse = _mapper.Map<CustomerResponseDto>(customer);

		if (customerResponse is null)
		{
			_logger.LogError("Failed to map customerRequestDto to customerResponseDto. customerRequestDto: {@customerRequestDto}", customerRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}

		_logger.LogInformation("Updated customer , Id {Id}", id);

		return Result.Success(customerResponse);
	}

	///<inheritdoc/>

	public async Task<Result<List<CustomerResponseDto>>> SearchCustomerByTextAsync(string text)
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

		var customer = await _dbContext.Customers
					   .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
					   .Where(n => n.CustomerFirstName.Contains(text))
					   .ToListAsync();

		_logger.LogInformation("Fetching search branch by name . Total count: {branch}.", customer.Count);

		return Result.Success(customer);
	}

	///<inheritdoc/>

	public async Task<Result> DeleteCustomerAsync(string id)
	{
		var customer = await _dbContext.Customers.FindAsync(id);

		if (customer is null)
		{
			_logger.LogWarning("customer Invaild Id ,Id {customerId}", id);

			return Result.NotFound(["customer Invaild Id"]);
		}

		_dbContext.Customers.Remove(customer);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("customer remove successfully in the database");

		return Result.SuccessWithMessage("customer remove successfully ");
	}
}


