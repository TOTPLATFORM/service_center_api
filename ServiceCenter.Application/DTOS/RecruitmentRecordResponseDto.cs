using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RecruitmentRecordResponseDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Status Status { get; set; }
    public string ApplicantName { get; set; } = "";
    public string ApplicantId { get; set; } = "";
    public DepartmentResponseDto Department { get; set; }
}