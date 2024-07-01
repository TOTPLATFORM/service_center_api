using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class PerformanceReviewResponseDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = "";
    public DateOnly ReviewDate { get; set; }
    public decimal PerformanceRating { get; set; }
    public string PerformanceDetails { get; set; } = "";
    public string Comments { get; set; } = "";
}