using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceProviderGetByIdResponseDto:EmployeeGetByIdResponseDto
{
    public string Id { get; set; } = "";
    public virtual ICollection<ServiceResponseDto> Services { get; set; }
    public virtual ICollection<ScheduleResponseDto> Schedules { get; set; }
    public virtual ICollection<ComplaintResponseDto> Complaints { get; set; }
}
