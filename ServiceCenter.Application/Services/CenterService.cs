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

public class CenterService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<CenterService> logger, IUserContextService userContext) : ICenterService
{
	private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger<CenterService> _logger = logger;
	private readonly IUserContextService _userContext = userContext;


	///<inheritdoc/>
	public async Task<Result> AddCenterAsync(CenterRequestDto centerRequestDto)
	{

		var existingCenter = await _dbContext.Centers.FirstOrDefaultAsync();

		if (existingCenter != null)
		{
			_logger.LogError("A center already exists in the system. CenterId: {ExistingCenterId}", existingCenter.Id);

			return Result.Invalid(new List<ValidationError>
		    {
			     new ValidationError
			     {
				     ErrorMessage = "A center already exists in the system"
			     }
		    });
		}

		var result = _mapper.Map<Center>(centerRequestDto);

		result.CreatedBy = _userContext.Email;

		_dbContext.Centers.Add(result);

		await _dbContext.SaveChangesAsync();

		_logger.LogInformation("Center added successfully to the database");

		return Result.SuccessWithMessage("Center added successfully");
	}
	
	///<inheritdoc/>
	public async Task<Result<CenterGetByIdResponseDto>> GetCenterAsync()
	{
		var center = await _dbContext.Centers
			.ProjectTo<CenterGetByIdResponseDto>(_mapper.ConfigurationProvider)
			.FirstOrDefaultAsync();

		if (center == null)
		{
			_logger.LogWarning("No center found in the system.");

			return Result.Invalid(new List<ValidationError>
		    {
			      new ValidationError
			      {
				     ErrorMessage = "No center found"
			      }
		    });
		}

		_logger.LogInformation("Center fetched successfully.");

		return Result.Success(center);

	}

	///<inheritdoc/>

	public async Task<Result<CenterResponseDto>> UpdateCenterAsync(int id, CenterRequestDto timeSlotRequestDto)
	{
		var result = await _dbContext.Centers.FindAsync(id);

		if (result is null)
		{
			_logger.LogWarning("Center Id not found,Id {CenterId}", id);
			return Result.NotFound(["Center not found"]);
		}

		result.ModifiedBy = _userContext.Email;

		_mapper.Map(timeSlotRequestDto, result);

		await _dbContext.SaveChangesAsync();

		var center = _mapper.Map<CenterResponseDto>(result);

		_logger.LogInformation("Updated Center , Id {Id}", id);

		return Result.Success(center);
	}	

	

}