using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class CustomerResponseDto
{
    public string Id { get; set; } = "";
    public BaseUserResponseDto User { get; set; }
    public  ContactResponseDto Contact { get; set; }
}
