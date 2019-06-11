// Filename: ParticipantFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Common.Fakers;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ParticipantFaker : CustomFaker<Participant>
    {
        private readonly UserIdFaker _userIdFaker = new UserIdFaker();
        private readonly ExternalAccountFaker _externalAccountFaker = new ExternalAccountFaker();
        //private readonly MatchFaker _matchFaker = new MatchFaker();

        private Game _game;
        private ChallengeState _state;

        public ParticipantFaker(Challenge challenge)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var externalAccount = _externalAccountFaker.FakeExternalAccount(_game ?? faker.PickRandom(Game.GetAll()));

                    var userId = _userIdFaker.FakeUserId();

                    return new Participant(challenge, userId, externalAccount);
                }
            );

            this.FinishWith(
                (faker, participant) =>
                {
                    var state = _state ?? faker.PickRandom(ChallengeState.GetAll());


                }
            );
        }

        public Participant FakeParticipant(Game game = null, ChallengeState state = null)
        {
            _game = game;

            _state = state;

            var participant = this.Generate();

            Console.WriteLine(participant.DumbAsJson());

            return participant;
        }
    }
}
