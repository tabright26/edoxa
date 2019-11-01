// Filename: GameOptions.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    [JsonObject]
    public sealed class GameOptions
    {
        [JsonProperty("games")]
        public Dictionary<string, GameConfig> Games { get; set; }
    }

    [JsonObject]
    public sealed class GameConfig
    {
        [JsonProperty("isDisplay")]
        public bool IsDisplay { get; set; }

        [JsonProperty("reactComponent")]
        public string ReactComponent { get; set; }

        [JsonProperty("services")]
        public Dictionary<string, bool> Services { get; set; }
    }
}
