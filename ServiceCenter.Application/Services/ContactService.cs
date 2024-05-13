using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Application.DTOS;
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

public class ContactService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ContactService> logger, IUserContextService userContext) : IContactService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<ContactService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddContactAsync(ContactRequestDto contactRequestDto)
	{
		var result = _mapper.Map<Contact>(contactRequestDto);

		if (result is null)
		{
			_logger.LogError("Failed to map ContactRequestDto to Contact. ContactRequestDto: {@ContactRequestDto}", contactRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

		_dbContext.Contacts.Add(result);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Contact added successfully to the database");

		return Result.SuccessWithMessage("Contact added successfully");
	}

	///<inheritdoc/>
	public async Task<Result<List<ContactResponseDto>>> GetAllContactsAsync()
	{
		var result = await _dbContext.Contacts
				 .ProjectTo<ContactResponseDto>(_mapper.ConfigurationProvider)
				 .ToListAsync();

		_logger.LogInformation("Fetching all Contacts. Total count: {Contact}.", result.Count);

		return Result.Success(result);
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
		contact.ModifiedBy = _userContext.Email;
		await _dbContext.SaveChangesAsync();
		var contactResponseDto = _mapper.Map<ContactResponseDto>(contact);

		_logger.LogInformation($"Successfully update order status to: {contact.Status} from: {previousContactStatus}");

		return Result.Success(contactResponseDto, "Successfully updated contact");
	}
}

