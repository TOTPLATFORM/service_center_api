using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ApplicantResponseDto : BaseUserResponseDto
{
    public DateTime ApplicationDate { get; set; }
    public DepartmentResponseDto Department { get; set; }


}