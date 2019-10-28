// Filename: ArenaServiceResponse.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Api.Areas.Games.Responses
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
