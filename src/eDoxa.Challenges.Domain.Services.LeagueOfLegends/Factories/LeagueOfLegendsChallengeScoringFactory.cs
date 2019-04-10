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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories
{
    public sealed class LeagueOfLegendsChallengeScoringFactory
    {
        private static readonly Lazy<LeagueOfLegendsChallengeScoringFactory> _lazy =
            new Lazy<LeagueOfLegendsChallengeScoringFactory>(() => new LeagueOfLegendsChallengeScoringFactory());

        public static LeagueOfLegendsChallengeScoringFactory Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        public IChallengeScoringStrategy Create(Challenge challenge)
        {
            switch (challenge.Settings.Type)
            {
                case ChallengeType.Default:
                    return new LeagueOfLegendsDefaultChallengeScoringStrategy();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}