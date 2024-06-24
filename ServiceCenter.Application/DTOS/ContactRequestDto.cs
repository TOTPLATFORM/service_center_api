using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ContactRequestDto : BaseUserRequestDto
{
	public Address Address { get; set; } = default;
    public ContactStatus Status { get; set; } =  ContactStatus.Opportunity;

}
