using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = "";
    public  ICollection<EmployeeResponseDto> Employees { get; set; } =  new HashSet<EmployeeResponseDto>();
    public CenterResponseDto Center { get; set; }
}
