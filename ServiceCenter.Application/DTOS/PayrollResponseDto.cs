using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class PayrollResponseDto
{
    public string EmployeeName { get; set; } = "";

    public string EmployeeId { get; set; } = "";

    public DateOnly SalaryDate { get; set; }

    public decimal TotalSalary { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal Bonus { get; set; }

    public decimal Deduction { get; set; }

    public double TotalHoursWorked { get; set; }

    public double StandardHours { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal BonusAmount { get; set; }

    public decimal DeductionAmount { get; set; }
}