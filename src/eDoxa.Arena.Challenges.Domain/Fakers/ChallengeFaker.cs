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

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public class ChallengeFaker : CustomFaker<Challenge>
    {
        private readonly SetupFaker _setupFaker = new SetupFaker();
        private readonly TimelineFaker _timelineFaker = new TimelineFaker();

        private Game _game;
        private ChallengeState _state;
        private CurrencyType _entryFeeCurrency;

        public ChallengeFaker()
        {
            this.CustomInstantiator(
                faker => new Challenge(
                    _game ?? faker.PickRandom(Game.GetAll()),
                    new ChallengeName("Challenge"),
                    _setupFaker.FakeSetup(_entryFeeCurrency ?? faker.PickRandom(CurrencyType.GetAll())),
                    faker.PickRandom(ValueObject.GetDeclaredOnlyFields<ChallengeDuration>())
                )
            );

            this.RuleFor(challenge => challenge.Timeline, faker => _timelineFaker.FakeTimeline(_state ?? faker.PickRandom(ChallengeState.GetAll())));
        }

        public IEnumerable<Challenge> FakeChallenges(int count)
        {
            var challenges = this.Generate(count);

            Console.WriteLine(challenges.DumbAsJson());

            return challenges;
        }

        public Challenge FakeChallenge(Game game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            _game = game;

            _state = state;

            _entryFeeCurrency = entryFeeCurrency;

            var challenge = this.Generate();

            Console.WriteLine(challenge.DumbAsJson());

            return challenge;
        }
    }
}
