using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceWeeklyScheduleDto
{
    public int ServiceId { get; set; } 
    public string ServiceName { get; set; } = "";
    public DayOfWeek DayOfWeek { get; set; }
    public DateTime Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
