using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class OrderResponseDto
{
    public int Id { get; set; }
    public string From { get; set; } = "";
    public Status OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime OrderArrivalDate { get; set; }
    public ICollection<ItemOrderResponseDto> ItemOrders { get; set; }
}
