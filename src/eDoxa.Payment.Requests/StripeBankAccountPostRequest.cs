// Filename: StripeBankAccountPostRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Payment.Requests
{
    [JsonObject]
    public sealed class StripeBankAccountPostRequest
    {
        [JsonConstructor]
        public StripeBankAccountPostRequest(string token)
        {
            Token = token;
        }

        public StripeBankAccountPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("token")]
        public string Token { get; private set; }
    }
}
