// Filename: ChallengeBuilder.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeBuilder
    {
        private readonly IChallenge _challenge;

        public ChallengeBuilder(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration
        )
        {
            _challenge = new Challenge(game, name, setup, duration);
        }

        public void StoreScoring(IScoringFactory factory)
        {
            var strategy = factory.CreateStrategy(_challenge);

            _challenge.ApplyScoringStrategy(strategy);
        }

        public void StorePayout(IPayoutFactory factory)
        {
            var strategy = factory.CreateStrategy(_challenge);

            _challenge.ApplyPayoutStrategy(strategy);
        }

        public void EnableTestMode(TestMode testMode)
        {
            testMode.Enable(_challenge as Challenge);
        }

        public IChallenge Build()
        {
            return _challenge;
        }
    }
}
