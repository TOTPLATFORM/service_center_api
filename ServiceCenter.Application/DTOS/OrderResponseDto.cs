using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class OrderResponseDto
{
    public int Id { get; set; }
    public ContactResponseDto Contact { get; set; }
    public string OrderStatus { get; set; } = "";
    public DateTime OrderDate { get; set; }
    public ICollection<ProductOrderResponseDto> ItemOrders { get; set; }
    public int TotalPrice { get; set; }

}
