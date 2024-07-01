using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class ContactTest
{
    public static void AddContact(this ServiceCenterBaseDbContext context)
    {
        context.Contacts.AddRange(
        new Contact
        {
            Id = 1,
            DateOfBirth = DateOnly.Parse("2000/12/30"),
            FirstName = "hager",
            LastName = "shaban",
            Gender = Gender.Female,
            Status = ContactStatus.Lead
        },
        new Contact
        {
            Id = 2,
            DateOfBirth = DateOnly.Parse("2000/12/30"),
           FirstName = "hager",
            LastName = "shaban",
            Gender = Gender.Female,
            Status = ContactStatus.Cancelled
        }
        );
    }
}
