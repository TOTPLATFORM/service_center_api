using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class RecruitmentRecord : AuditableEntity
{
    public int DepartmentId { get; set; }
    public Status Status { get; set; }
    public string ApplicantId { get; set; } = "";
    public virtual Applicant Applicant { get; set; }
    public virtual Department Specialization { get; set; }
}