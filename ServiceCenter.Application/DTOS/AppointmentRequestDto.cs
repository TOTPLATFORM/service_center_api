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
    public string ContactId { get; set; } = "";
    [Required]
    public int ScheduleId { get; set; }
    //[Required]
    //public DateTime AppointmentDate { get; set; }
    [Required]
    public string Description { get; set; } = "";
}
