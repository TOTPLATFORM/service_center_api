using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ManagerResponseDto
{
    public string Id { get; set; } = "";
    public string Responsibilities { get; set; } = "";
    public DateOnly HiringDate { get; set; }
    public int WorkingHours { get; set; }
    public int Experience { get; set; }
    public EmployeeResponseDto Employee { get; set; } = default;

}
