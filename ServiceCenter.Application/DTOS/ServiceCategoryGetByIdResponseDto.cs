using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceCategoryGetByIdResponseDto
{
    public int Id { get; set; }
    public string ServiceCategoryName { get; set; } = "";
    public string ServiceCategoryDescription { get; set; } = "";
    public virtual ICollection<ServiceResponseDto> Services { get; set; } = new HashSet<ServiceResponseDto>();
}
