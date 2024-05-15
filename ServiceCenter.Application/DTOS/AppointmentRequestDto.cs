using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class AppointmentRequestDto
{
    [Required]
    public int ScheduleId { get; set; }
    [Required]
    public string CustomerId { get; set; } = "";
}
