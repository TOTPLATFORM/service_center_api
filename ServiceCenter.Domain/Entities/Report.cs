using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Report : AuditableEntity
{
    public string Task { get; set; } = "";
    public string Priority { get; set; } = "";
    public ReportStatus Status { get; set; }
    public DateTime DueDate { get; set; }

     public virtual Manager? Manager { get; set; } = default;
    public virtual Customer Customer { get; set; } = default;
    public virtual Sales Sales { get; set; } = default;
}
