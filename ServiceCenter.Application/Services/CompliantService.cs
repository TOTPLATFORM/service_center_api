using AutoMapper;
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

public class ComplaintService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ComplaintService> logger, IUserContextService userContext) : IComplaintService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ComplaintService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddComplaintAsync(ComplaintRequestDto ComplaintRequestDto)
    {
        var result = _mapper.Map<Complaint>(ComplaintRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ComplaintRequestDto to Complaint. ComplaintRequestDto: {@ComplaintRequestDto}", ComplaintRequestDto);
            return Result.Invalid(new List<ValidationError>
    {
        new ValidationError
        {
            ErrorMessage = "Validation Errror"
        }
    });
        }
        result.CreatedBy = _userContext.Email;

        _dbContext.Complaints.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Complaint added successfully to the database");
        return Result.SuccessWithMessage("Complaint added successfully");
    }

}

