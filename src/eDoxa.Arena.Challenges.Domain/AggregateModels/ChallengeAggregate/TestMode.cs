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

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TestMode : ValueObject
    {
        private readonly Random _random = new Random();

        private readonly MatchStatsFaker _matchStatsFaker = new MatchStatsFaker();
        private readonly TimelineFaker _timelineFaker = new TimelineFaker();

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

            challenge.EnableTestMode(this, _timelineFaker.FakeTimeline(StartingState));
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
                    challenge.SnapshotParticipantMatch(participant, new MatchReference(Guid.NewGuid()), _matchStatsFaker.FakeMatchStats(challenge.Game));
                }
            }
        }
    }
}
