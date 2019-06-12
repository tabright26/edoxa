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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Fakers;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ParticipantFaker : CustomFaker<Participant>
    {
        private readonly UserIdFaker _userIdFaker = new UserIdFaker();
        private readonly ExternalAccountFaker _externalAccountFaker = new ExternalAccountFaker();

        public ParticipantFaker()
        {
            this.CustomInstantiator(faker =>
                {
                    State = State ?? faker.PickRandom(ChallengeState.GetAll());

                    BestOf = BestOf ?? faker.PickRandom(ValueObject.GetDeclaredOnlyFields<BestOf>());

                    return new Participant(_userIdFaker.Generate(), _externalAccountFaker.FakeExternalAccount(Game), BestOf);
                }
            );

            this.RuleFor(participant => participant.Id, faker => ParticipantId.FromGuid(faker.Random.Guid()));

            this.RuleFor(participant => participant.Matches, faker => State != ChallengeState.Inscription ? MatchFaker.FakeMatches(faker.Random.Int(0, BestOf + 3), Game) : new HashSet<Match>());

            this.RuleFor(participant => participant.LastSync, (_, participant) => participant.Matches.Max(match => match.Timestamp as DateTime?));
        }

        public MatchFaker MatchFaker { get; set; } = new MatchFaker();

        private Game Game { get; set; }

        private ChallengeState State { get; set; }

        private BestOf BestOf { get; set; }

        public IEnumerable<Participant> FakeParticipants(int count, Game game, ChallengeState state = null, BestOf bestOf = null)
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
