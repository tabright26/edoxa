// Filename: MatchViewModel.cs
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
    public class MatchViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("synchronizedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime SynchronizedAt { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }

        [JsonProperty("participantId")]
        public Guid ParticipantId { get; set; }

        [JsonProperty("stats")]
        public StatViewModel[] Stats { get; set; }
    }
}
