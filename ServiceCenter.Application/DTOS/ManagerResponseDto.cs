using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ManagerResponseDto
{
    public string Id { get; set; } = "";
    public string ManagerEmail { get; set; } = "";
    public string ManagerFirstName { get; set; } = "";
    public string ManagerLastName { get; set; } = "";
    public string ManagerPhoneNumber { get; set; } = "";
    public string UserName { get; set; } = "";
    public string DepartmentName { get; set; } = "";

}
