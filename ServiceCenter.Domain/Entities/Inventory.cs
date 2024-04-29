using ServiceCenter.Core.Entities;

namespace ServiceCenter.Domain.Entities;

public class Inventory : AuditableEntity
{
	public string InventoryName { get; set; } = "";
	public string InventoryLocation { get; set; } = "";
	public int InventoryCapacity { get; set; }
	public virtual ICollection<ItemCategory> Categories { get; set; } = new HashSet<ItemCategory>();
}
