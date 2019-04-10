// Filename: UpdateAddressCommand.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands;

using Stripe;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public class UpdateAddressCommand : Command<Address>
    {
        public UpdateAddressCommand(UserId userId, string city, string country, string line1, string line2, string postalCode, string state)
        {
            UserId = userId;
            City = city;
            Country = country;
            Line1 = line1;
            Line2 = line2;
            PostalCode = postalCode;
            State = state;
        }

        [IgnoreDataMember]
        public UserId UserId { get; set; }

        [IgnoreDataMember]
        public string Name { get; set; }

        [IgnoreDataMember]
        public string Phone { get; set; }

        [DataMember(Name = "city", IsRequired = false)]
        public string City { get; private set; }

        [DataMember(Name = "country", IsRequired = false)]
        public string Country { get; private set; }

        [DataMember(Name = "line1", IsRequired = false)]
        public string Line1 { get; private set; }

        [DataMember(Name = "line2", IsRequired = false)]
        public string Line2 { get; private set; }

        [DataMember(Name = "postalCode", IsRequired = false)]
        public string PostalCode { get; private set; }

        [DataMember(Name = "state", IsRequired = false)]
        public string State { get; private set; }
    }
}