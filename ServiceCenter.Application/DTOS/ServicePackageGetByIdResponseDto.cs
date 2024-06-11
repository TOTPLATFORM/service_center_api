using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public  class ServicePackageGetByIdResponseDto
{
    public int Id { get; set; }
    public string PackageName { get; set; } = "";
    public string PackageDescription { get; set; } = "";
    public int PackagePrice { get; set; }
}
