using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SalesRequestDto
{
    [Required]
    public string SalesEmail { get; set; } = "";
    [Required]
    public string SalesFirstName { get; set; } = "";
    [Required]
    public string SalesLastName { get; set; } = "";
    [Required]
    public string SalesPhoneNumber { get; set; } = "";
    [Required]
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    [Required]
    public int CenterId { get; set; }
}
