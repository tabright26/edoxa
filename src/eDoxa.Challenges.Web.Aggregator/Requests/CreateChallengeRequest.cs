﻿// Filename: CreateChallengeRequest.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Challenges.Web.Aggregator.Requests
{
    [JsonObject]
    public sealed class CreateChallengeRequest
    {
        [JsonConstructor]
        public CreateChallengeRequest(
            string name,
            string game,
            int bestOf,
            int entries,
            int duration,
            decimal entryFeeAmount,
            string entryFeeCurrency
        )
        {
            Name = name;
            Game = game;
            BestOf = bestOf;
            Entries = entries;
            Duration = duration;
            EntryFeeAmount = entryFeeAmount;
            EntryFeeCurrency = entryFeeCurrency;
        }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("game")]
        public string Game { get; }

        [JsonProperty("bestOf")]
        public int BestOf { get; }

        [JsonProperty("entries")]
        public int Entries { get; }

        [JsonProperty("duration")]
        public int Duration { get; }

        [JsonProperty("entryFeeAmount")]
        public decimal EntryFeeAmount { get; }

        [JsonProperty("entryFeeCurrency")]
        public string EntryFeeCurrency { get; }
    }
}
