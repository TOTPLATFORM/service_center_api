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
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Services;

public class LeaveRequestService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<LeaveRequestService> logger, IUserContextService userContext) : ILeaveRequestService
{

    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<LeaveRequestService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;
    public async Task<Result> AddLeaveRequestAsync(LeaveRequestRequestDto leaveRequestDto)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestDto);

        leaveRequest.CreatedBy = _userContext.Email;
        await _dbContext.LeaveRequests.AddAsync(leaveRequest);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("leaveRequest added successfully to the database");
        return Result.SuccessWithMessage("leaveRequest added successfully");
    }

    public async Task<Result> DeleteLeaveRequestAsync(int id)
    {
        var request = await _dbContext.LeaveRequests.FindAsync(id);

        if (request is null)
        {
            _logger.LogWarning("request Invaild Id ,Id {LeaveRequestId}", id);
            return Result.NotFound(["request Invaild Id"]);
        }

        _dbContext.LeaveRequests.Remove(request);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("request remove successfully in the database");
        return Result.SuccessWithMessage("request removed successfully");
    }

    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestForSpecificEmployee(string employeetId, int itemCount, int index)
    {
        var requests = await _dbContext.LeaveRequests
            .Where(s => s.EmployeeId == employeetId)
            .ProjectTo<LeaveRequestResponseDto>(_mapper.ConfigurationProvider)
            .GetAllWithPagination(itemCount, index);

        if (index > requests.TotalCount)
        {
            requests.End = requests.TotalCount;
        }


        _logger.LogInformation("Fetching requests. Total count: {request}.", requests.TotalCount);
        return Result.Success(requests);
    }

    public async Task<Result<PaginationResult<LeaveRequestResponseDto>>> GetAllLeaveRequestsAsync(int itemCount, int index)
    {
        var requests = await _dbContext.LeaveRequests.ProjectTo<LeaveRequestResponseDto>(_mapper.ConfigurationProvider)
                                                                .GetAllWithPagination(itemCount, index);
        if (index > requests.TotalCount)
        {
            requests.End = requests.TotalCount;
        }

        _logger.LogInformation("Fetching all requests. Total count: {Request}.", requests.TotalCount);
        return Result.Success(requests);

    }

    public async Task<Result<LeaveRequestResponseDto>> GetLeaveRequestByIdAsync(int id)
    {
        var request = await _dbContext.LeaveRequests
           .ProjectTo<LeaveRequestResponseDto>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync(s => s.Id == id);
        if (request is null)
        {
            _logger.LogWarning("request Id not found,Id {LeaveRequestId}", id);
            return Result.NotFound(["request not found"]);
        }
        _logger.LogInformation("Fetching request");
        return Result.Success(request);
    }



    public async Task<Result<LeaveRequestResponseDto>> UpdateLeaveRequestAsync(int id, LeaveRequestRequestDto leaveRequestDto)
    {
        var request = await _dbContext.LeaveRequests.FindAsync(id);

        if (request is null)
        {
            _logger.LogWarning("request Id not found,Id {requestId}", id);
            return Result.NotFound(["request not found"]);
        }

        _mapper.Map(leaveRequestDto, request);
        request.ModifiedBy = _userContext.Email;
        await _dbContext.SaveChangesAsync();

        var requestResponse = _mapper.Map<LeaveRequestResponseDto>(request);

        _logger.LogInformation("Updated request , Id {Id}", id);

        return Result.Success(requestResponse);
    }
}