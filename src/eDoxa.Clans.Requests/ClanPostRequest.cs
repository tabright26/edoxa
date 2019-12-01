// Filename: ClanPostRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Clans.Requests
{
    [JsonObject]
    public sealed class ClanPostRequest
    {
        [JsonConstructor]
        public ClanPostRequest(string name, string summary = null)
        {
            Name = name;
            Summary = summary;
        }

        public ClanPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("summary", Required = Required.AllowNull)]
        public string Summary { get; private set; }
    }
}
