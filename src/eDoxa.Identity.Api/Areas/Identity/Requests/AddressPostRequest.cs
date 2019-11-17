// Filename: AddressPostRequest.cs
// Date Created: 2019-08-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public sealed class AddressPostRequest
    {
        public AddressPostRequest(
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            Country = country;
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

#nullable disable
        public AddressPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "country")]
        public Country Country { get; private set; }

        [DataMember(Name = "line1")]
        public string Line1 { get; private set; }

        [DataMember(Name = "line2", IsRequired = false)]
        public string? Line2 { get; private set; }

        [DataMember(Name = "city")]
        public string City { get; private set; }

        [DataMember(Name = "state", IsRequired = false)]
        public string? State { get; private set; }

        [DataMember(Name = "postalCode", IsRequired = false)]
        public string? PostalCode { get; private set; }
    }
}
