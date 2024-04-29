using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
    public class ItemRequestDto
    {
        [Required]
        [Required]
        [Required]
        [Range(1, int.MaxValue)]
        [Required]
        [Range(1, int.MaxValue)]
        [Required]
        public int CategoryId { get; set; }
    }
}
