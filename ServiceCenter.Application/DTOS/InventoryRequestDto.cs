using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class InventoryRequestDto
{
    [Required]
    public string InventoryName { get; set; } = "";
    [Required]
    public string InventoryLocation { get; set; } = "";
    [Required]
    public int InventoryCapacity { get; set; }
    [Required]
    public string ManagerId { get; set; } = "";
}
