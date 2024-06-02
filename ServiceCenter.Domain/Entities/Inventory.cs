using ServiceCenter.Core.Entities;

namespace ServiceCenter.Domain.Entities;

public class Inventory : AuditableEntity
{
	public string InventoryName { get; set; } = "";
	public string InventoryLocation { get; set; } = "";
	public int InventoryCapacity { get; set; }
    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }
    public virtual ICollection<Transaction?> Transactions { get; set; } = new HashSet<Transaction>();
    //  public ICollection<InventoryProductBrand> InventoryProductBrands { get; set; }
}
