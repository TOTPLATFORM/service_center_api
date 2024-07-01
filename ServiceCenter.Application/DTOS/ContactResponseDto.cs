﻿using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ContactResponseDto
{
    public string Id { get; set; } 
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Gender { get; set; } = "";

    public string City { get; set; } = "";
    public string Country { get; set; } = "";
    public string PostalCode { get; set; } = "";
    public ContactStatus Status { get; set; }
    public  ICollection<ComplaintResponseDto?> Complaints { get; set; } = new HashSet<ComplaintResponseDto?>();
    public  ICollection<FeedbackResponseDto?> Feedbacks { get; set; } = new HashSet<FeedbackResponseDto?>();
    public  ICollection<RatingResponseDto?> Ratings { get; set; } = new HashSet<RatingResponseDto?>();
}
