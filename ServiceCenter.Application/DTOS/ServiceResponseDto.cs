using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceResponseDto
{
    public int Id { get; set; }
    public string ServiceDescription { get; set; } = "";
    public int ServicePrice { get; set; }
    public Status Avaliable { get; set; }
    public string ServiceCategoryName { get; set; } = "";
    public string PackageName { get; set; } = "";
    public string EmployeeName { get; set; } = "";
}
