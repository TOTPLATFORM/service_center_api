
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class OrderRequestDto
{
    [Required]
    public string ContactId { get; set; } = "";
    [Required]
    public string OrderStatus { get; set; } = "";
    public ICollection<ItemOrderRequestDto> ItemOrders { get; set; }

}
