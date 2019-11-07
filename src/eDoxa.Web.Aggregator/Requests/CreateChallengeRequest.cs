// Filename: CreateChallengeRequest.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Web.Aggregator.Requests
{
    [DataContract]
    public sealed class CreateChallengeRequest
    {
        public CreateChallengeRequest(
            string name,
            string game,
            int bestOf,
            int entries,
            int duration
        )
        {
            Name = name;
            Game = game;
            BestOf = bestOf;
            Entries = entries;
            Duration = duration;
        }

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
