// Filename: PhonePostRequest.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class PhonePostRequest
    {
        public PhonePostRequest(string number)
        {
            Number = number;
        }

        [DataMember(Name = "number")]
        public string Number { get; }
    }
}
