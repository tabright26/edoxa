// Filename: FakerExtensions.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Arena.Challenges.Domain.Fakers.DataSets;

namespace eDoxa.Arena.Challenges.Domain.Fakers.Extensions
{
    public static class FakerExtensions
    {
        public static ChallengeDataSet Challenge(this Faker faker)
        {
            return new ChallengeDataSet(faker);
        }

        public static ChallengeTimelineDataSet Timeline(this Faker faker)
        {
            return new ChallengeTimelineDataSet(faker);
        }

        public static ChallengeSetupDataSet Setup(this Faker faker)
        {
            return new ChallengeSetupDataSet(faker);
        }

        public static MatchDataSet Match(this Faker faker)
        {
            return new MatchDataSet(faker);
        }

        public static ParticipantDataSet Participant(this Faker faker)
        {
            return new ParticipantDataSet(faker);
        }
    }
}
