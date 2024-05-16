using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Overview : AuditableEntity
{
    public string Task { get; set; } = "";
    public string Priority { get; set; } = "";
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }

    public required string SalesId { get; set; }
    public virtual Sales Sales { get; set; } = default;
}
