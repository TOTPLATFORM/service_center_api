using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ManagerRequestDto : BaseUserRequestDto
{ 
    public string Responsibilities { get; set; } = "";
    [Required]
    public DateOnly HiringDate { get; set; }
    [Required]
    public int WorkingHours { get; set; }
    [Required]
    public int Experience { get; set; }
    [Required]
    public int BranchId { get; set; }
    [Required]
    public int DepartmentId { get; set; }
}
