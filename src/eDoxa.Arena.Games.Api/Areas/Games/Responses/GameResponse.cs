// Filename: ArenaGameResponse.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Api.Areas.Games.Responses
{
    [JsonObject]
    public sealed class GameResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("imageName")]
        public string ImageName { get; set; }

        [JsonProperty("reactComponent")]
        public string ReactComponent { get; set; }

        [JsonProperty("linked")]
        public bool Linked { get; set; }

        [JsonProperty("services")]
        public IDictionary<string, ServiceResponse> Services { get; set; }
    }
}
