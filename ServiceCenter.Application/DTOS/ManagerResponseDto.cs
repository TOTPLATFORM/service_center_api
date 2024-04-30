using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ManagerResponseDto
{
    public string Id { get; set; } = "";
    public string DoctorEmail { get; set; } = "";
    public string DoctorFirstName { get; set; } = "";
    public string DoctorLastName { get; set; } = "";
    public string DoctorPhoneNumber { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Responsibilities { get; set; } = "";
    public DateOnly HiringDate { get; set; }
    public int WorkingHours { get; set; }
    public int Experience { get; set; }
    public string DepartmentName { get; set; }
    public virtual ICollection<TimeSlotResponseDto> TimeSlots { get; set; }
}
