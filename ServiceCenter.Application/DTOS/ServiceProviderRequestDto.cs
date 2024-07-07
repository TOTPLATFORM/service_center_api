using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceProviderRequestDto:EmployeeRequestDto
{
    [Required]
    public ICollection<int> ServiceIds { get; set; }
}
