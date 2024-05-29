using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class   : AuditableEntity
{
	public string PackageName { get; set; } = "";
	public string PackageDescription { get; set; } = "";
    public int PackagePrice { get; set; }
    public virtual ICollection<Service> Services { get; set; } = new HashSet<Service>();
}
