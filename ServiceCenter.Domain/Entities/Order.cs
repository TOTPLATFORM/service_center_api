using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Order : AuditableEntity
{
	public string From { get; set; } = "";
	public Status OrderStatus { get; set; }
	public DateTime OrderDate { get; set; } = DateTime.Now;
	public DateTime OrderArrivalDate { get; set; }
	public virtual ICollection<ItemOrder> ItemOrders { get; set; } = new HashSet<ItemOrder>();
}
