using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ScheduleResponseDto
{
    public int Id { get; set; }
    public string? EmployeeName { get; set; }

    public int? TimeSlotId { get; set; }
    public int? AppotimentId { get; set; }
}
