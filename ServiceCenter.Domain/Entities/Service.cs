using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Service : AuditableEntity
{
    public string ServiceName { get; set; } = "";
    public string ServiceDescription { get; set; } = "";
    public int ServicePrice { get; set; }
    public Status Avaliable { get; set; }
	public int ServiceCategoryId { get; set; }
    public virtual ServiceCategory ServiceCategory { get; set; } = default;
    public ICollection<PackageService>? PackageServices { get; set; } = new HashSet<PackageService>();
    public string? EmployeeId { get; set; } 
    public virtual Employee Employee { get; set; } = default;

}
