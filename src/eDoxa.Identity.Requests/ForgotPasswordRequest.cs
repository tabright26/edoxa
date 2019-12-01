// Filename: ForgotPasswordRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public class ForgotPasswordRequest
    {
        [JsonConstructor]
        public ForgotPasswordRequest(string email)
        {
            Email = email;
        }

        public ForgotPasswordRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("email")]
        public string Email { get; private set; }
    }
}
