// Filename: ChallengeViewModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class ChallengeViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("synchronizedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? SynchronizedAt { get; set; }

        [JsonProperty("timeline")]
        public ChallengeTimelineViewModel Timeline { get; set; }

        [JsonProperty("setup")]
        public ChallengeSetupViewModel Setup { get; set; }

        [JsonProperty("scoring")]
        public ScoringViewModel Scoring { get; set; }

        [JsonProperty("payout")]
        public PayoutViewModel Payout { get; set; }

        [JsonProperty("participants")]
        public ParticipantViewModel[] Participants { get; set; }
    }
}
