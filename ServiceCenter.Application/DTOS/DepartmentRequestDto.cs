using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class DepartmentRequestDto
{
    [Required]
    public string DepartmentName { get; set; } = "";
    public ICollection<string> EmployessId { get; set; }

}
