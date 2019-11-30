// Filename: UpdateProfileRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class UpdateProfileRequest
    {
        [JsonConstructor]
        public UpdateProfileRequest(string firstName)
        {
            FirstName = firstName;
        }

        public UpdateProfileRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("firstName")]
        public string FirstName { get; private set; }
    }
}
