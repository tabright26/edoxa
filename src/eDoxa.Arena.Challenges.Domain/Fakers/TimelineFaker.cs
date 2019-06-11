// Filename: TimelineFaker.cs
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
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class TimelineFaker : CustomFaker<ChallengeTimeline>
    {
        private static readonly IEnumerable<ChallengeDuration> Durations = ValueObject.GetDeclaredOnlyFields<ChallengeDuration>();

        public TimelineFaker()
        {
            this.RuleSet(
                ChallengeState.Inscription.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new ChallengeTimeline(faker.PickRandom(Durations)));
                }
            );

            this.RuleSet(
                ChallengeState.InProgress.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new ChallengeTimeline(faker.PickRandom(Durations)));

                    ruleSet.RuleFor(timeline => timeline.StartedAt, DateTime.UtcNow);
                }
            );

            this.RuleSet(
                ChallengeState.Ended.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new ChallengeTimeline(faker.PickRandom(Durations)));

                    ruleSet.RuleFor(timeline => timeline.StartedAt, (faker, timeline) => DateTime.UtcNow.Subtract(timeline.Duration));
                }
            );

            this.RuleSet(
                ChallengeState.Closed.ToString(),
                ruleSet =>
                {
                    ruleSet.CustomInstantiator(faker => new ChallengeTimeline(faker.PickRandom(Durations)));

                    ruleSet.RuleFor(timeline => timeline.StartedAt, (faker, timeline) => DateTime.UtcNow.Subtract(timeline.Duration));

                    ruleSet.RuleFor(timeline => timeline.ClosedAt, DateTime.UtcNow);
                }
            );
        }

        public ChallengeTimeline FakeTimeline(ChallengeState state)
        {
            var timeline = this.Generate(state.ToString());

            Console.WriteLine(timeline.DumbAsJson());

            return timeline;
        }
    }
}
