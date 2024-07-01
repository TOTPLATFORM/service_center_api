using AutoMapper;
using Microsoft.Extensions.Logging;
using ServiceCenter.Application.Reports;
using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Infrastructure.BaseContext;
using ServiceCenter.Application.Contracts;
using ServiceCenter.Domain.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Application.ExtensionForServices;
using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ServiceCenter.Application.Services;

public class ReportService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ReportService> logger, IUserContextService userContext) : IReportService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<ReportService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;

    ///<inheritdoc/>
    public async Task<Result> AddReportAsync(ReportRequestDto ReportRequestDto)
    {
        var sales = await _dbContext.Sales.FirstOrDefaultAsync(s => s.Id == ReportRequestDto.SalesId);
        var Manager = await _dbContext.Managers.FirstOrDefaultAsync(s => s.Id == ReportRequestDto.ManagerId);
        var Contact = await _dbContext.Contacts.FirstOrDefaultAsync(s => s.Id == ReportRequestDto.ContactId);
        var result = _mapper.Map<Report>(ReportRequestDto);
        if (result is null)
        {
            _logger.LogError("Failed to map ReportRequestDto to Report. ReportRequestDto: {@ReportRequestDto}", ReportRequestDto);
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
            });
        }
        result.CreatedBy = _userContext.Email;
        result.Manager=Manager;
        result.Contact = Contact;
        result.Sales = sales;

        _dbContext.Reports.Add(result);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Report added successfully to the database");
        return Result.SuccessWithMessage("Report added successfully");
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<ReportResponseDto>>> GetAllReportAsync(int itemCount, int index)
    {
        var result = await _dbContext.Reports
             .ProjectTo<ReportResponseDto>(_mapper.ConfigurationProvider)
             .GetAllWithPagination(itemCount,index);

        _logger.LogInformation("Fetching all  Report. Total count: { Report}.", result.Data.Count);

        return Result.Success(result);
    }

    ///<inheritdoc/>
    public async Task<Result<ReportResponseDto>> GetReportByIdAsync(int id)
    {
        var result = await _dbContext.Reports
            .ProjectTo<ReportResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (result is null)
        {
            _logger.LogWarning("Report Id not found,Id {ReportId}", id);

            return Result.NotFound(["Report not found"]);
        }

        _logger.LogInformation("Fetching Report");

        return Result.Success(result);
    }
    ///<inheritdoc/>
    public async Task<Result<ReportResponseDto>> UpdateReportAsync(int id, string task)
    {
        var result = await _dbContext.Reports.FindAsync(id);

        if (result is null)
        {
            _logger.LogWarning("Report Id not found,Id {ReportId}", id);
            return Result.NotFound(["Report not found"]);
        }
        result.Task= task;
        result.ModifiedBy = _userContext.Email;
         await _dbContext.SaveChangesAsync();

        var ReportResponse = _mapper.Map<ReportResponseDto>(result);
        if (ReportResponse is null)
        {
            _logger.LogError("Failed to map ReportRequestDto to ReportResponseDto. ReportRequestDto: {@ReportRequestDto}", ReportResponse);

            return Result.Invalid(new List<ValidationError>
        {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
        });
        }

        _logger.LogInformation("Updated Report , Id {Id}", id);

        return Result.Success(ReportResponse);
    }
    ///<inheritdoc/>
    public async Task<Result> DeleteReportAsync(int id)
    {
        var Report = await _dbContext.Reports.FindAsync(id);

        if (Report is null)
        {
            _logger.LogWarning("Report Invaild Id ,Id {ReportId}", id);
            return Result.NotFound(["Report Invaild Id"]);
        }

        _dbContext.Reports.Remove(Report);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Report removed successfully in the database");
        return Result.SuccessWithMessage("Report removed successfully");

    }

    ///<inheritdoc/>
    public async Task<Result<ReportResponseDto>> UpdateReportStatusAsync(int id, ReportStatus status)
    {
        var Report = await _dbContext.Reports.FindAsync(id);

        if (Report is null)
        {
            _logger.LogWarning($"Report with id {id} was not found while attempting to update Report status by id");
            return Result.NotFound(["The Report is not found"]);
        }

        Report.Status = status;
        var role =await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");


        var contact = await _dbContext.Contacts.FindAsync(Report.Contact.Id);
        if (status == ReportStatus.Good)
        {
            contact.Status = ContactStatus.Customer;
            var roleUser= await _dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == contact.Id);
            var roleUserNew = new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = contact.Id,
            };
            _dbContext.UserRoles.Remove(roleUser);
            _dbContext.UserRoles.Add(roleUserNew);
        }
        if (status == ReportStatus.Bad)
        {
             contact.Status = ContactStatus.Cancelled;
        }
        Report.ModifiedBy = _userContext.Email;
        _dbContext.Contacts.Update(contact);
        _dbContext.Reports.Update(Report);
        await _dbContext.SaveChangesAsync();
        var reportResponseDto = _mapper.Map<ReportResponseDto>(Report);
        _logger.LogInformation($"Successfully update report status to: {Report.Status}");
        return Result.Success(reportResponseDto, "Successfully updated report");
    }
}