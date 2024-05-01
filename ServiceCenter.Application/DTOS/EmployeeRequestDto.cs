using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class EmployeeRequestDto
{
    [Required]
    [EmailAddress]
    public string EmployeeEmail { get; set; } = "";
    [Required]
    public string EmployeeFirstName { get; set; } = "";
    public string? EmployeeLastName { get; set; } 

    [Required]
    [Phone]
    public string EmployeePhoneNumber { get; set; } = "";
    [Required]
    public string UserName { get; set; } = "";
    [PasswordPropertyText]
    public string Password { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; } = "";
    [Required]
    public int DepartmentId { get; set; } 
    public string? ServiceName { get; set; } 
}
