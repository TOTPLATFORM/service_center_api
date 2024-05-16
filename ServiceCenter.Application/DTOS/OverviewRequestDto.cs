using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class OverviewRequestDto
{
    [Required]
    public string Task { get; set; } = "";
    [Required]
    public string Priority { get; set; } = "";
    [Required]
    public Status Status { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public string SalesId { get; set; } = default;
}
