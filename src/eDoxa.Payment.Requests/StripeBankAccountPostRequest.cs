// Filename: StripeBankAccountPostRequest.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Payment.Requests
{
    [DataContract]
    public sealed class StripeBankAccountPostRequest
    {
        public StripeBankAccountPostRequest(string token)
        {
            Token = token;
        }

        [DataMember(Name = "token")]
        public string Token { get; }
    }
}
