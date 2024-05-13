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
    public TimeOnly StartTime { get; set; }
    [Required]
    public TimeOnly EndTime { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public string Day { get; set; } = "";
    [Required]
    public string CustomerId { get; set; } = "";
    [Required]
    public string EmployeeId { get; set; } = "";
}
