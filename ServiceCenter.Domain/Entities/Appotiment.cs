using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appointment : AuditableEntity
{
    public string CustomerId { get; set; } = "";
    public int ScheduleId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; } // E.g., Scheduled, Completed, Canceled
    public string Description { get; set; } = "";
    public virtual Customer Customer { get; set; }
    public virtual Schedule Schedule { get; set; }
}

