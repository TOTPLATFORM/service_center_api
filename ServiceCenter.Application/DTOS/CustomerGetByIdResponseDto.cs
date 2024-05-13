using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CustomerGetByIdResponseDto : CustomerResponseDto
{
	public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
	public BranchResponseDto Branch { get; set; } = default;
	public ICollection<ComplaintResponseDto> Complaints { get; set; } = new HashSet<ComplaintResponseDto>();
	public ICollection<FeedbackResponseDto> Feedbacks { get; set; } = new HashSet<FeedbackResponseDto>();

}
