using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class LeaveRequestRequestDto
{
    [Required]
    public DateOnly StartDate { get; set; }
    [Required]
    public DateOnly EndDate { get; set; }

    [Required]
    public string Description { get; set; } = "";

    [Required]
    public string EmployeeId { get; set; } = "";

    [Required]
    public int LeaveTypeId { get; set; }

}