using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class EmployeeResponseDto
{
    public string Id { get; set; } = "";
    public string EmployeeEmail { get; set; } = "";
    public string EmployeeFirstName { get; set; } = "";
    public string EmployeeLastName { get; set; } = "";
    public string EmployeePhoneNumber { get; set; } = "";
    public string UserName { get; set; } = "";
    public DepartmentResponseDto Department { get; set; }
    //public virtual ICollection<ServiceResponseDto> Services { get; set; }
}
