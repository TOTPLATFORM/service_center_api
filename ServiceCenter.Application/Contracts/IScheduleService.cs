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
    /// <summary>
    /// function to get  Schedule  by id that take   Schedule id
    /// </summary>
    /// <param name="id"> Schedule  id</param>
    /// <returns> Schedule  response dto</returns>
    public Task<Result<ScheduleResponseDto>> GetScheduleByIdAsync(int id);
    /// <summary>
    /// function to update Schedule  that take ScheduleRequestDto   
    /// </summary>
    /// <param name="id">Schedule id</param>
    /// <param name="ScheduleRequestDto">Schedule dto</param>
    /// <returns>Updated Schedule </returns>
    public Task<Result<ScheduleResponseDto>> UpdateScheduleAsync(int id, ScheduleRequestDto ScheduleRequestDto);
    /// <summary>
    /// function to delete Schedule  that take Schedule  id   
    /// </summary>
    /// <param name="id">Schedule  id</param>
    /// <returns>Schedule  removed successfully </returns>
    public Task<Result> DeleteScheduleAsync(int id);
    public Task<Result<List<ScheduleResponseDto>>> GetAllSchedulesForSpecificEmployee(string employeeId);
}
