// Filename: ChallengeFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class ChallengeFaker : CustomFaker<Challenge>
    {
        private readonly ChallengeSetupFaker _challengeSetupFaker = new ChallengeSetupFaker();
        private readonly ChallengeTimelineFaker _challengeTimelineFaker = new ChallengeTimelineFaker();

        private Game _game;
        private ChallengeState _state;
        private CurrencyType _entryFeeCurrency;

        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker => new Challenge(
                    _game ?? faker.PickRandom(Game.GetAll()),
                    new ChallengeName("Challenge"),
                    _challengeSetupFaker.FakeSetup(_entryFeeCurrency ?? faker.PickRandom(CurrencyType.GetAll())),
                    faker.PickRandom(ValueObject.GetDeclaredOnlyFields<ChallengeDuration>())
                )
            );

            this.RuleFor(challenge => challenge.Timeline, (faker, challenge) => _challengeTimelineFaker.FakeTimeline(_state ?? faker.PickRandom(ChallengeState.GetAll())));

            this.FinishWith(
                (faker, challenge) =>
                {
                    var participantFaker = new ParticipantFaker();

                    participantFaker.UseSeed(faker.Random.Int(1000000, 9999999));

                    participantFaker.FakeParticipants(challenge.Setup.Entries, challenge.Game, challenge.State, challenge.Setup.BestOf).ForEach(participant => challenge.RegisterParticipant(participant.UserId, participant.ExternalAccount));
                }
            );
        }

        public IEnumerable<Challenge> FakeChallenges(int count)
        {
            return this.Generate(count);
        }

        public Challenge FakeChallenge(Game game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            _game = game;

            _state = state;

            _entryFeeCurrency = entryFeeCurrency;

            return this.Generate();
        }
    }
}
