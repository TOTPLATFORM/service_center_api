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

	private DateTime _orderDate;

	public DateTime OrderDate
	{
		get => _orderDate;
		set
		{
			_orderDate = value;
			OrderArrivalDate = _orderDate.AddDays(2); 
		}
	}
	public DateTime OrderArrivalDate { get; set; }
}
