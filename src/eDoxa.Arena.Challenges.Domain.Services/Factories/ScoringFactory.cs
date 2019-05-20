// Filename: ScoringFactory.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Services.Factories
{
    public sealed class ScoringFactory
    {
        private static readonly Lazy<ScoringFactory> Lazy = new Lazy<ScoringFactory>(() => new ScoringFactory());

        public static ScoringFactory Instance => Lazy.Value;

        public IScoringStrategy CreateScoringStrategy(Challenge challenge)
        {
            if (challenge.Game.Equals(Game.LeagueOfLegends))
            {
                return LeagueOfLegendsChallengeScoringFactory.Instance.CreateScoring(challenge);
            }

            throw new NotImplementedException();
        }
    }
}