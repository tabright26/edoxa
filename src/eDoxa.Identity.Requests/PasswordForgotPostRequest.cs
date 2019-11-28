// Filename: PasswordForgotPostRequest.cs
// Date Created: 2019-08-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public class PasswordForgotPostRequest
    {
        public PasswordForgotPostRequest(string email)
        {
            Email = email;
        }

#nullable disable
        public PasswordForgotPostRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "email")]
        public string Email { get; private set; }
    }
}
