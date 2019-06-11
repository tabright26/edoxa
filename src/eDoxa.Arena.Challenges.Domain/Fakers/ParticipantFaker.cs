// Filename: ParticipantFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Fakers;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ParticipantFaker : CustomFaker<Participant>
    {
        private readonly UserIdFaker _userIdFaker = new UserIdFaker();
        private readonly ExternalAccountFaker _externalAccountFaker = new ExternalAccountFaker();

        private Game _game;
        private ChallengeState _state;
        private BestOf _bestOf;

        public ParticipantFaker()
        {
            this.CustomInstantiator(
                faker =>
                {
                    var game = _game ?? faker.PickRandom(Game.GetAll());

                    var state = _state ?? faker.PickRandom(ChallengeState.GetAll());

                    var userId = _userIdFaker.FakeUserId();

                    var externalAccount = _externalAccountFaker.FakeExternalAccount(game);

                    var bestOf = _bestOf ?? faker.PickRandom(ValueObject.GetDeclaredOnlyFields<BestOf>());

                    var participant = new Participant(userId, externalAccount, bestOf);

                    if (state != ChallengeState.Inscription)
                    {
                        var matchFaker = new MatchFaker();

                        matchFaker.UseSeed(faker.Random.Int(1000000, 9999999));

                        var matches = matchFaker.FakeMatches(faker.Random.Int(0, bestOf + 3), game);

                        matches.ForEach(match => participant.SnapshotMatch(match.Reference, new MatchStats(match.Stats), new Scoring(match.Stats)));
                    }

                    return participant;
                }
            );
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
