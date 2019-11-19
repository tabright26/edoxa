// Filename: ChallengePostRequest.cs
// Date Created: 2019-11-07
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Runtime.Serialization;

namespace eDoxa.Challenges.Requests
{
    [DataContract]
    public sealed class     CreateChallengeRequest
    {
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

#nullable disable
        public CreateChallengeRequest()
        {
            // Required by Fluent Validation.
        }
#nullable restore

        [DataMember(Name = "challengeId")]
        public Guid ChallengeId { get; private set; }

        [DataMember(Name = "name")]
        public string Name { get; private set; }

        [DataMember(Name = "game")]
        public string Game { get; private set; }

        [DataMember(Name = "bestOf")]
        public int BestOf { get; private set; }

        [DataMember(Name = "entries")]
        public int Entries { get; private set; }

        [DataMember(Name = "duration")]
        public int Duration { get; private set; }
    }
}
