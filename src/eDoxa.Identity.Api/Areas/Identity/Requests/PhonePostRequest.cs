// Filename: PhonePostRequest.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Api.Areas.Identity.Requests
{
    [DataContract]
    public sealed class PhonePostRequest
    {
        public PhonePostRequest(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; }
    }
}
