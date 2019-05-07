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

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Enumerations;

namespace eDoxa.Challenges.Domain.Services.Factories
{
    public sealed class ScoringFactory
    {
        private static readonly Lazy<ScoringFactory> Lazy = new Lazy<ScoringFactory>(() => new ScoringFactory());

        public static ScoringFactory Instance => Lazy.Value;

        public IScoringStrategy CreateScoringStrategy(Challenge challenge)
        {
            switch (challenge.Game)
            {
                case Game.LeagueOfLegends:

                    return LeagueOfLegendsChallengeScoringFactory.Instance.CreateScoring(challenge);

                default:

                    throw new NotImplementedException();
            }
        }
    }
}