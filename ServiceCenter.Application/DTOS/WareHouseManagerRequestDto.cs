using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class WareHouseManagerRequestDto : BaseUserRequestDto
{
    
    [Required]
    public string PositionTitle { get; set; } = "";
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndtDate { get; set; }
    public int? InventoryId { get; set; }
}
