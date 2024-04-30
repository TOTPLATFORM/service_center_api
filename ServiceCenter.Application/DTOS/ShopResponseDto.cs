using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ShopResponseDto
{
    public int Id { get; set; }
    public string ShopPhoneNumber { get; set; } = "";
    public DateOnly CreatedDate { get; set; }
    public virtual ICollection<ProductResponseDto> Products { get; set; }
}
