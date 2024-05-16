using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class VendorResponseDto
{
    public string Id { get; set; } = "";
    public string VendorFirstName { get; set; } = "";
    public string VendorLastName { get; set; } = "";
    public string VendorPhoneNumber { get; set; } = "";
    public string VendorEmail { get; set; } = "";
    public string ContactPerson { get; set; } = "";
    public DateOnly ContractStartDate { get; set; }
    public DateOnly ContractEndDate { get; set; }
    public string CenterName { get; set; } = "";
}
