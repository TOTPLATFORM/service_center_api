using ServiceCenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class ItemOrder : AuditableEntity
{
	public int Quantity { get; set; }
	public int ItemId { get; set; }
	public virtual Item Item { get; set; }
}
