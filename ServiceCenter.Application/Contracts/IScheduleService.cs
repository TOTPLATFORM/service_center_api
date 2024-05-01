using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;

public interface IScheduleService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Schedule  that take  ScheduleDto   
    /// </summary>
    /// <param name="ScheduleRequestDto">Schedule  request dto</param>
    /// <returns> Schedule  added successfully </returns>
    public Task<Result> AddScheduleAsync(ScheduleRequestDto ScheduleRequestDto);
    /// <summary>
    /// function to get all Schedule  
    /// </summary>
    /// <returns>list all Schedule  response dto </returns>
    public Task<Result<List<ScheduleResponseDto>>> GetAllScheduleAsync();
}
