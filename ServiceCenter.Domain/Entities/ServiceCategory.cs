using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ServiceCategory : AuditableEntity
{
    public string ServiceCategoryName { get; set; } = "";
    public string ServiceCategoryDescription { get; set; } = "";
    public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
}
