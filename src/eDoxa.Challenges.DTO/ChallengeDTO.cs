// Filename: ChallengeDTO.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Challenges.DTO
{
    [JsonObject]
    public class ChallengeDTO
    {
        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("game")] public Game Game { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("type")] public ChallengeType Type { get; set; }

        [JsonProperty("state")] public ChallengeState State { get; set; }

        [JsonProperty("setup")] public ChallengeSetupDTO Setup { get; set; }

        [JsonProperty("timeline")] public ChallengeTimelineDTO Timeline { get; set; }

        [JsonProperty("scoring")] public ChallengeScoringDTO Scoring { get; set; }

        [JsonProperty("payout")] public ChallengePayoutDTO Payout { get; set; }

        [JsonProperty("participants")] public ParticipantListDTO Participants { get; set; }
    }
}