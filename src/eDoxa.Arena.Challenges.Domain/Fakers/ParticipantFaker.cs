// Filename: ParticipantFaker.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.Fakers.Providers;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    internal sealed class ParticipantFaker : Faker<Participant>
    {
        public ParticipantFaker(
            ChallengeGame game,
            ChallengeSetup setup,
            ChallengeTimeline timeline,
            IScoring scoring,
            DateTime? synchronizedAt
        )
        {
            this.CustomInstantiator(
                faker =>
                {
                    var participant = new Participant(
                        faker.UserId(),
                        faker.Participant().GameAccountId(game),
                        new FakeDateTimeProvider(FakerHub.Date.Between(timeline.CreatedAt, timeline.StartedAt ?? DateTime.UtcNow.DateKeepHours()))
                    );

                    participant.SetEntityId(faker.Participant().Id());

                    if (synchronizedAt.HasValue)
                    {
                        var matchFaker = new MatchFaker(game, scoring, synchronizedAt.Value);

                        matchFaker.UseSeed(faker.Random.Int());

                        var matches = matchFaker.Generate(faker.Random.Int(1, setup.BestOf + 5));

                        matches.ForEach(match => participant.Synchronize(match));

                        participant.Synchronize(new FakeDateTimeProvider(synchronizedAt.Value));
                    }

                    return participant;
                }
            );
        }
    }
}
