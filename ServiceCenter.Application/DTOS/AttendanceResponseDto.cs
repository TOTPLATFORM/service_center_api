using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class AttendanceResponseDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = "";
    public DateOnly AttendanceDate { get; set; }
    public TimeOnly ClockInTime { get; set; }
    public TimeOnly ClockOutTime { get; set; }
    public TimeSpan DayHours { get; set; }
    public double TotalHours { get; set; }
}