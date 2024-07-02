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

public class CustomerRequestDto
{
    public ContactRequestDto Contact { get; set; } = default;
    public BaseUserRequestDto User { get; set; } = default;

}
