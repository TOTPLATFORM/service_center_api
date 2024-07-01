using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class LeaveRequestResponseDto
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Description { get; set; } = "";
    public string EmployeeId { get; set; } = "";
    public LeaveTypeResponseDto LeaveType { get; set; }

}