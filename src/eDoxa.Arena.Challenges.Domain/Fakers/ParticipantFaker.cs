// Filename: ParticipantFaker.cs
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
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ParticipantFaker : CustomFaker<Participant>
    {
        private readonly MatchFaker _matchFaker = new MatchFaker();

        public ParticipantFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    State = faker.ChallengeState(State);

                    return new Participant(faker.UserId(), faker.UserGameReference(Game), faker.ChallegeSetupBestOf(BestOf));
                }
            );

            this.RuleFor(participant => participant.Id, faker => faker.ParticipantId());

            this.RuleFor(
                participant => participant.Matches,
                (faker, participant) => State != ChallengeState.Inscription
                    ? _matchFaker.FakeMatches(faker.Random.Int(0, participant.MatchBestOf + 3), Game)
                    : new HashSet<Match>()
            );

            this.RuleFor(participant => participant.Timestamp, (faker, participant) => faker.ParticipantTimestamp(participant.Matches));

            this.RuleFor(participant => participant.LastSync, (faker, participant) => participant.Matches.Max(match => match.Timestamp as DateTime?));
        }

        private Game Game { get; set; }

        private ChallengeState State { get; set; }

        private BestOf BestOf { get; set; }

        [NotNull]
        public override Faker<Participant> UseSeed(int seed)
        {
            _matchFaker.UseSeed(seed);

            return base.UseSeed(seed);
        }

        public IEnumerable<Participant> FakeParticipants(
            int count,
            Game game,
            ChallengeState state = null,
            BestOf bestOf = null
        )
        {
            Game = game;

            State = state;

            BestOf = bestOf;

            return this.Generate(count);
        }

        public IEnumerable<Participant> FakeParticipants(int count, Challenge challenge)
        {
            Game = challenge.Game;

            State = challenge.State;

            BestOf = challenge.Setup.BestOf;

            return this.Generate(count);
        }

        public Participant FakeParticipant(Game game, ChallengeState state = null, BestOf bestOf = null)
        {
            Game = game;

            State = state;

            BestOf = bestOf;

            return this.Generate();
        }

        public Participant FakeParticipant(Challenge challenge)
        {
            Game = challenge.Game;

            State = challenge.State;

            BestOf = challenge.Setup.BestOf;

            return this.Generate();
        }
    }
}
