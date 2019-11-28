// Filename: PasswordResetPostRequest.cs
// Date Created: 2019-08-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Identity.Requests
{
    [DataContract]
    public sealed class ResetPasswordRequest
    {
        public ResetPasswordRequest(string email, string password, string code)
        {
            Email = email;
            Password = password;
            Code = code;
        }

#nullable disable
        public ResetPasswordRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "email")]
        public string Email { get; private set; }

        [DataMember(Name = "password")]
        public string Password { get; private set; }

        [DataMember(Name = "code")]
        public string Code { get; private set; }
    }
}
