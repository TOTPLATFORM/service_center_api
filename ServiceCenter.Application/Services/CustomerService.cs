using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Core.JWT;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceCenter.Application.Services;

public class CustomerService(UserManager<ApplicationUser> userManager,ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CustomerService> logger, IUserContextService userContext) : ICustomerService
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<CustomerService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	///<inheritdoc/>
	public async Task<Result> RegisterCustomerAsync(CustomerRequestDto customerRequestDto)
	{
        if (_dbContext.Customers.Any(u => u.UserName == customerRequestDto.User.UserName))
        {
            _logger.LogError("UserName is already in use. UserName: {@UserName}", customerRequestDto.User.UserName);

            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "UserName is already in use."
                }
            });
        }
        var role = "Customer";
		var customer = _mapper.Map<Customer>(customerRequestDto);
		_mapper.Map<Contact>(customerRequestDto.Contact);
		customer.Contact.Status = ContactStatus.Customer;
		await _userManager.CreateAsync(customer, customerRequestDto.User.Password);
		var customerAddResult = await  _userManager.AddToRoleAsync(customer, role);
		if (!customerAddResult.Succeeded)
		{
			var errors = customerAddResult.Errors.FirstOrDefault().ToString();

			_logger.LogError($"An error occured while creating user: {errors}");
			return Result.Error(errors);
		}

		_logger.LogInformation($"Successfully registered a new user with username {customerRequestDto.User.UserName}");
		return Result.SuccessWithMessage("Customer added successfully");
	}


	///<inheritdoc/>
	public async Task<Result<PaginationResult<CustomerResponseDto>>> GetAllCustomersAsync(int itemCount, int index)
	{
		var result = await _dbContext.Customers
				 .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
				 .GetAllWithPagination(itemCount, index);

		_logger.LogInformation("Fetching all Customers. Total count: {Customer}.", result.Data.Count);

		return Result.Success(result);
	}

	public async Task<Result<CustomerResponseDto>> GetCustomertByIdAsync(string id)
    {
        var result = await _dbContext.Customers
				.ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(customer => customer.Id == id);

		if (result is null)
		{
			_logger.LogWarning("Customer Id not found,Id {CustomerId}", id);

			return Result.NotFound(["Customer not found"]);
		}

		_logger.LogInformation("Fetching Customer");

		return Result.Success(result);
	}


    public Task<Result> RegisterCustomerWithExistContactAsync(BaseUserRequestDto user)
	{
        throw new NotImplementedException();
    }


	///<inheritdoc/>

	public async Task<Result<CustomerResponseDto>> UpdateCustomerAsync(string id, BaseUserUpdateRequestDto customerDto)
	{
		var customer = await _dbContext.Customers.FindAsync(id);

		if (customer is null)
		{
			_logger.LogWarning("customer  Id not found,Id {id}", id);

			return Result.NotFound(["The customer  is not found"]);
		}
        
		_mapper.Map(customerDto, customer);

		//var result = await _authService.UpdateUserAsync(customer);

		//if (!result.IsSuccess)
		//{
		//	return Result.Error(result.Errors.FirstOrDefault());
		//}

		var updatedCustomer = _mapper.Map<CustomerResponseDto>(customer);

		_logger.LogInformation("mananger  updated successfully");
		return Result.Success(updatedCustomer, "customer  updated successfully");
	}
}