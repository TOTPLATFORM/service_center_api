using ServiceCenter.Application.DTOS;
using ServiceCenter.Core.Result;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Contracts;
public interface IAppointmentService : IApplicationService, IScopedService
{
    /// <summary>
    /// function to add  Appointment  that take  AppointmentDto   
    /// </summary>
    /// <param name="AppointmentRequestDto">Appointment  request dto</param>
    /// <returns> Appointment  added successfully </returns>
    public Task<Result> AddAppointmentAsync(AppointmentRequestDto AppointmentRequestDto);
    /// <summary>
    /// function to get all Appointment  
    /// </summary>
    /// <returns>list all Appointment  response dto </returns>
    public Task<Result<List<AppointmentResponseDto>>> GetAllAppointmentAsync();
}
