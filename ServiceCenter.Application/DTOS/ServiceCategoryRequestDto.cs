using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceCategoryRequestDto
{
    [Required]
    public string ServiceCategoryName { get; set; } = "";
    [Required]
    public string ServiceCategoryDescription { get; set; } = "";
    [Required]
    public string ServiceName { get; set; } = "";
}
