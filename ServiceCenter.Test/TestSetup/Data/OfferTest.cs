using Org.BouncyCastle.Asn1.X509;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Test.TestSetup.Data;

public static class OfferTest
{
    public static void AddOffer(this ServiceCenterBaseDbContext context)
    {
        context.Offers.AddRange(
        new Offer
        {
            Id = 1,
            OfferName = "offer1",
            OfferDescription = "offer is Done",
            StartDate = DateOnly.Parse("2000/12/30"),
            EndDate = DateOnly.Parse("2000/12/30"),
            Discount = 20,
            ProductId = 1
        },
        new Offer
        {
            Id = 2,
            OfferName = "offer2",
            OfferDescription = "offer is Done",
            StartDate = DateOnly.Parse("2000/12/30"),
            EndDate = DateOnly.Parse("2000/12/30"),
            Discount = 20,
            ProductId = 1
        }
        );
    }
}
