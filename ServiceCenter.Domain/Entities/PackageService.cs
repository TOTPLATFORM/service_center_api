using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class PackageService 
{
    public int ServiceId { get; set; }
    public Service Service { get; set; } = default;
    public int ServicePackageId { get; set; }
    public ServicePackage ServicePackage { get; set; } = default;
}
