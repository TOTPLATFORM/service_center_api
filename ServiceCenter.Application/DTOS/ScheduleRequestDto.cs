using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ScheduleRequestDto
{

    public string? EmployeeId { get; set; } 
    public int? TimeSlotId { get; set; }
    public int? AppotimentId { get; set;}
}
