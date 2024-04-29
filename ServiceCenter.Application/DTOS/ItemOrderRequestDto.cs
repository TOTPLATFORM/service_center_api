using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ItemOrderRequestDto
{
    [Required]
    public int ItemId { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
