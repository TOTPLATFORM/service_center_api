using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CenterGetByIdResponseDto:CenterResponseDto
{
    public ICollection<BranchResponseDto> Branches { get; set; } = new HashSet<BranchResponseDto>();
    public ICollection<DepartmentResponseDto> Departments { get; set; } = new HashSet<DepartmentResponseDto>();

    public ICollection<ServiceResponseDto> Services { get; set; } = new HashSet<ServiceResponseDto>();
    public ICollection<SalesResponseDto?> Sales { get; set; } = new HashSet<SalesResponseDto?>();
    public ICollection<CampaginResponseDto?> Campagins { get; set; } = new HashSet<CampaginResponseDto?>();
    public ICollection<VendorResponseDto?> Vendors { get; set; } = new HashSet<VendorResponseDto?>();
}
