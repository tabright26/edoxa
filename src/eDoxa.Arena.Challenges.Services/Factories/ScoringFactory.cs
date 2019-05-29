// Filename: ScoringFactory.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Services.Factories
{
    public sealed class ScoringFactory
    {
        private static readonly Lazy<ScoringFactory> Lazy = new Lazy<ScoringFactory>(() => new ScoringFactory());

        public static ScoringFactory Instance => Lazy.Value;

        public IScoringStrategy CreateScoringStrategy(Game game)
        {
            if (game.Equals(Game.LeagueOfLegends))
            {
                return new LeagueOfLegendsScoringStrategy();
            }

            throw new NotImplementedException();
        }
    }
}
