using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public string CustomerName { get; set; } = "";
        public string EmployeeName { get; set; } = "";
    }
}
