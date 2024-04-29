using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class ServicePackageRequestDto
    {
        [Required]
        public string PackageName { get; set; } = "";
        [Required]
        public string PackageDescription { get; set; } = "";
        [Required]
        public int PackagePrice { get; set; }
        [Required]
        public string ServiceName { get; set; } = "";
    }
}
