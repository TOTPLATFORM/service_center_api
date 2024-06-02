﻿using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class SalesResponseDto
{
    public string Id { get; set; } = "";
    public string SalesEmail { get; set; } = "";
    public string SalesFirstName { get; set; } = "";
    public string SalesLastName { get; set; } = "";
    public string SalesPhoneNumber { get; set; } = "";
    public string UserName { get; set; } = "";
    public virtual ICollection<ProductResponseDto> Products { get; set; }
    public virtual ICollection<ReportResponseDto> Overviews { get; set; }
}
