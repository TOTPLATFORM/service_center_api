using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class PerformanceReviewRequestDto
{
    [Required]
    public DateOnly ReviewDate { get; set; }
    [Required]
    public decimal PerformanceRating { get; set; }
    [Required]
    public string PerformanceDetails { get; set; } = "";
    [Required]
    public string Comments { get; set; } = "";
    [Required]
    public string EmployeeId { get; set; } = "";
}