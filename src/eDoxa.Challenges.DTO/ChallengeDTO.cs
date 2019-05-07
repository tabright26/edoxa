﻿// Filename: ChallengeDTO.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Enumerations;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Challenges.DTO
{
    [JsonObject]
    public class ChallengeDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("game")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Game Game { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeType Type { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ChallengeState1 State { get; set; }

        [JsonProperty("setup")]
        public ChallengeSetupDTO Setup { get; set; }

        [JsonProperty("timeline")]
        public ChallengeTimelineDTO Timeline { get; set; }

        [JsonProperty("scoring")]
        public ChallengeScoringDTO Scoring { get; set; }

        [JsonProperty("payout")]
        public ChallengePayoutDTO Payout { get; set; }

        //[JsonProperty("liveData")]
        //public ChallengeLiveDataDTO LiveData { get; set; }        

        [JsonProperty("participants")]
        public ParticipantListDTO Participants { get; set; }
    }
}