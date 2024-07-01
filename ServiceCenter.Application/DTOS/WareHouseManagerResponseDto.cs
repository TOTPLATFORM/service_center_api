using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class WareHouseManagerResponseDto
{
    public string Id { get; set; }
    public string PositionTitle { get; set; } = "";
    public DateOnly StartDate { get; set; }
    public DateOnly EndtDate { get; set; }
    public string InventoryName { get; set; } = "";
    public EmployeeResponseDto Employee { get; set; } = default;
}
