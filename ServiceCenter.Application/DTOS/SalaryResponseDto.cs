using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SalaryResponseDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = "";
    public DateOnly SalaryDate { get; set; }
    public decimal Bonus { get; set; }
    public decimal Deduction { get; set; }
}