// Filename: LeagueOfLegendsChallengeScoringFactory.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Factories
{
    public sealed class LeagueOfLegendsChallengeScoringFactory
    {
        private static readonly Lazy<LeagueOfLegendsChallengeScoringFactory> Lazy =
            new Lazy<LeagueOfLegendsChallengeScoringFactory>(() => new LeagueOfLegendsChallengeScoringFactory());

        public static LeagueOfLegendsChallengeScoringFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }

        public IScoringStrategy CreateScoring(Challenge challenge)
        {
            var type = challenge.Setup.Type;

            if (type.Equals(ChallengeType.Default))
            {
                return new LeagueOfLegendsDefaultScoringStrategy();
            }

            if (type.Equals(ChallengeType.Random))
            {
                return new LeagueOfLegendsDefaultScoringStrategy();
            }

            throw new NotImplementedException();
        }
    }
}