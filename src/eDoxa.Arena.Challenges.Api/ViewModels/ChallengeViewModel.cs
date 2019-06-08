// Filename: ChallengeViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class ChallengeViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("game")]
        public Game Game { get; set; }

        [JsonProperty("state")]
        public ChallengeState State { get; set; }

        [JsonProperty("timeline")]
        public ChallengeTimelineViewModel Timeline { get; set; }

        [JsonProperty("setup")]
        public ChallengeSetupViewModel Setup { get; set; }

        [JsonProperty("scoring")]
        public ScoringViewModel Scoring { get; set; }

        [JsonProperty("payout")]
        public PayoutViewModel Payout { get; set; }

        [JsonProperty("scoreboard")]
        public ScoreboardViewModel Scoreboard { get; set; }

        [JsonProperty("participants")]
        public ParticipantViewModel[] Participants { get; set; }
    }
}
