// Filename: CreateChallengeRequest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Requests
{
    [JsonObject]
    public sealed class CreateChallengeRequest
    {
        [JsonConstructor]
        public CreateChallengeRequest(
            Guid challengeId,
            string name,
            string game,
            int bestOf,
            int entries,
            int duration
        )
        {
            ChallengeId = challengeId;
            Name = name;
            Game = game;
            BestOf = bestOf;
            Entries = entries;
            Duration = duration;
        }

        public CreateChallengeRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("challengeId")]
        public Guid ChallengeId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("game")]
        public string Game { get; private set; }

        [JsonProperty("bestOf")]
        public int BestOf { get; private set; }

        [JsonProperty("entries")]
        public int Entries { get; private set; }

        [JsonProperty("duration")]
        public int Duration { get; private set; }
    }
}
