using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IServiceService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Service  that take  ServiceDto   
    /// </summary>
    /// <param name="ServiceRequestDto">Service  request dto</param>
    /// <returns> Service  added successfully </returns>
    public Task<Result> AddServiceAsync(ServiceRequestDto ServiceRequestDto);
    /// <summary>
    /// function to get all Service  
    /// </summary>
    /// <returns>list all Service  response dto </returns>
    public Task<Result<List<ServiceResponseDto>>> GetAllServiceAsync();
}