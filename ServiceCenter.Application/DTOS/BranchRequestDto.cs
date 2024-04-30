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
    public string BranchPhoneNumber { get; set; } = "";
    [Required]
    public string EmailAddress { get; set; } = "";
    [Required]
    public int CenterId { get; set; }
    [Required]
    public string CustomerId { get; set; } = "";
    

}
