using ServiceCenter.Core.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Entities;

public class Expense : AuditableEntity
{
    public decimal Value { get; set; }
    public string Description { get; set; } = "";
    public int TransactionId { get; set; }
    public TransactionType TransactionType { get; set; }
}