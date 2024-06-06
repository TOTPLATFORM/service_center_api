using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceProviderWeeklyScheduleDto
{
    public string ServiceProviderId { get; set; } = "";
    public string ServiceProviderName { get; set; } = "";
    public DayOfWeek DayOfWeek { get; set; }
    public DateTime Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
