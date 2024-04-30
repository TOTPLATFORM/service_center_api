using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class InventoryResponseDto
{
    public int Id { get; set; }
    public string InventoryName { get; set; } = "";
    public string InventoryLocation { get; set; } = "";
    public int InventoryCapacity { get; set; }

    public ICollection<ItemCategoryResponseDto> Categories { get; set; }
}
