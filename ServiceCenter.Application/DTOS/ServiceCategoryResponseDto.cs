using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceCategoryResponseDto
{
    public int Id { get; set; }
    public string ServiceCategoryName { get; set; } = "";
    public string ServiceCategoryDescription { get; set; } = "";
    public ICollection<ServiceResponseDto> Services { get; set; }
}
