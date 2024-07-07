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
    public string FirstName { get; set; } = "";
    [Required]
    public string LastName { get; set; } = "";
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public Address Address { get; set; } = default;  
    [Required]
    public int DepartmentId { get; set; }
    [Required]
    public decimal BaseSalary { get; set; }
}
