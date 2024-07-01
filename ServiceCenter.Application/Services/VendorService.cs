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

public class VendorService(ServiceCenterBaseDbContext dbContext, IMapper mapper, ILogger<VendorService> logger, IUserContextService userContext, IAuthService authService) : IVendorService
{
    private readonly ServiceCenterBaseDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<IVendorService> _logger = logger;
    private readonly IUserContextService _userContext = userContext;
    private readonly IAuthService _authService = authService;

    ///<inheritdoc/>
    public async Task<Result> AddVendorAsync(VendorRequestDto vendorRequestDto)
    {

        string role = "Vendor";

        var vendor = _mapper.Map<Vendor>(vendorRequestDto);

        var center = await _dbContext.Centers.FirstOrDefaultAsync();

        vendor.Center = center;

        var vendorAdded = await _authService.RegisterUserWithRoleAsync(vendor, vendorRequestDto.Password, role);

        if (!vendorAdded.IsSuccess)
        {
            return Result.Error(vendorAdded.Errors.FirstOrDefault());
        }

      
        _logger.LogInformation("Vendor added successfully in the database");

        return Result.SuccessWithMessage("Vendor added successfully");

    }

    public async Task<Result<PaginationResult<VendorResponseDto>>> GetAllVendorsAsync(int itemCount,int index)
    {
        var vendor = await _dbContext.Users.OfType<Vendor>()
                  .ProjectTo<VendorResponseDto>(_mapper.ConfigurationProvider)
                  .GetAllWithPagination(itemCount,index);
        _logger.LogInformation("Fetching all vendor. Total count: {vendor}.", vendor.Data.Count);
        return Result.Success(vendor);
    }

    ///<inheritdoc/>
    public async Task<Result<VendorResponseDto>> GetVendorByIdAsync(string Id)
    {
        var vendor = await _dbContext.Vendors
            .ProjectTo<VendorResponseDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(d => d.Id == Id);

        if (vendor is null)
        {
            _logger.LogWarning("vendor Id not found,Id {vendorId}", Id);
            return Result.NotFound(["vendor not found"]);
        }
        _logger.LogInformation("Fetching vendor");

        return Result.Success(vendor);
    }

    ///<inheritdoc/>
    public async Task<Result<VendorResponseDto>> UpdateVendorAsync(string id, VendorRequestDto vendorRequestDto)
    {
        var vendor = await _dbContext.Vendors.FindAsync(id);

        if (vendor is null)
        {
            _logger.LogWarning("vendor Id not found,Id {vendorId}", id);

            return Result.NotFound(["vendor not found"]);
        }

        _mapper.Map(vendorRequestDto, vendor);

        await _dbContext.SaveChangesAsync();

        var vendorResponse = _mapper.Map<VendorResponseDto>(vendor);

        if (vendorResponse is null)
        {
            _logger.LogError("Failed to map vendorRequestDto to vendorResponseDto. vendorRequestDto: {@vendorRequestDto}", vendorRequestDto);

            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }

        _logger.LogInformation("Updated vendor , Id {Id}", id);

        return Result.Success(vendorResponse);
    }

    ///<inheritdoc/>
    public async Task<Result<PaginationResult<VendorResponseDto>>> SearchVendorByTextAsync(string text,int itemcount, int index)
    {
        var vendor = await _dbContext.Vendors
                       .ProjectTo<VendorResponseDto>(_mapper.ConfigurationProvider)
                       .Where(n => n.Contact.FirstName.Contains(text))
                       .GetAllWithPagination(itemcount,index);

        _logger.LogInformation("Fetching search branch by name . Total count: {branch}.", vendor.Data.Count);

        return Result.Success(vendor);
    }

   
}