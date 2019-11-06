// Filename: MatchDto.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Dtos
{
    [JsonObject]
    public sealed class MatchDto
    {
        [JsonConstructor]
        public MatchDto(string gameUuid, IDictionary<string, double> stats)
        {
            GameUuid = gameUuid;
            Stats = stats;
        }

        [JsonProperty("gameUuid")]
        public string GameUuid { get; }

        [JsonProperty("stats")]
        public IDictionary<string, double> Stats { get; }
    }
}
