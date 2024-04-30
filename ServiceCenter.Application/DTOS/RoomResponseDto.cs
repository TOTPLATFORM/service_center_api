using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RoomResponseDto
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public bool Availability { get; set; }
    public string CenterName { get; set; } = "";
}
