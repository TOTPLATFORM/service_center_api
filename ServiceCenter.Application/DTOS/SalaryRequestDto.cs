using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SalaryRequestDto
{
    [Required]
    public string EmployeeId { get; set; }
    [Required]
    public DateOnly SalaryDate { get; set; }
    [Required]
    public decimal Bonus { get; set; }
    [Required]
    public decimal Deduction { get; set; }
}