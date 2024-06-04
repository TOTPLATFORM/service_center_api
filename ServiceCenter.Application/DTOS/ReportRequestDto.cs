﻿using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ReportRequestDto
{
    [Required]
    public string Task { get; set; } = "";
    [Required]
    public string Priority { get; set; } = "";
    public ReportStatus Status { get; set; } = ReportStatus.Pending;
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public string SalesId { get; set; } = default;
    [Required]
    public string ContactId { get; set; } = default;
    public string ManagerId { get; set; } = default;
}
