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

}

