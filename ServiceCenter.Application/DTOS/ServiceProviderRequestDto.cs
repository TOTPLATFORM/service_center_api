using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceProviderRequestDto:BaseUserRequestDto
{
    [Required]
    public int DepartmentId { get; set; }
    [Required]
    public ICollection<int> ServiceIds { get; set; }
}
