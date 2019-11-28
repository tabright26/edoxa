// Filename: AddressPutRequest.cs
// Date Created: 2019-08-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class AddressPutRequest
    {
        public AddressPutRequest(
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

#nullable disable
        public AddressPutRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

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
