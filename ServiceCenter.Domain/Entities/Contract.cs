using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;
    
public class Contract : AuditableEntity
{
    public  string Duration { get; set; } = "";
    public int ServicePackageId { get; set; }
    public virtual ServicePackage ServicePackage { get; set; }
}
