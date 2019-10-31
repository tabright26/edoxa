// Filename: GameResponse.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.Api.Areas.Games.Responses
{
    [JsonObject]
    public sealed class GameResponse
    {
        [JsonProperty("normalizedName")]
        public string NormalizedName { get; set; }
    }
}
