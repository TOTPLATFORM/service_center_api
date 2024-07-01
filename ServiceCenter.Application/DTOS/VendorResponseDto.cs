using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class VendorResponseDto
{
    public string Id { get; set; } = "";
    public string ContactPerson { get; set; } = "";
    public DateOnly ContractStartDate { get; set; }
    public DateOnly ContractEndDate { get; set; }
     public string Email { get; set; } = "";
    public string UserName { get; set; } = "";
    public ContactResponseDto Contact { get; set; }

}
