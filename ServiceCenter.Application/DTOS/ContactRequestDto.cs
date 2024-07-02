using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTOS;

public class ContactRequestDto 
{
    [Required]
    public string FirstName { get; set; } = "";
    [Required]
    public string LastName { get; set; } = "";
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public Gender Gender { get; set; }
    [Required]
    public Address Address { get; set; } = default;
    [EmailAddress]
    [Required]
	public string Email { get; set; } = "";
    [Phone]
    [Required]
    public string WhatshappNumber { get; set; } = "";


}
