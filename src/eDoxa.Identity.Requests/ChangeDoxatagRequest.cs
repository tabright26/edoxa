// Filename: ChangeDoxatagRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class ChangeDoxatagRequest
    {
        [JsonConstructor]
        public ChangeDoxatagRequest(string name)
        {
            Name = name;
        }

        public ChangeDoxatagRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
