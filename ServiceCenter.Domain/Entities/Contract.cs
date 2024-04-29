using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;
    
public class Contract : AuditableEntity
{
    public int ServicePackageId { get; set; }
    public ServicePackage ServicePackage { get; set; }
}
