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
}
