// Filename: ChallengePostRequest.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Areas.Challenges.Requests
{
    [DataContract]
    public sealed class ChallengePostRequest
    {
        public ChallengePostRequest(
            string name,
            Game game,
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
        public Game Game { get;  private set; }

        [DataMember(Name = "bestOf")]
        public int BestOf { get; private set; }

        [DataMember(Name = "entries")]
        public int Entries { get; private set; }

        [DataMember(Name = "duration")]
        public int Duration { get; private set; }
    }
}
