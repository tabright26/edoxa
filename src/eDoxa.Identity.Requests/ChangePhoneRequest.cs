// Filename: ChangePhoneRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class ChangePhoneRequest
    {
        [JsonConstructor]
        public ChangePhoneRequest(string number)
        {
            Number = number;
        }

        public ChangePhoneRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("number")]
        public string Number { get; private set; }
    }
}
