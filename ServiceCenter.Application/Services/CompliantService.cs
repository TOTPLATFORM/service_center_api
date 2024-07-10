using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mysqlx.Crud;
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
        result.Branch = null;
        result.ServiceProvider = null;
        if (ComplaintRequestDto.BranchId > 1)
        {
            var Branch = await _dbContext.Branches.FirstOrDefaultAsync(o => o.Id == ComplaintRequestDto.BranchId);
            result.Branch = Branch;
        }
        if (ComplaintRequestDto.ServiceProviderId == null)
        {
            var serviceProvider = await _dbContext.ServiceProviders.FirstOrDefaultAsync(o => o.Id == ComplaintRequestDto.ServiceProviderId);
            result.ServiceProvider = serviceProvider;
        }
        var contact = await _dbContext.Customers.FirstOrDefaultAsync(m => m.Id == ComplaintRequestDto.CustomerId);

        result.CreatedBy = _userContext.Email;
        result.Customer = contact;

        _dbContext.Complaints.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Complaint added successfully to the database");
        return Result.SuccessWithMessage("Complaint added successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetAllComplaintsAsync(int itemCount, int index)
    {
        var result = await _dbContext.Complaints
             .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching all  Complaint. Total count: { Complaint}.", result.Data.Count);

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ComplaintResponseDto>> GetComplaintByIdAsync(int id)
    {
        var result = await _dbContext.Complaints
            .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Complaint Id not found,Id {ComplaintId}", id);

            return Result.NotFound(["Complaint not found"]);
        }

        _logger.LogInformation("Fetching Complaint");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ComplaintResponseDto>> UpdateComplaintStatusAsync(int id, Status complaintStatus)
    {
        var result = await _dbContext.Complaints.FindAsync(id);
        if (result is null)
        {
            _logger.LogWarning("Complaint Id not found,Id {ComplaintId}", id);
            return Result.NotFound(["Complaint not found"]);
        }
        result.ComplaintStatus = complaintStatus;

        result.ModifiedBy = _userContext.Email;

        await _dbContext.SaveChangesAsync();
        var ComplaintResponse = _mapper.Map<ComplaintResponseDto>(result);
        if (ComplaintResponse is null)
        {
            _logger.LogError("Failed to map ComplaintRequestDto to ComplaintResponseDto. ComplaintRequestDto: {@ComplaintRequestDto}", ComplaintResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Complaint , Id {Id}", id);

        return Result.Success(ComplaintResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteComplaintAsync(int id)
    {
        var Complaint = await _dbContext.Complaints.FindAsync(id);

        if (Complaint is null)
        {
            _logger.LogWarning("Complaint Invaild Id ,Id {ComplaintId}", id);
            return Result.NotFound(["Complaint Invaild Id"]);
        }

        _dbContext.Complaints.Remove(Complaint);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Complaint removed successfully in the database");
        return Result.SuccessWithMessage("Complaint removed successfully");
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsForSpecificCustomerAsync(string customerId, int itemCount, int index)
    {
        var Complaints = await _dbContext.Complaints
              .Where(s => s.Customer.Id == customerId)
              .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
              .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Complaints. Total count: {Complaints}.", Complaints.Data.Count);
        return Result.Success(Complaints);
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsForSpecificBranchAsync(int branchId, int itemCount, int index)
    {
        var Complaints = await _dbContext.Complaints
         .Where(s => s.Branch.Id == branchId)
         .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
         .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Complaints. Total count: {Complaints}.", Complaints.Data.Count);
        return Result.Success(Complaints);
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> GetComplaintsForSpecificServiceProviderAsync(string serviceProviderId, int itemCount, int index)
    {
        var Complaints = await _dbContext.Complaints
               .Where(s => s.ServiceProvider.Id == serviceProviderId)
               .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
                .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Complaints. Total count: {Complaints}.", Complaints.Data.Count);
        return Result.Success(Complaints);
    }
    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ComplaintResponseDto>>> SearchComplaintByStatusAsync(Status text, int itemCount, int index)
    {
        var Complaints = await _dbContext.Complaints
         .Where(s => s.ComplaintStatus == text)
         .ProjectTo<ComplaintResponseDto>(_mapper.ConfigurationProvider)
         .GetAllWithPagination(itemCount, index);

        _logger.LogInformation("Fetching Complaints. Total count: {Complaints}.", Complaints.Data.Count);
        return Result.Success(Complaints);
    }
}