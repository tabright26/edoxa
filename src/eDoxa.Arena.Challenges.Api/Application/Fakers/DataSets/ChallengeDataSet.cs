// Filename: ChallengeDataSet.cs
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

using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers.DataSets
{
    public class ChallengeDataSet
    {
        public ChallengeDataSet(Faker faker)
        {
            Faker = faker;
        }

        public Faker Faker { get; }

        public ChallengeId Id()
        {
            return ChallengeId.FromGuid(Faker.Random.Guid());
        }

        public ChallengeName Name()
        {
            return new ChallengeName(Faker.PickRandom("Daily Challenge", "Monthly Challenge", "Weekly Challenge"));
        }

        public ChallengeGame Game(ChallengeGame game = null)
        {
            return game ?? Faker.PickRandom(ChallengeGame.GetEnumerations());
        }

        public DateTime CreatedAt(DateTime? startedAt, IEnumerable<DateTime> participantRegistrations)
        {
            return Faker.Date.Recent(1, startedAt.HasValue ? participantRegistrations.Min() : DateTime.UtcNow.DateKeepHours());
        }

        public ChallengeState State(ChallengeState state = null)
        {
            return state ?? Faker.PickRandom(ChallengeState.GetEnumerations());
        }

        public ChallengeTimeline Timeline(ChallengeState state = null)
        {
            state = Faker.Challenge().State(state);

            var duration = Faker.Timeline().Duration();

            var created = Faker.Date.Recent(1, DateTime.UtcNow.DateKeepHours());

            var timeline = new ChallengeTimeline(new DateTimeProvider(created), duration);

            if (state == ChallengeState.InProgress)
            {
                timeline = timeline.Start(new DateTimeProvider(DateTime.UtcNow.DateKeepHours()));
            }

            if (state == ChallengeState.Ended)
            {
                timeline = timeline.Start(new DateTimeProvider(DateTime.UtcNow.DateKeepHours().Subtract(duration)));
            }

            if (state == ChallengeState.Closed)
            {
                timeline = timeline.Close(new DateTimeProvider(DateTime.UtcNow.DateKeepHours()));
            }

            return timeline;
        }
    }
}
