using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ManagerGetByIdResponseDto 
{
    public string Id { get; set; } = "";
    public string Responsibilities { get; set; } = "";
    public DateOnly HiringDate { get; set; }
    public int WorkingHours { get; set; }
    public int Experience { get; set; }
    public BranchResponseDto Branch { get; set; } = default;
    public EmployeeResponseDto Employee { get; set; } = default;
}
