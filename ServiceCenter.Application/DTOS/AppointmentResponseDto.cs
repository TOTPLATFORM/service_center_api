using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class AppointmentResponseDto
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Description { get; set; } = "";
    public ContactResponseDto Contact { get; set; }
    public ServiceProviderResponseDto ServiceProvider { get; set; }
    public int ScheduleId { get; set; }
}
