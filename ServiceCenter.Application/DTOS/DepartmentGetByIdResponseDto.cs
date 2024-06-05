using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class DepartmentGetByIdResponseDto
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = "";
}
