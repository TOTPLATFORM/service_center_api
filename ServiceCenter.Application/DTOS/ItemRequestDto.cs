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
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    [Range(1, int.MaxValue)]
    public int Stock { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}
