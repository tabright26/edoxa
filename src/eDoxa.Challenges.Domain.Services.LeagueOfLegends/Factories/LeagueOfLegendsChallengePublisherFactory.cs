// Filename: LeagueOfLegendsChallengePublisherFactory.cs
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
    public sealed class LeagueOfLegendsChallengePublisherFactory
    {
        private static readonly Lazy<LeagueOfLegendsChallengePublisherFactory> _lazy =
            new Lazy<LeagueOfLegendsChallengePublisherFactory>(() => new LeagueOfLegendsChallengePublisherFactory());

        public static LeagueOfLegendsChallengePublisherFactory Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        public IChallengePublisherStrategy Create(ChallengePublisherPeriodicity periodicity)
        {
            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:
                    return new LeagueOfLegendsDailyChallengePublisherStrategy();
                case ChallengePublisherPeriodicity.Weekly:
                    return new LeagueOfLegendsWeeklyChallengePublisherStrategy();
                case ChallengePublisherPeriodicity.Monthly:
                    return new LeagueOfLegendsMonthlyChallengePublisherStrategy();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}