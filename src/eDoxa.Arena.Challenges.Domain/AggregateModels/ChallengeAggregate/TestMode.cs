// Filename: TestMode.cs
// Date Created: 2019-06-02
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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TestMode : ValueObject
    {
        private readonly Random _random = new Random();

        public TestMode(ChallengeState startingState, TestModeMatchQuantity matchQuantity, TestModeParticipantQuantity participantQuantity) : this()
        {
            StartingState = startingState;
            MatchQuantity = matchQuantity;
            ParticipantQuantity = participantQuantity;
        }

        private TestMode()
        {
            // Required by EF Core.
        }

        public ChallengeState StartingState { get; private set; }

        public TestModeMatchQuantity MatchQuantity { get; private set; }

        public TestModeParticipantQuantity ParticipantQuantity { get; private set; }

        public override string ToString()
        {
            return string.Join(",", this.GetAtomicValues().Select(signature => $"{signature.GetType().Name}={signature.ToString()}"));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return MatchQuantity;
            yield return ParticipantQuantity;
        }

        public void Enable(Challenge challenge)
        {
            this.FakeParticipants(challenge);

            if (StartingState != ChallengeState.Inscription)
            {
                this.FakeMatches(challenge);
            }

            challenge.EnableTestMode(this, this.FakeTimeline(challenge.Timeline.Duration));
        }

        private ChallengeTimeline FakeTimeline(ChallengeDuration duration)
        {
            if (StartingState == ChallengeState.InProgress)
            {
                return new TestModeChallengeTimeline(duration, DateTime.UtcNow);
            }

            if (StartingState == ChallengeState.Ended)
            {
                return new TestModeChallengeTimeline(duration, DateTime.UtcNow.Subtract(duration));
            }

            if (StartingState == ChallengeState.Closed)
            {
                return new TestModeChallengeTimeline(duration, DateTime.UtcNow.Subtract(duration), DateTime.UtcNow);
            }

            return new TestModeChallengeTimeline(duration);
        }

        private void FakeParticipants(Challenge challenge)
        {
            var entries = challenge.Setup.Entries;

            if (ParticipantQuantity == TestModeParticipantQuantity.HalfFull)
            {
                FakeParticipants(challenge, entries / 2);
            }

            if (ParticipantQuantity == TestModeParticipantQuantity.Fulfilled)
            {
                FakeParticipants(challenge, entries);
            }
        }

        private static void FakeParticipants(Challenge challenge, int entries)
        {
            for (var index = 0; index < entries; index++)
            {
                challenge.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));
            }
        }

        private void FakeMatches(Challenge challenge)
        {
            var bestOf = challenge.Setup.BestOf;

            if (MatchQuantity == TestModeMatchQuantity.Under)
            {
                this.FakeMatches(challenge, _random.Next(bestOf - 3, bestOf + 1));
            }

            if (MatchQuantity == TestModeMatchQuantity.Exact)
            {
                this.FakeMatches(challenge, bestOf);
            }

            if (MatchQuantity == TestModeMatchQuantity.Over)
            {
                this.FakeMatches(challenge, _random.Next(bestOf, bestOf + 3));
            }
        }

        private void FakeMatches(Challenge challenge, int bestOf)
        {
            foreach (var participant in challenge.Participants)
            {
                for (var index = 0; index < bestOf; index++)
                {
                    challenge.SnapshotParticipantMatch(participant.Id, this.FakeMatchStats());
                }
            }
        }

        private IMatchStats FakeMatchStats()
        {
            return new MatchStats(
                new MatchExternalId(Guid.NewGuid()),
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
    }
}
