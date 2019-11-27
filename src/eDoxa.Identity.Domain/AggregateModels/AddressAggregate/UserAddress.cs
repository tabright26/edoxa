// Filename: Address.cs
// Date Created: 2019-08-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Domain.Miscs;

#nullable disable

namespace eDoxa.Identity.Domain.AggregateModels.AddressAggregate
{
    public class UserAddress
    {
        public Guid Id { get; set; }

        public UserAddressType Type { get; set; }

        public Country Country { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }

        public Guid UserId { get; set; }
    }
}
