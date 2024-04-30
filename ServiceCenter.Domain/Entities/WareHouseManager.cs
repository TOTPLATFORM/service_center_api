using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class WareHouseManager : ApplicationUser
{
    public string PositionTitle { get; set; } = "";
    public DateOnly StartDate { get; set; }
	public DateOnly EndtDate { get; set; }
    public int InventoryId { get; set; }
    public virtual Inventory Inventory { get; set; } = default;


}
