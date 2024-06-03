//using AutoMapper;
//using Microsoft.Extensions.Logging;
//using ServiceCenter.Application.Reports;
//using ServiceCenter.Application.DTOS;
//using ServiceCenter.Core.Result;
//using ServiceCenter.Infrastructure.BaseContext;
//using ServiceCenter.Application.Contracts;
//using ServiceCenter.Domain.Entities;
//using AutoMapper.QueryableExtensions;
//using Microsoft.EntityFrameworkCore;

//namespace ServiceCenter.Application.Services;

//public class ReportService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<ReportService> logger, IUserContextService userContext) : IReportService
//{
//    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
//    private readonly IMapper _mapper = mapper;
//    private readonly ILogger<ReportService> _logger = logger;
//    private readonly IUserContextService _userContext = userContext;

//    ///<inheritdoc/>
//    public async Task<Result> AddReportAsync(ReportRequestDto ReportRequestDto)
//    {
//        var sales = await _dbContext.Users.OfType<Sales>().FirstOrDefaultAsync(s => s.Id == ReportRequestDto.SalesId);
//        var result = _mapper.Map<Report>(ReportRequestDto);
//        if (result is null)
//        {
//            _logger.LogError("Failed to map ReportRequestDto to Report. ReportRequestDto: {@ReportRequestDto}", ReportRequestDto);
//            return Result.Invalid(new List<ValidationError>
//            {
//                new ValidationError
//                    {
//                        ErrorMessage = "Validation Errror"
//                    }
//            });
//        }
//        result.CreatedBy = _userContext.Email;

//        _dbContext.Reports.Add(result);

//        await _dbContext.SaveChangesAsync();
//        _logger.LogInformation("Report added successfully to the database");
//        return Result.SuccessWithMessage("Report added successfully");
//    }

//    ///<inheritdoc/>
//    public async Task<Result<List<ReportResponseDto>>> GetAllReportAsync()
//    {
//        var result = await _dbContext.Reports
//             .ProjectTo<ReportResponseDto>(_mapper.ConfigurationProvider)
//             .ToListAsync();

//        _logger.LogInformation("Fetching all  Report. Total count: { Report}.", result.Count);

//        return Result.Success(result);
//    }

//    ///<inheritdoc/>
//    public async Task<Result<ReportResponseDto>> GetReportByIdAsync(int id)
//    {
//        var result = await _dbContext.Reports
//            .ProjectTo<ReportResponseDto>(_mapper.ConfigurationProvider)
//            .FirstOrDefaultAsync(p => p.Id == id);

//        if (result is null)
//        {
//            _logger.LogWarning("Report Id not found,Id {ReportId}", id);

//            return Result.NotFound(["Report not found"]);
//        }

//        _logger.LogInformation("Fetching Report");

//        return Result.Success(result);
//    }
//    ///<inheritdoc/>
//    public async Task<Result<ReportResponseDto>> UpdateReportAsync(int id, ReportRequestDto ReportRequestDto)
//    {
//        var result = await _dbContext.Reports.FindAsync(id);

//        if (result is null)
//        {
//            _logger.LogWarning("Report Id not found,Id {ReportId}", id);
//            return Result.NotFound(["Report not found"]);
//        }

//        result.ModifiedBy = _userContext.Email;

//        _mapper.Map(ReportRequestDto, result);

//        await _dbContext.SaveChangesAsync();

//        var ReportResponse = _mapper.Map<ReportResponseDto>(result);
//        if (ReportResponse is null)
//        {
//            _logger.LogError("Failed to map ReportRequestDto to ReportResponseDto. ReportRequestDto: {@ReportRequestDto}", ReportResponse);

//            return Result.Invalid(new List<ValidationError>
//        {
//                new ValidationError
//                {
//                    ErrorMessage = "Validation Errror"
//                }
//        });
//        }

//        _logger.LogInformation("Updated Report , Id {Id}", id);

//        return Result.Success(ReportResponse);
//    }
//    ///<inheritdoc/>
//    public async Task<Result> DeleteReportAsync(int id)
//    {
//        var Report = await _dbContext.Reports.FindAsync(id);

//        if (Report is null)
//        {
//            _logger.LogWarning("Report Invaild Id ,Id {ReportId}", id);
//            return Result.NotFound(["Report Invaild Id"]);
//        }

//        _dbContext.Reports.Remove(Report);
//        await _dbContext.SaveChangesAsync();
//        _logger.LogInformation("Report removed successfully in the database");
//        return Result.SuccessWithMessage("Report removed successfully");
//    }


//}