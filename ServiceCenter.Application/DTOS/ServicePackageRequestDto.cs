using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServicePackageRequestDto
{
    [Required]
    public string PackageName { get; set; } = "";
    [Required]
    public string PackageDescription { get; set; } = "";
    [Required]
    [Range(10, int.MaxValue)]
    public int PackagePrice { get; set; }
    public List<int>? ServicesIds { get; set; } 
}
