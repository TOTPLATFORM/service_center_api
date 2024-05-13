using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ServiceGetByIdResponseDto : ServiceResponseDto
{
    public ICollection<TimeSlotResponseDto> TimeSlots { get; set; } = new HashSet<TimeSlotResponseDto>();
}
