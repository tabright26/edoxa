// Filename: MatchDto.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Dtos
{
    [JsonObject]
    public sealed class MatchDto
    {
        [JsonConstructor]
        public MatchDto(string reference, DateTime timestamp, IDictionary<string, double> stats)
        {
            Reference = reference;
            Timestamp = timestamp;
            Stats = stats;
        }

        [JsonProperty("reference")]
        public string Reference { get; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; }

        [JsonProperty("stats")]
        public IDictionary<string, double> Stats { get; }
    }
}
