// Filename: GameResponse.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public class GameResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isLinked")]
        public bool IsLinked { get; set; }

        [JsonProperty("isSupported")]
        public bool IsSupported { get; set; }
    }
}
