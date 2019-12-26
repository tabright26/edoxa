// Filename: ChallengeOptions.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

namespace eDoxa.Challenges.Api.Application
{
    public sealed class ChallengeOptions
    {
        public string Name { get; set; }

        public string[] Game { get; set; }

        public int[] BestOf { get; set; }

        public int[] Entries { get; set; }

        public int[] Duration { get; set; }
    }
}
