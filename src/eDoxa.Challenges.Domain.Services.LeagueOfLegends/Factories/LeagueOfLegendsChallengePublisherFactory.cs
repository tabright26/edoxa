// Filename: LeagueOfLegendsChallengePublisherFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories
{
    public sealed class LeagueOfLegendsChallengePublisherFactory
    {
        private static readonly Lazy<LeagueOfLegendsChallengePublisherFactory> Lazy =
            new Lazy<LeagueOfLegendsChallengePublisherFactory>(() => new LeagueOfLegendsChallengePublisherFactory());

        public static LeagueOfLegendsChallengePublisherFactory Instance => Lazy.Value;

        public IPublisherStrategy CreatePublisher(ChallengeInterval interval)
        {
            if (interval == ChallengeInterval.Daily)
            {
                return new LeagueOfLegendsDailyPublisherStrategy();
            }

            if (interval == ChallengeInterval.Weekly)
            {
                return new LeagueOfLegendsWeeklyPublisherStrategy();
            }

            if (interval == ChallengeInterval.Monthly)
            {
                return new LeagueOfLegendsMonthlyPublisherStrategy();
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}