﻿using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class OverviewResponseDto
{
    public int Id { get; set; }
    public string Task { get; set; } = "";
    public string Priority { get; set; } = "";
    public Status Status { get; set; }
    public DateTime DueDate { get; set; }

    public virtual SalesResponseDto Sales { get; set; } = default;
}
