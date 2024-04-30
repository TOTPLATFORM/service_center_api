using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class VendorRequestDto;
    {
        [Required]
        public string VendorName { get; set; } = "";
        [Required]
        public string VendorType { get; set; } = "";
        [Required]
        public string ContactPerson { get; set; } = "";
        [Required]
        public DateOnly ContractStartDate { get; set; }
        [Required]
        public DateOnly ContractEndDate { get; set; }
        [Required]
        public int CenterId { get; set; }
    }
}
