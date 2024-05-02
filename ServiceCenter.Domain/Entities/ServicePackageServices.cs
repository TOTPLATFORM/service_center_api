using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

[NotMapped]
public class ServicePackageServices 
{
    public int ServiceId { get; set; }
    public virtual Service Service { get; set; } = default;
    public int ServicePackageId { get; set; }
    public virtual ServicePackage ServicePackage { get; set; } = default;
}
