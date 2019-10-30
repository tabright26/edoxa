// Filename: ServiceResponse.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Api.Responses
{
    [JsonObject]
    public sealed class ServiceResponse
    {
        [JsonProperty("displayed")]
        public bool Displayed { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}
