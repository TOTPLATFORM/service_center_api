using Microsoft.AspNetCore.Identity;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CustomerRequestDto : BaseUserRequestDto
{
    [Required]
    [Phone]
    public string WhatsAppNumber { get; set; }
    [Required]
    public string UserName { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";

    [Required]
    public Address Address { get; set; } = default;
}
