﻿using ServiceCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class RatingServiceRequestDto
{
    [Required]
    public int RatingValue { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public string CustomerId { get; set; } = "";
}
