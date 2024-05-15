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
    public string EmployeeId { get; set; } = "";
    [Required]
    public int TimeSlotId { get; set; }
}
