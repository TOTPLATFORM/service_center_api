using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class ShopRequestDto
    {
        [Required]
        public string ShopPhoneNumber { get; set; } = "";
        [Required]
        public DateOnly CreatedDate { get; set; }
        public string?  ProductName { get; set; }
    }
}
