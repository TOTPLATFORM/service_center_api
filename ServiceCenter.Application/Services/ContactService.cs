﻿using AutoMapper;
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
using System.ComponentModel.DataAnnotations;
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
		if (_dbContext.Contacts.Any(u => u.Email == contactRequestDto.Email))
		{
			_logger.LogError("Email is already in use. Email: {@Email}", contactRequestDto.Email);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Email is already in use."
				}
			});
		}
        if (_dbContext.Contacts.Any(u => u.WhatshappNumber == contactRequestDto.WhatshappNumber))
        {
            _logger.LogError("PhoneNumber is already in use. PhoneNumber: {@WhatshappNumber}", contactRequestDto.WhatshappNumber);

            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "PhoneNumber is already in use."
                }
            });
        }

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

        _dbContext.Contacts.Add(result);

        await _dbContext.SaveChangesAsync();

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
    public async Task<Result<ContactResponseDto>> GetContacttByIdAsync(Guid id)
    {
        var result = await _dbContext.Contacts
                .ProjectTo<ContactResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(c => c.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Contact Id not found,Id {ContactId}", id);

            return Result.NotFound(["Contact not found"]);
        }

        _logger.LogInformation("Fetching Contact");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ContactResponseDto>>> SearchContactByTextAsync(string text, int itemCount, int index)
    {
        var result = await _dbContext.Contacts
              .ProjectTo<ContactResponseDto>(_mapper.ConfigurationProvider)
              .Where(n => n.FirstName.Contains(text)||n.LastName.Contains(text))
              .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching search contact by name . Total count: {contacts}.", result.Data.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ContactResponseDto>> UpdateContactAsync(Guid id, ContactRequestDto contactRequestDto)
    {
        var result = await _dbContext.Contacts.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Contact Id not found,Id {ContactId}", id);
            return Result.NotFound(["Contact not found"]);
        }

      //  result.ModifiedBy = _userContext.Email;

        _mapper.Map(contactRequestDto, result);

        await _dbContext.SaveChangesAsync();

        var contact = _mapper.Map<ContactResponseDto>(result);

        if (contact is null)
        {
            _logger.LogError("Failed to map ContactRequestDto to ContactResponseDto. ContactRequestDto: {@ContactRequestDto}", contact);

            return Result.Invalid(new List<ValidationError>
            {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }

        _logger.LogInformation("Updated Contact , Id {Id}", id);

        return Result.Success(contact);
    }
    ///<inheritdoc/>
    public async Task<Result<ContactResponseDto>> UpdateContactStatusAsync(Guid id, ContactStatus status)
	{
		var contact = await _dbContext.Contacts.FindAsync(id);

		if (contact is null)
		{
			_logger.LogWarning($"Contact with id {id} was not found while attempting to update contact status by id");

			return Result.NotFound(["The contact is not found"]);
		}

		var previousContactStatus = contact.Status;
		contact.Status = status;
		await _dbContext.SaveChangesAsync();
		var contactResponseDto = _mapper.Map<ContactResponseDto>(contact);

		_logger.LogInformation($"Successfully update order status to: {contact.Status} from: {previousContactStatus}");

		return Result.Success(contactResponseDto, "Successfully updated contact");
	}
}

