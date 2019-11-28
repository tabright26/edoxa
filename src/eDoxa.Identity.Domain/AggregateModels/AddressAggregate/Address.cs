// Filename: Address.cs
// Date Created: 2019-11-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Domain.AggregateModels.AddressAggregate
{
    public sealed class Address : Entity<AddressId>, IAggregateRoot
    {
        public Address(
            UserId userId,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            Type = null;
            Country = country;
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            PostalCode = postalCode;
            UserId = userId;
        }

        public AddressType? Type { get; private set; }

        public Country Country { get; private set; }

        public string Line1 { get; private set; }

        public string? Line2 { get; private set; }

        public string City { get; private set; }

        public string? State { get; private set; }

        public string? PostalCode { get; private set; }

        public UserId UserId { get; private set; }

        public void Update(
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            PostalCode = postalCode;
        }
    }
}
