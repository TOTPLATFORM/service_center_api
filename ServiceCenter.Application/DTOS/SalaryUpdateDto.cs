using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SalaryUpdateDto
{
    [Required]
    public decimal Bonus { get; set; }
    [Required]
    public decimal Deduction { get; set; }
}