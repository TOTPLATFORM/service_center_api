using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class TransactionResponseDto
{
    public string TransactionType { get; set; } = "";
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public Status Status { get; set; } = Status.Pending;
    public  InventoryResponseDto Inventory { get; set; } = default;
    public  VendorResponseDto Vendor { get; set; } = default;
}
