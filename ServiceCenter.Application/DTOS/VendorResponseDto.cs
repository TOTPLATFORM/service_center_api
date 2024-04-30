using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class VendorResponseDto
    {
        public int Id { get; set; }
        public string VendorName { get; set; } = "";
        public string VendorType { get; set; } = "";
        public string ContactPerson { get; set; } = "";
        public DateOnly ContractStartDate { get; set; }
        public DateOnly ContractEndDate { get; set; }
        public string CenterName { get; set; } = "";
    }
}
