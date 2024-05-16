using AutoMapper;
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

public class CampaginService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CampaginService> logger, IUserContextService userContext) : ICampaginService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<CampaginService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddCampaginAsync(CampaginRequestDto campaginRequestDto)
	{
		var result = _mapper.Map<Campagin>(campaginRequestDto);

		if (result is null)
		{
			_logger.LogError("Failed to map CampaginRequestDto to Campagin. CampaginRequestDto: {@CampaginRequestDto}", campaginRequestDto);

			return Result.Invalid(new List<ValidationError>
			{
				new ValidationError
				{
					ErrorMessage = "Validation Errror"
				}
			});
		}
		result.CreatedBy = _userContext.Email;

		_dbContext.Campagins.Add(result);

		var contactsToUpdate = await _dbContext.Contacts
										   .Where(c => c.Status == ContactStatus.Oppurtienty)
										   .ToListAsync();

		foreach (var contact in contactsToUpdate)
		{
			contact.Status = ContactStatus.Lead;
		}

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Campagin added successfully to the database");

		return Result.SuccessWithMessage("Campagin added successfully");
	}
}
