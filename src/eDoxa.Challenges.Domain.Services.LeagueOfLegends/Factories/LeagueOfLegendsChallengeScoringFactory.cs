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

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories
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