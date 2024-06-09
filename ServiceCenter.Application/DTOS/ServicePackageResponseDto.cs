using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServicePackageResponseDto
{
    public int Id { get; set; }
    public string PackageName { get; set; } = "";
    public string PackageDescription { get; set; } = "";
    public int PackagePrice { get; set; }

    //public virtual ICollection<ServiceResponseDto> Services { get; set; } = new HashSet<ServiceResponseDto>();
}
