using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class BranchRequestDto
{
    [Required]
    public string BranchName { get; set; } = "";
    [Required]
    public Address Address { get; set; }
    [Required]
    [Phone]
    public string BranchPhoneNumber { get; set; } = "";
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = "";
    //public string? ManagerId { get; set; }
    //public int CenterId { get; set; }
    //public int? InventoryId { get; set; }
}
