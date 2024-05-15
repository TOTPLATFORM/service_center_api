using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Appointment : AuditableEntity
{
    public virtual Schedule Schedule { get; set; }
    public virtual Customer Customer { get; set; }



}

