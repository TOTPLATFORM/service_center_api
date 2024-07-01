using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class AttendanceRequestDto
{
    [Required]
    public DateOnly AttendanceDate { get; set; }
    [Required]
    public TimeOnly ClockInTime { get; set; }
    [Required]
    public TimeOnly ClockOutTime { get; set; }
    [Required]
    public string EmployeeId { get; set; }
}