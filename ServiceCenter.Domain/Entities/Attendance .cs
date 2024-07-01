using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Attendance : AuditableEntity
{
    public DateOnly AttendanceDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public TimeOnly ClockInTime { get; set; }
    public TimeOnly ClockOutTime { get; set; }
    public TimeSpan DayHours => ClockOutTime - ClockInTime;
    public double TotalHours => DayHours.TotalHours;
    public string EmployeeId { get; set; } = "";
    public virtual Employee Employee { get; set; }
}