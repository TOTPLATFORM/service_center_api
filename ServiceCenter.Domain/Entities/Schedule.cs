using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Schedule :  AuditableEntity
{
	public int? TimeSlotId { get; set; }
    public virtual TimeSlot TimeSlot { get; set; } = default;
    public string?  EmployeeId  { get; set; }
    public virtual Employee Employee { get; set; } = default;
    public int? AppointmentId { get; set; }
    public virtual Appointment Appointment { get; set; } = default;
}
