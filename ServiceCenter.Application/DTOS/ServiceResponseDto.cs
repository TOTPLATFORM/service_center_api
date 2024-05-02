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

    public string ServiceName { get; set; } = "";
    public string ServiceDescription { get; set; } = "";
    public int ServicePrice { get; set; }
    public Status Avaliable { get; set; }
    public string ServiceCategoryName { get; set; } = "";
	public virtual HashSet<string> LinkedPackages { get; set; } = new HashSet<string>();
	public string EmployeeName { get; set; } = "";
}
