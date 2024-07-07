using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class EmployeeGetByIdResponseDto:EmployeeResponseDto
{
    public decimal BaseSalary { get; set; }
    public string Gender { get; set; } = "";
    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string PostalCode { get; set; } = "";
}
