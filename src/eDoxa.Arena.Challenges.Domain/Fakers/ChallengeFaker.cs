// Filename: ChallengeFaker.cs
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
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class ChallengeFaker : CustomFaker<Challenge>
    {
        private readonly ChallengeSetupFaker _challengeSetupFaker = new ChallengeSetupFaker();
        private readonly ChallengeTimelineFaker _challengeTimelineFaker = new ChallengeTimelineFaker();
        private readonly ParticipantFaker _participantFaker = new ParticipantFaker();

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

            this.RuleFor(challenge => challenge.Participants, (_, challenge) => _participantFaker.FakeParticipants(challenge.Setup.Entries, challenge.Game, challenge.State, challenge.Setup.BestOf));

            this.RuleFor(challenge => challenge.LastSync, (_, challenge) => challenge.Participants.Max(participant => participant.Timestamp as DateTime?));
        }

        [NotNull]
        public override Faker<Challenge> UseSeed(int seed)
        {
            var challengeFaker = base.UseSeed(seed);

            _challengeSetupFaker.UseSeed(seed);

            _challengeTimelineFaker.UseSeed(seed);

            _participantFaker.UseSeed(seed);

            return challengeFaker;
        }

        public IEnumerable<Challenge> FakeChallenges(
            int count,
            Game game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null
        )
        {
            _game = game;

            _state = state;

            _entryFeeCurrency = entryFeeCurrency;

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
