using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ScheduleRequestDto
{
    [Required]
    public string ServiceProviderId { get; set; } = "";
    [Required]
    public DayOfWeek DayOfWeek { get; set; }
    [Required]
    public TimeOnly StartTime { get; set; }
    [Required]
    public TimeOnly EndTime { get; set; }
    [Required]
    public TimeSpan Duration { get; set; }
}
