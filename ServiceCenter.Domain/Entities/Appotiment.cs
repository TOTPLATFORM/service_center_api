using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appointment : AuditableEntity
{
    public int ScheduleId {  get; set; }
    public virtual Schedule Schedule { get; set; }
    public int? RatingId { get; set; }
    public virtual Rating Rating { get; set; }
    public int? OrderId { get; set; }
    public virtual Order Order { get; set; }


}

