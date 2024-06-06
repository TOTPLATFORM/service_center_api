using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class  ItemRequestDto
{
    [Required]
    public string ItemName { get; set; } = "";
    [Required]
    public string ItemDescription { get; set; } = "";
    [Required]
    [Range(1, int.MaxValue)]
    public int ItemStock { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int ItemPrice { get; set; }
    [Required]
   public int CategoryId { get; set; }

}

