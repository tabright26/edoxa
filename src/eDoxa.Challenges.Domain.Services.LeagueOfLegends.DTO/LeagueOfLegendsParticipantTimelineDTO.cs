﻿// Filename: LeagueOfLegendsParticipantTimelineDTO.cs
// Date Created: 2019-03-25
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO
{
    public class LeagueOfLegendsParticipantTimelineDTO
    {
        [JsonProperty("lane")]
        public string Lane { get; set; }

        [JsonProperty("participantId")]
        public long ParticipantId { get; set; }

        [JsonProperty("csDiffPerMinDeltas")]
        public Dictionary<string, double> CsDiffPerMinDeltas { get; set; }

        [JsonProperty("goldPerMinDeltas")]
        public Dictionary<string, double> GoldPerMinDeltas { get; set; }

        [JsonProperty("xpDiffPerMinDeltas")]
        public Dictionary<string, double> XpDiffPerMinDeltas { get; set; }

        [JsonProperty("creepsPerMinDeltas")]
        public Dictionary<string, double> CreepsPerMinDeltas { get; set; }

        [JsonProperty("xpPerMinDeltas")]
        public Dictionary<string, double> XpPerMinDeltas { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("damageTakenDiffPerMinDeltas")]
        public Dictionary<string, double> DamageTakenDiffPerMinDeltas { get; set; }

        [JsonProperty("damageTakenPerMinDeltas")]
        public Dictionary<string, double> DamageTakenPerMinDeltas { get; set; }
    }
}