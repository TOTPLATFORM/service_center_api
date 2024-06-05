using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ItemCategory : AuditableEntity
{
	public string CategoryName { get; set; } = "";
	
        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
        public virtual ICollection<Inventory> Inventories { get; set; } = new HashSet<Inventory>();
}
