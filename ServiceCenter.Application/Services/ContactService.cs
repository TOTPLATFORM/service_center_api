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
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class ContactService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ContactService> logger,IAuthService authService, IUserContextService userContext) : IContactService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<ContactService> _logger = logger;
    private readonly IAuthService _authService = authService;
    private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddContactAsync(ContactRequestDto contactRequestDto)
	{
		
        var role = "Contact";
        var contact = _mapper.Map<Contact>(contactRequestDto);

       var contactAdded = await _authService.RegisterUserWithRoleAsync(contact, contactRequestDto.Password, role);

        if (!contactAdded.IsSuccess)
        {
            return Result.Error(contactAdded.Errors.FirstOrDefault());
        }

        _logger.LogInformation("Contact added successfully in the database");

        return Result.SuccessWithMessage("Contact added successfully");
    }

	///<inheritdoc/>
	public async Task<Result<PaginationResult<ContactResponseDto>>> GetAllContactsAsync(int itemCount,int index)
	{
		var result = await _dbContext.Contacts
				 .ProjectTo<ContactResponseDto>(_mapper.ConfigurationProvider)
				 .GetAllWithPagination(itemCount,index);

		_logger.LogInformation("Fetching all Contacts. Total count: {Contact}.", result.Data.Count);

		return Result.Success(result);
	}

	///<inheritdoc/>

	public async Task<Result<ContactResponseDto>> RegisterCustomerAsync(CustomerRequestDto customerRequestDto)
	{
		var contact = _mapper.Map<Contact>(customerRequestDto);

		if (contact.Status != ContactStatus.Customer)
		{
			contact.Status = ContactStatus.Customer;
		}

		var role = "Customer";

		var contactAdded = await _authService.RegisterUserWithRoleAsync(contact, customerRequestDto.Password, role);

		if (!contactAdded.IsSuccess)
		{
			return Result.Error(contactAdded.Errors.FirstOrDefault());
		}

		_logger.LogInformation("customer added successfully in the database");

		return Result.SuccessWithMessage("Contact added successfully");
	}

	///<inheritdoc/>
	public async Task<Result<ContactResponseDto>> UpdateContactStatusAsync(int id, ContactStatus status)
	{
		var contact = await _dbContext.Contacts.FindAsync(id);

		if (contact is null)
		{
			_logger.LogWarning($"Contact with id {id} was not found while attempting to update contact status by id");

			return Result.NotFound(["The contact is not found"]);
		}

		if (status == ContactStatus.Cancelled)
		{
			 _dbContext.Contacts.Remove(contact);
			 await _dbContext.SaveChangesAsync();
		}

		var previousContactStatus = contact.Status;
		contact.Status = status;
		await _dbContext.SaveChangesAsync();
		var contactResponseDto = _mapper.Map<ContactResponseDto>(contact);

		_logger.LogInformation($"Successfully update order status to: {contact.Status} from: {previousContactStatus}");

		return Result.Success(contactResponseDto, "Successfully updated contact");
	}
}

