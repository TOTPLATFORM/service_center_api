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
    public TimeSlot TimeSlot { get; set; }
    public string?  EmployeeId  { get; set; }
    public Employee Employee { get; set; }
}
