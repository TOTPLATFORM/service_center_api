using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class BranchResponseDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string BranchPhoneNumber { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public string CenterName { get; set; } = "";


      
    }
}
