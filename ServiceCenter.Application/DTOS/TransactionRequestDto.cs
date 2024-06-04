using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class TransactionRequestDto
{
    [Required]
    public string TransactionType { get; set; } = "";
    [Required]
    public int Quantity { get; set; }
    [Required]
    public DateTime TransactionDate { get; set; }
    [Required]
    public Status Status { get; set; } = Status.Pending;
    public int InventoryId { get; set; } 
    public string VendorId { get; set; } = "";
}
