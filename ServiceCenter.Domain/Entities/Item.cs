using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Item : AuditableEntity
{
	public string ItemName { get; set; } = "";
	public string ItemDescription { get; set; } = "";
	public int ItemStock { get; set; }
	public int ItemPrice { get; set; }
	public virtual ItemCategory Category { get; set; }
    public virtual ICollection<Service?> Services { get; set; } = new HashSet<Service?>();
}
