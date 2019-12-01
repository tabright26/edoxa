// Filename: ResetPasswordRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class ResetPasswordRequest
    {
        [JsonConstructor]
        public ResetPasswordRequest(string email, string password, string code)
        {
            Email = email;
            Password = password;
            Code = code;
        }

        public ResetPasswordRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("password")]
        public string Password { get; private set; }

        [JsonProperty("code")]
        public string Code { get; private set; }
    }
}
