using Org.BouncyCastle.Asn1.X509;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Domain.Enums;
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
            OfferName = Category.FreeDelivery,
            OfferDescription = "offer is Done",
            StartDate = DateOnly.Parse("2000/12/30"),
            EndDate = DateOnly.Parse("2000/12/30"),
            Discount = 20,
        },
        new Offer
        {
            Id = 2,
            OfferName = Category.Sales50Percentange,
            OfferDescription = "offer is Done",
            StartDate = DateOnly.Parse("2000/12/30"),
            EndDate = DateOnly.Parse("2000/12/30"),
            Discount = 20,
        }
        );
    }
}
