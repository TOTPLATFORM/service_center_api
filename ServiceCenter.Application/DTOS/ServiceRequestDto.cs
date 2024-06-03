using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceRequestDto
{
    [Required]
    public string ServiceName { get; set; } = "";
    [Required]
    public string ServiceDescription { get; set; } = "";
    [Required]
    public int ServicePrice { get; set; }
    [Required]
    public Status Avaliable { get; set; } = Status.Pending;
    [Required]
    public int ServiceCategoryId { get; set; }
    [Required]
    public int CenterId { get; set; }
}
