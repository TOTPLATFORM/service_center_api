using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class ItemCategoryRequestDto
    {
        [Required]
        public string CategoryName { get; set; } = "";
        [Required]
        [Range(1, int.MaxValue)]
        public int ReferenceNumber { get; set; }
        [Required]
        public int InventoryId { get; set; }
    }
}
