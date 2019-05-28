// Filename: TestModeFactory.cs
// Date Created: 2019-05-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Domain;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Entities;

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public class TestModeFactory
    {
        private static readonly Lazy<TestModeFactory> Lazy = new Lazy<TestModeFactory>(() => new TestModeFactory());

        public static TestModeFactory Instance => Lazy.Value;

        public Challenge CreateChallenge(Challenge challenge, ChallengeState state)
        {
            return new TestModeChallenge(challenge, state);
        }

        private sealed class TestModeChallenge : Challenge
        {
            private readonly Random _random = new Random();

            public TestModeChallenge(Challenge challenge, ChallengeState state) : base(
                challenge.Game,
                challenge.Name,
                challenge.Setup,
                challenge.Timeline,
                challenge.Scoring,
                true
            )
            {
                if (state != ChallengeState.Inscription)
                {
                    this.FakeParticipants();

                    if (state == ChallengeState.Ended || state == ChallengeState.Closed)
                    {
                        this.FakeMatches();
                    }
                }

                Timeline = FakeTimeline(challenge.Timeline, state);
            }

            private static ChallengeTimeline FakeTimeline(ChallengeTimeline timeline, ChallengeState state)
            {
                if (state == ChallengeState.InProgress)
                {
                    return new TestModeChallengeTimeline(timeline.Duration);
                }

                if (state == ChallengeState.Ended)
                {
                    return new TestModeChallengeTimeline(timeline.Duration, DateTime.UtcNow.Subtract(timeline.Duration));
                }

                if (state == ChallengeState.Closed)
                {
                    return new TestModeChallengeTimeline(timeline.Duration, DateTime.UtcNow.Subtract(timeline.Duration), DateTime.UtcNow);
                }

                return timeline;
            }

            private void FakeParticipants()
            {
                for (var index = 0; index < Setup.Entries; index++)
                {
                    this.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));
                }
            }

            private void FakeMatches()
            {
                Participants.ForEach(
                    participant =>
                    {
                        for (var index = 0; index < _random.Next(2, Setup.BestOf + 2); index++)
                        {
                            this.SnapshotParticipantMatch(participant.Id, this.FakeMatchStats());
                        }
                    }
                );
            }

            private IMatchStats FakeMatchStats()
            {
                return new MatchStats(
                    new MatchExternalId(2233345251),
                    new
                    {
                        Kills = _random.Next(0, 40 + 1),
                        Deaths = _random.Next(0, 15 + 1),
                        Assists = _random.Next(0, 50 + 1),
                        TotalDamageDealtToChampions = _random.Next(10000, 500000 + 1),
                        TotalHeal = _random.Next(10000, 350000 + 1)
                    }
                );
            }

            private sealed class TestModeChallengeTimeline : ChallengeTimeline
            {
                public TestModeChallengeTimeline(ChallengeDuration duration) : base(duration)
                {
                }

                public TestModeChallengeTimeline(ChallengeDuration duration, DateTime startedAt) : base(duration, startedAt)
                {
                }

                public TestModeChallengeTimeline(ChallengeDuration duration, DateTime startedAt, DateTime closedAt) : base(duration, startedAt, closedAt)
                {
                }
            }
        }
    }
}
