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

using eDoxa.Challenges.Domain.Abstractions;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories
{
    public sealed class LeagueOfLegendsChallengePublisherFactory
    {
        private static readonly Lazy<LeagueOfLegendsChallengePublisherFactory> Lazy =
            new Lazy<LeagueOfLegendsChallengePublisherFactory>(() => new LeagueOfLegendsChallengePublisherFactory());

        public static LeagueOfLegendsChallengePublisherFactory Instance => Lazy.Value;

        public IPublisherStrategy CreatePublisher(PublisherInterval interval)
        {
            if (interval == PublisherInterval.Daily)
            {
                return new LeagueOfLegendsDailyPublisherStrategy();
            }

            if (interval == PublisherInterval.Weekly)
            {
                return new LeagueOfLegendsWeeklyPublisherStrategy();
            }

            if (interval == PublisherInterval.Monthly)
            {
                return new LeagueOfLegendsMonthlyPublisherStrategy();
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}