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
    public int CenterId { get; set; }
    public virtual Center Center { get; set; } = default;
    public int ServiceCategoryId { get; set; }
    public virtual ServiceCategory ServiceCategory { get; set; } = default;
    public virtual List<ServicePackage> ServicePackages { get; set; } = new List<ServicePackage>();

   // public virtual ICollection<ServiceProviderService> ServiceProviderServices { get; set; }
    public virtual ICollection<Item> Item { get; set; }=new HashSet<Item>();
    public virtual ICollection<Feedback?> Feedbacks { get; set; } = new HashSet<Feedback>();

}
