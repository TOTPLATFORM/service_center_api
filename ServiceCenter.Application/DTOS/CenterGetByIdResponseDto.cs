using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CenterGetByIdResponseDto
{
    public int Id { get; set; }
    public string CenterName { get; set; } = "";
    public int OpeningHours { get; set; }
    public string Specialty { get; set; } = "";
    public ICollection<BranchGetByIdResponseDto> Branches { get; set; } = new HashSet<BranchGetByIdResponseDto>();
    public ICollection<DepartmentResponseDto> Departments { get; set; } = new HashSet<DepartmentResponseDto>();

    public ICollection<ServiceResponseDto> Services { get; set; } = new HashSet<ServiceResponseDto>();
    public ICollection<SalesResponseDto?> Sales { get; set; } = new HashSet<SalesResponseDto?>();
    public ICollection<CampaginResponseDto?> Campagins { get; set; } = new HashSet<CampaginResponseDto?>();
    public ICollection<VendorResponseDto?> Vendors { get; set; } = new HashSet<VendorResponseDto?>();
}
