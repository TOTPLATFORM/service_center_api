using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class EmployeeRequestDto : BaseUserRequestDto
{
    [Required]
    public int ContactId { get; set; }
    [Required]
    public int DepartmentId { get; set; } 
}
