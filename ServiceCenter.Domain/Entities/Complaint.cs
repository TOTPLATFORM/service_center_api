using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Complaint : AuditableEntity
{
    public string ComplaintDescription { get; set; } = "";
    public Status ComplaintStatus { get; set; }
    public virtual Contact Contact { get; set; } = default;
    public virtual ServiceProvider? ServiceProvider { get; set; } = default;
    public virtual Branch? Branch { get; set; } = default;


}
