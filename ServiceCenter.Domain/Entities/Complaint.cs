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
    public DateOnly ComplaintDate { get; set; }
    public string ComplaintDescription { get; set; } = "";
    public string ComplaintCategory { get; set; } = "";
    public Status ComplaintStatus { get; set; }
    public string AssignedTo { get; set; } = "";
    public string CustomerId { get; set; } = default;
    public virtual Customer Customer { get; set; } = default;
}
