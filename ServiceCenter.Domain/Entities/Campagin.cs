using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Campagin : AuditableEntity
{
    public string CampaginName { get; set; } = "";
    public string CampaginDescription { get; set; } = "";
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Budget { get; set; }
    public string Goals { get; set; } = "";
    public CampaginStatus Status { get; set; }
    public int CenterId { get; set; }
    public virtual Center Center { get; set; } = default;
    public string ManagerId { get; set; } = default;
    public virtual Manager Manager { get; set; } = default;
}
