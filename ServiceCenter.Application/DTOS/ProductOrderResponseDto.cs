using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ProductOrderResponseDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public ProductResponseDto Product { get; set; }
}

