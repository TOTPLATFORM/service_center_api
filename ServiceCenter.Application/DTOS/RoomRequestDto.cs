using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS
{
    public class RoomRequestDto
    {
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public bool Availability { get; set; }
        [Required]
        public int CenterId { get; set; }
    }
}
