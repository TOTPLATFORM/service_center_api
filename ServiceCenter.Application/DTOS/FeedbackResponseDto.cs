﻿using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class FeedbackResponseDto
{
    public int Id { get; set; }
    public DateTime FeedbackDate { get; set; }
    public string FeedbackDescription { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public  ServiceResponseDto? Service { get; set; } = default;
    public  ProductResponseDto? Product { get; set; } = default;


}
