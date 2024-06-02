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
    public ComplaintType ComplaintCategory { get; set; } 
    public Status ComplaintStatus { get; set; }


}
