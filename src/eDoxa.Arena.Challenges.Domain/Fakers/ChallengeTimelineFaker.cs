// Filename: ChallengeTimelineFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ChallengeTimelineFaker : CustomFaker<ChallengeTimeline>
    {
        private static readonly IEnumerable<ChallengeDuration> Durations = ValueObject.GetDeclaredOnlyFields<ChallengeDuration>();

        public ChallengeTimelineFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    State = State ?? faker.PickRandom(ChallengeState.GetAll());

                    return new ChallengeTimeline(faker.PickRandom(Durations));
                }
            );

            this.RuleFor(
                timeline => timeline.StartedAt,
                (faker, timeline) =>
                {
                    if (State == ChallengeState.InProgress)
                    {
                        return DateTime.UtcNow;
                    }

                    if (State == ChallengeState.Ended || State == ChallengeState.Closed)
                    {
                        return DateTime.UtcNow.Subtract(timeline.Duration);
                    }

                    return timeline.StartedAt;
                }
            );

            this.RuleFor(timeline => timeline.ClosedAt, (faker, timeline) => State == ChallengeState.Closed ? DateTime.UtcNow : timeline.ClosedAt);
        }

        private ChallengeState State { get; set; }

        public ChallengeTimeline FakeTimeline(ChallengeState state = null)
        {
            State = state;

            return this.Generate();
        }
    }
}
