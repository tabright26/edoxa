// Filename: DivisionPostRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Clans.Requests
{
    [JsonObject]
    public sealed class DivisionPostRequest
    {
        [JsonConstructor]
        public DivisionPostRequest(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public DivisionPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }
    }
}
