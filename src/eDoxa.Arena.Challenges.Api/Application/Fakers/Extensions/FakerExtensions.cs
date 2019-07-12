// Filename: FakerExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Fakers.DataSets;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions
{
    public static class FakerExtensions
    {
        private static ICollection<UserId> _testUserIds = ArenaChallengesStorage.TestUserIds.OrderBy(testUserId => testUserId).ToList();

        public static UserId UserId(this Faker faker)
        {
            if (!_testUserIds.Any())
            {
                throw new ApplicationException("There is no longer any test user ID available.");
            }

            var testUserId = faker.PickRandom(_testUserIds);

            _testUserIds.Remove(testUserId);

            return Domain.AggregateModels.UserId.FromGuid(testUserId);
        }

        public static void ResetUserIds()
        {
            _testUserIds = ArenaChallengesStorage.TestUserIds.OrderBy(testUserId => testUserId).ToList();
        }

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
