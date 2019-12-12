﻿// Filename: CreateChallengeRequest.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Shared.Enums;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Web.Aggregator.Requests
{
    [JsonObject]
    public sealed class CreateChallengeRequest
    {
        [JsonConstructor]
        public CreateChallengeRequest(
            string name,
            Game game,
            int bestOf,
            int entries,
            int duration,
            double entryFeeAmount,
            Currency entryFeeCurrency
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
        public Game Game { get; }

        [JsonProperty("bestOf")]
        public int BestOf { get; }

        [JsonProperty("entries")]
        public int Entries { get; }

        [JsonProperty("duration")]
        public int Duration { get; }

        [JsonProperty("entryFeeAmount")]
        public double EntryFeeAmount { get; }

        [JsonProperty("entryFeeCurrency")]
        public Currency EntryFeeCurrency { get; }
    }
}
