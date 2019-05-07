// Filename: FakeRandomChallengeFactory.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;

namespace eDoxa.Challenges.Domain.Factories
{
    public sealed class FakeRandomChallengeFactory
    {
        public const int DefaultRandomChallengeCount = 5;

        private static readonly Random Random = new Random();
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;
        private static readonly Lazy<FakeRandomChallengeFactory> Lazy = new Lazy<FakeRandomChallengeFactory>(() => new FakeRandomChallengeFactory());

        public static FakeRandomChallengeFactory Instance => Lazy.Value;

        public IReadOnlyCollection<Challenge> CreateRandomChallenges(ChallengeState1 state = ChallengeState1.Opened)
        {
            var challenges = new Collection<Challenge>();

            for (var row = 0; row < DefaultRandomChallengeCount; row++)
            {
                challenges.Add(this.CreateRandomChallengeFromState(state));
            }

            return challenges;
        }

        public Challenge CreateRandomChallenge(ChallengeState1 state = ChallengeState1.Opened)
        {
            return this.CreateRandomChallenges(state).First();
        }

        public IReadOnlyCollection<Challenge> CreateRandomChallengesWithOtherStates(ChallengeState1 state)
        {
            return this.CreateRandomChallenges(state).Union(this.CreateOtherRandomChallenges(state)).ToList();
        }

        private Challenge CreateRandomChallengeFromState(ChallengeState1 state)
        {
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(state);

            if (state >= ChallengeState1.Opened)
            {
                var timeline = challenge.Timeline;

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(challenge, FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState1.Opened));

                for (var row = 0; row < Random.Next(1, challenge.Setup.Entries + 1); row++)
                {
                    var userId = new UserId();

                    challenge.RegisterParticipant(userId, new LinkedAccount(Guid.NewGuid()));
                }

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(challenge, FakeDefaultChallengeFactory.CreateChallengeTimeline(ChallengeState1.InProgress));

                foreach (var participant in challenge.Participants)
                {
                    for (var index = 0; index < Random.Next(1, challenge.Setup.BestOf + Random.Next(0, challenge.Setup.BestOf + 1) + 1); index++)
                    {
                        var stats = FakeDefaultChallengeFactory.CreateMatchStats();

                        challenge.SnapshotParticipantMatch(participant.Id, stats);
                    }
                }

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, timeline);
            }

            return challenge;
        }

        private IReadOnlyCollection<Challenge> CreateOtherRandomChallenges(ChallengeState1 state)
        {
            var challenges = new Collection<Challenge>();

            foreach (var otherState in OtherStates(state))
            {
                for (var row = 0; row < DefaultRandomChallengeCount; row++)
                {
                    challenges.Add(this.CreateRandomChallengeFromState(otherState));
                }
            }

            return challenges;
        }

        private static IEnumerable<ChallengeState1> OtherStates(ChallengeState1 state)
        {
            var states = Enum.GetValues(typeof(ChallengeState1)).Cast<ChallengeState1>().ToList();

            states.Remove(state);

            states.Remove(ChallengeState1.None);

            states.Remove(ChallengeState1.All);

            return states;
        }
    }
}