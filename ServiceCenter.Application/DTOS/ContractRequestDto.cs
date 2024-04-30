using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class ContractRequestDto
    {
        [Required]
        public string Duration { get; set; } = "";
        [Required]
        public int ServicePackageId { get; set; }
    }
}
