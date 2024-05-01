using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IServicePackageService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  ServicePackage  that take  ServicePackageDto   
    /// </summary>
    /// <param name="ServicePackageRequestDto">ServicePackage  request dto</param>
    /// <returns> ServicePackage  added successfully </returns>
    public Task<Result> AddServicePackageAsync(ServicePackageRequestDto ServicePackageRequestDto);
    /// <summary>
    /// function to get all ServicePackage  
    /// </summary>
    /// <returns>list all ServicePackage  response dto </returns>
    public Task<Result<List<ServicePackageResponseDto>>> GetAllServicePackageAsync();
    /// <summary>
    /// function to delete ServicePackage  that take ServicePackage  id   
    /// </summary>
    /// <param name="id">ServicePackage  id</param>
    /// <returns>ServicePackage  removed successfully </returns>
    public Task<Result> DeleteServicePackageAsync(int id);
}
