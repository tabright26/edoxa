// Filename: FakerExtensions.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions
{
    public static class FakerExtensions
    {
        public static UserDataSet User(this Faker faker)
        {
            return new UserDataSet(faker);
        }

        public static ChallengeDataSet Challenge(this Faker faker)
        {
            return new ChallengeDataSet(faker);
        }

        public static GameDataSet Game(this Faker faker)
        {
            return new GameDataSet(faker);
        }

        public static ParticipantDataSet Participant(this Faker faker)
        {
            return new ParticipantDataSet(faker);
        }

        public static MatchDataSet Match(this Faker faker)
        {
            return new MatchDataSet(faker);
        }
    }
}
