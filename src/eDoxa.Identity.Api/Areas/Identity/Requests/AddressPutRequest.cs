// Filename: AddressPutRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public class AddressPutRequest
    {
        public AddressPutRequest(
            string street,
            string city,
            string postalCode,
            string country
        )
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }

        [DataMember(Name = "street")]
        public string Street { get; private set; }

        [DataMember(Name = "city")]
        public string City { get; private set; }

        [DataMember(Name = "postalCode")]
        public string PostalCode { get; private set; }

        [DataMember(Name = "country")]
        public string Country { get; private set; }
    }
}
