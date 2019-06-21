// Filename: ScoringFactory.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public sealed class ScoringFactory
    {
        private static readonly Lazy<ScoringFactory> Lazy = new Lazy<ScoringFactory>(() => new ScoringFactory());

        public static ScoringFactory Instance => Lazy.Value;

        public IScoringStrategy CreateStrategy(IChallenge challenge)
        {
            return this.CreateStrategy(challenge.Game);
        }

        public IScoringStrategy CreateStrategy(ChallengeGame game)
        {
            if (game == ChallengeGame.LeagueOfLegends)
            {
                return new LeagueOfLegendsScoringStrategy();
            }

            throw new NotImplementedException();
        }
    }
}
