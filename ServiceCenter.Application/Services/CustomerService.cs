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
using MySqlX.XDevAPI;

namespace ServiceCenter.Application.Services;

public class CustomerService(UserManager<ApplicationUser> userManager,ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CustomerService> logger, IUserContextService userContext,IAuthService authService) : ICustomerService
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<CustomerService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;
	private readonly IAuthService _authService = authService;

	///<inheritdoc/>
	public async Task<Result> RegisterCustomerAsync(CustomerRequestDto customerRequestDto)
	{
		var existsContact =await _dbContext.Contacts.FirstOrDefaultAsync(C => C.Email == customerRequestDto.Email || C.WhatsAppNumber == customerRequestDto.WhatsAppNumber);
        if (_dbContext.Customers.Any(u => u.UserName == customerRequestDto.UserName))
        {
            _logger.LogError("UserName is already in use. UserName: {@UserName}", customerRequestDto.UserName);

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
		customer.Status = ContactStatus.Customer;

		var clientAdded = await _authService.RegisterUserWithRoleAsync(customer, customerRequestDto.Password, role);

		if (!clientAdded.IsSuccess)
		{
			return Result.Error(clientAdded.Errors.FirstOrDefault());
		}

		if (existsContact is not null)
		{
			_dbContext.Contacts.Remove(existsContact);
			 await _dbContext.SaveChangesAsync();
		}

		_logger.LogInformation($"Successfully registered a new user with username {customerRequestDto.UserName}");
		return Result.SuccessWithMessage("Customer added successfully");
	}
    ///<inheritdoc/>
	public async Task<Result<PaginationResult<CustomerResponseDto>>> GetAllCustomersAsync(int itemCount, int index)
	{
		var result = await _dbContext.Customers
			     .AsNoTracking()
				 .ProjectTo<CustomerResponseDto>(_mapper.ConfigurationProvider)
				 .GetAllWithPagination(itemCount, index);

		_logger.LogInformation("Fetching all Customers. Total count: {Customer}.", result.Data.Count);

		return Result.Success(result);
    } 
	///<inheritdoc/>
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




	
}