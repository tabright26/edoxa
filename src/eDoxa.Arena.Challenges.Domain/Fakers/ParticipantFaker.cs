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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Fakers;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ParticipantFaker : CustomFaker<Participant>
    {
        private readonly UserIdFaker _userIdFaker = new UserIdFaker();
        private readonly ExternalAccountFaker _externalAccountFaker = new ExternalAccountFaker();
        private readonly MatchFaker _matchFaker = new MatchFaker();

        private Game _game;
        private ChallengeState _state;
        private BestOf _bestOf;

        public ParticipantFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    _matchFaker.UseSeed(faker.Random.Int(1000000, 9999999));

                    var game = _game ?? faker.PickRandom(Game.GetAll());

                    var state = _state ?? faker.PickRandom(ChallengeState.GetAll());

                    var userId = _userIdFaker.FakeUserId();

                    var externalAccount = _externalAccountFaker.FakeExternalAccount(game);

                    var bestOf = _bestOf ?? faker.PickRandom(ValueObject.GetDeclaredOnlyFields<BestOf>());

                    var participant = new Participant(userId, externalAccount, bestOf);

                    var matches = _matchFaker.FakeMatches(faker.Random.Int(0, bestOf + 3), game);

                    if (state != ChallengeState.Inscription)
                    {
                        matches.ForEach(match => participant.SnapshotMatch(match.Reference, new MatchStats(match.Stats), new Scoring(match.Stats)));
                    }

                    return participant;
                }
            );

            this.RuleFor(participant => participant.LastSync, (_, participant) => participant.Matches.Max(match => match.Timestamp as DateTime?));
        }

        [NotNull]
        public override Faker<Participant> UseSeed(int seed)
        {
            var challengeFaker = base.UseSeed(seed);

            _userIdFaker.UseSeed(seed);

            _externalAccountFaker.UseSeed(seed);

            _matchFaker.UseSeed(seed);

            return challengeFaker;
        }

        public IEnumerable<Participant> FakeParticipants(int count, Game game = null, ChallengeState state = null, BestOf bestOf = null)
        {
            _game = game;

            _state = state;

            _bestOf = bestOf;

            return this.Generate(count);
        }

        public Participant FakeParticipant(Game game = null, ChallengeState state = null, BestOf bestOf = null)
        {
            _game = game;

            _state = state;

            _bestOf = bestOf;

            return this.Generate();
        }
    }
}
