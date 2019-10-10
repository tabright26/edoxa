// Filename: PersonPostRequest.cs
// Date Created: 2019-10-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Payment.Api.Areas.Stripe.Requests
{
    [DataContract]
    public sealed class PersonPostRequest
    {
        public PersonPostRequest(string token)
        {
            Token = token;
        }

        [DataMember(Name = "token")]
        public string Token { get; }
    }
}
