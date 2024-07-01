using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;
public class ExpenseRequestDto
{
    [Required]
    public decimal Value { get; set; }
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public int TransactionId { get; set; }
    [Required]
    public TransactionType TransactionType { get; set; }
}
