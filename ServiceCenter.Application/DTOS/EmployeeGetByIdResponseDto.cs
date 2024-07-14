using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class EmployeeGetByIdResponseDto:EmployeeResponseDto
{
    public decimal BaseSalary { get; set; }
}
