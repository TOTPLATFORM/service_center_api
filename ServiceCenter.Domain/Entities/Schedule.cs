using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Schedule :  AuditableEntity
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string ServiceProviderId { get; set; } = "";
    public virtual ServiceProvider ServiceProvider { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; }
}
