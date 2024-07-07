using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
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
    public int DepartmentId { get; set; }
    [Required]
    public decimal BaseSalary { get; set; }

    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
